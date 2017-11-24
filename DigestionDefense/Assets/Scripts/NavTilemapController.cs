using UnityEngine;
using UnityEngine.Tilemaps;

public sealed class NavTilemapController
{
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
			Setup();
		}
	}

	private MyPathNode[,] m_Grid;

	public MyPathNode[,] grid
	{
		get
		{
			return m_Grid;
		}
	}

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

	private void Setup()
	{
		m_Grid = ParseGrid(m_Tilemap);
	}

	private MyPathNode[,] ParseGrid(Tilemap tilemap)
	{
		int width = tilemap.size.x;
		int height = tilemap.size.y;
		MyPathNode[,] grid = new MyPathNode[tilemap.size.x, tilemap.size.y];
		for (int x = 0; x < width; ++x)
		{
			for (int y = 0; y < height; ++y)
			{
				bool isWall = IsCollider(tilemap, x, y);
				grid[x, y] = new MyPathNode()
				{
					IsWall = isWall,
					X = x,
					Y = y,
				};
			}
		}
		return grid;
	}

	private bool IsCollider(Tilemap tilemap, int x, int y)
	{
		Tile.ColliderType collider = tilemap.GetColliderType(new Vector3Int(x, y, 0));
		return collider != Tile.ColliderType.None;
	}
}
