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
			FindPathInWorld();
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
			SetCurrentCell(value);
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

	private bool m_IsVerbose = true;

	private void SetCurrentCell(Vector3 positionInWorld)
	{
		if (m_Nav == null || m_Nav.tilemap == null)
		{
			return;
		}
		m_CurrentCell = m_Nav.WorldToGrid(positionInWorld);
	}

	private void SetDestinationCell(Vector3 destinationInWorld)
	{
		if (m_Nav == null || m_Nav.tilemap == null)
		{
			return;
		}
		m_DestinationCell = m_Nav.WorldToGrid(destinationInWorld);
	}

	private void FindPathInWorld()
	{
		if (m_IsVerbose)
		{
			Debug.Log("NavTilemapAgent.FindPath: From "
				+ m_Position + " to " + m_Destination);
		}
		SetDestinationCell(m_Destination);
		SetCurrentCell(m_Position);
		FindPath();
	}

	private void FindPath()
	{
		if (m_Solver == null)
		{
			m_Solver = new SpatialAStar<MyPathNode, object>(m_Nav.grid);
		}
		if (m_IsVerbose)
		{
			Debug.Log("NavTilemapAgent.FindPath: From "
				+ m_CurrentCell + " to " + m_DestinationCell);
		}
		IEnumerable<MyPathNode> path = m_Solver.Search(
			(Vector2)m_CurrentCell,
			(Vector2)m_DestinationCell,
			null);
	}

	public void Update(float deltaTime)
	{
		if (deltaTime <= 0.0f)
		{
			return;
		}
		if (path == null)
		{
			return;
		}
	}
}
