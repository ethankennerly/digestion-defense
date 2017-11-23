using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public sealed class NavTilemapView : MonoBehaviour
{
	private NavTilemapController m_Controller = new NavTilemapController();

	public NavTilemapController controller
	{
		get
		{
			return m_Controller;
		}
	}

	private void Start()
	{
		m_Controller.tilemap = (Tilemap)GetComponent(typeof(Tilemap));
	}
}
