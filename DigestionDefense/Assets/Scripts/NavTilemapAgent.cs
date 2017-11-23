using System.Collections.Generic;
using SettlersEngine;
using UnityEngine;
using UnityEngine.Tilemaps;

public sealed class NavTilemapAgent
{
	private Vector3 m_Destination;

	public Vector3 destination
	{
		get
		{
			return m_Destination;
		}
		set
		{
			m_Destination = value;
			SetPath(value);
		}
	}

	private Vector3 m_Position;

	public Vector3 position
	{
		get
		{
			return m_Position;
		}
		set
		{
			m_Position = value;
			Vector3Int cell = m_Nav.tilemap.WorldToCell(value);
			m_CurrentCell.x = cell.x;
			m_CurrentCell.y = cell.y;
		}
	}

	private NavTilemapController m_Nav;

	public NavTilemapController nav
	{
		get
		{
			return m_Nav;
		}
		set
		{
			m_Nav = value;
		}
	}

	private Vector2Int m_CurrentCell = new Vector2Int();

	private Vector2Int m_DestinationCell = new Vector2Int();

	private SpatialAStar<MyPathNode, object> m_Solver;

	private IEnumerable<MyPathNode> path;

	private void SetPath(Vector3 destinationInWorld)
	{
		Vector3Int cell = m_Nav.tilemap.WorldToCell(destinationInWorld);
		m_DestinationCell.x = cell.x;
		m_DestinationCell.y = cell.y;
		FindPath();
	}

	private void FindPath()
	{
		if (m_Solver == null)
		{
			m_Solver = new SpatialAStar<MyPathNode, object>(m_Nav.grid);
		}
		IEnumerable<MyPathNode> path = m_Solver.Search(
			(Vector2)m_CurrentCell,
			(Vector2)m_DestinationCell,
			null);
	}

	private void UpdatePosition(float deltaTime)
	{
		if (deltaTime <= 0.0f)
		{
			return;
		}
		if (path == null)
		{
			return;
		}
		// TODO
	}
}
