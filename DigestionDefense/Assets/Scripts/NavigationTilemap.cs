using SettlersEngine;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public sealed class NavigationTilemap : MonoBehaviour
{
	private Tilemap m_Tilemap;

	private SpatialAStar<MyPathNode, object> m_Solver;

	private MyPathNode[,] m_Grid;

	private void Start()
	{
		m_Tilemap = (Tilemap)GetComponent(typeof(Tilemap));
		m_Grid = ParseGrid(m_Tilemap);
		m_Solver = new SpatialAStar<MyPathNode, object>(m_Grid);
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
