using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public sealed class NavTilemapView : MonoBehaviour
{
	[Tooltip("Optional for debugging. If defined, the game object is spawned at each passable node in the navigation grid.")]
	[SerializeField]
	private GameObject m_PassableVisualization;

	public GameObject passableVisualization
	{
		get
		{
			return m_PassableVisualization;
		}
		set
		{
			m_PassableVisualization = value;
		}
	}

	private NavTilemapController m_Controller = new NavTilemapController();

	public NavTilemapController controller
	{
		get
		{
			return m_Controller;
		}
	}

	private void OnEnable()
	{
		m_Controller.onPassableCreated += VisualizePassable;
		if (m_Controller.tilemap == null)
		{
			m_Controller.tilemap = (Tilemap)GetComponent(typeof(Tilemap));
		}
	}

	private void OnDisable()
	{
		m_Controller.onPassableCreated -= VisualizePassable;
	}

	private void VisualizePassable(Vector2 worldPoint)
	{
		if (passableVisualization == null)
		{
			return;
		}
		GameObject passableNode = (GameObject)Instantiate(passableVisualization, transform);
		Vector3 position = transform.position;
		position.x = worldPoint.x;
		position.y = worldPoint.y;
		passableNode.transform.position = position;
		passableNode.name += worldPoint.ToString();
	}
}
