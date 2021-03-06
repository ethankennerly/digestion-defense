using SettlersEngine;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace FineGameDesign.Nav
{
    public sealed class NavTilemapController
    {
        public event Action<Vector2> onPassableCreated;

        private int m_TilemapLayerMask;

        private Tilemap m_Tilemap;

        public Tilemap tilemap
        {
            get
            {
                return m_Tilemap;
            }
            set
            {
                if (m_Tilemap == value)
                {
                    return;
                }
                m_Tilemap = value;

                m_TilemapLayerMask = 1 << value.gameObject.layer;

                Setup();
            }
        }

        private WalkableNode[,] m_Grid;

        public WalkableNode[,] grid
        {
            get
            {
                return m_Grid;
            }
        }

        private bool m_IsVerbose = false;

        // Offsets from minimum cell bounds, which might be less than zero.
        // Clamps within bounds.  Otherwise the cell is off the grid.
        public Vector2Int WorldToGrid(Vector3 positionInWorld)
        {
            Vector3Int cell3 = m_Tilemap.WorldToCell(positionInWorld);
            BoundsInt bounds = m_Tilemap.cellBounds;
            Vector2Int size = new Vector2Int(bounds.size.x - 1, bounds.size.y - 1);
            Vector2Int cell = new Vector2Int(cell3.x - bounds.xMin, cell3.y - bounds.yMin);
            cell.Clamp(Vector2Int.zero, size);
            return cell;
        }

        // Returns center of cell in grid.
        public Vector3 GridToWorld(Vector2Int cell)
        {
            BoundsInt bounds = m_Tilemap.cellBounds;
            Vector3Int cell3 = new Vector3Int(cell.x + bounds.xMin, cell.y + bounds.yMin, 0);
            Vector3 position = m_Tilemap.CellToWorld(cell3);
            Vector3 cellSize = m_Tilemap.layoutGrid.cellSize;
            position.x += 0.5f * cellSize.x;
            position.y += 0.5f * cellSize.y;
            return position;
        }

        private Vector2 GridToWorld2D(int gridX, int gridY)
        {
            Vector2Int cell = new Vector2Int(gridX, gridY);
            Vector3 position = GridToWorld(cell);
            Vector2 position2D = new Vector2(position.x, position.y);
            return position2D;
        }

        private Vector3Int GridToCell(int gridX, int gridY)
        {
            BoundsInt bounds = m_Tilemap.cellBounds;
            Vector3Int cell = new Vector3Int(gridX - bounds.xMin, gridY - bounds.yMin, 0);
            return cell;
        }

        private bool IsCollision(int gridX, int gridY)
        {
            Vector2 point = GridToWorld2D(gridX, gridY);
            Collider2D collider = Physics2D.OverlapPoint(point, m_TilemapLayerMask);
            if (collider == null)
                return false;

            if (collider is TilemapCollider2D)
                return true;

            if (collider.isTrigger)
                return false;

            return true;
        }

        private void Setup()
        {
            m_Grid = ParseGrid(m_Tilemap);
        }

        private WalkableNode[,] ParseGrid(Tilemap wallTilemap)
        {
            int width = wallTilemap.size.x;
            int height = wallTilemap.size.y;
            WalkableNode[,] grid = new WalkableNode[width, height];
            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    bool isWall = IsCollision(x, y);
                    grid[x, y] = new WalkableNode()
                    {
                        IsWall = isWall,
                        X = x,
                        Y = y,
                    };
                    if (m_IsVerbose && isWall)
                    {
                        Debug.Log("NavTilemapController.ParseGrid: ("
                            + x + ", " + y + ") is a wall.");
                    }
                    if (isWall || onPassableCreated == null)
                    {
                        continue;
                    }
                    onPassableCreated(GridToWorld2D(x, y));
                }
            }
            return grid;
        }
    }
}
