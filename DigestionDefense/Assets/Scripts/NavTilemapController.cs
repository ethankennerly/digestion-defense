using SettlersEngine;
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

	private SpatialAStar<MyPathNode, object> m_Solver;

	private MyPathNode[,] m_Grid;

	private void Setup()
	{
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
