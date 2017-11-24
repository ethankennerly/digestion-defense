using System;
using System.Collections.Generic;
using SettlersEngine;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public sealed class NavTilemapAgent
{
	[SerializeField]
	private bool m_AllowsDiagonals = true;

	public bool allowsDiagonals
	{
		get
		{
			return m_AllowsDiagonals;
		}
		set
		{
			m_AllowsDiagonals = value;
			if (m_Solver == null)
			{
				return;
			}
			CreateSolver();
		}
	}

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

	public event Action<Vector3> onPositionChanged;

	private Vector3 m_Position;

	public Vector3 position
	{
		get
		{
			return m_Position;
		}
		set
		{
			if (m_Position == value)
			{
				return;
			}
			m_Position = value;
			SetCurrentCell(value);
			if (onPositionChanged == null)
			{
				return;
			}
			onPositionChanged(value);
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

	private IEnumerable<MyPathNode> m_Path;

	private float m_TweenTime = 0.0f;

	private float m_StepDuration = 0.25f;

	private bool m_IsVerbose = false;

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

	private void CreateSolver()
	{
		m_Solver = new SpatialAStar<MyPathNode, object>(m_Nav.grid);
		m_Solver.allowsDiagonals = m_AllowsDiagonals;
	}

	private void FindPath()
	{
		if (m_Solver == null)
		{
			CreateSolver();
		}
		m_Path = m_Solver.Search(
			(Vector2)m_CurrentCell,
			(Vector2)m_DestinationCell,
			null);
		if (m_IsVerbose)
		{
			Debug.Log("NavTilemapAgent.FindPath: From "
				+ m_CurrentCell + " to " + m_DestinationCell
				+ " path " + m_Path);
		}
	}

	public void Update(float deltaTime)
	{
		if (deltaTime <= 0.0f)
		{
			return;
		}
		if (!TweenStep(deltaTime))
		{
			return;
		}
		Step();
	}

	private bool TweenStep(float deltaTime)
	{
		if (m_Path == null)
		{
			return false;
		}
		m_TweenTime += deltaTime;
		if (m_TweenTime < m_StepDuration)
		{
			return false;
		}
		m_TweenTime -= m_StepDuration;
		return true;
	}

	private void Step()
	{
		FindPath();
		if (m_Path == null)
		{
			return;
		}
		int index = 0;
		MyPathNode nextNode = null;
		// XXX It would be more efficient if path were a list or an array rather than IEnumerable.
		foreach (MyPathNode node in m_Path)
		{
			if (index == 1)
			{
				nextNode = node;
				break;
			}
			++index;
		}
		if (nextNode == null)
		{
			return;
		}
		m_CurrentCell.x = nextNode.X;
		m_CurrentCell.y = nextNode.Y;
		position = m_Nav.GridToWorld(m_CurrentCell);
	}
}
