using System;
using UnityEngine;

public static class ClickPoint
{
	public static event Action<Vector3> onCollisionEnter;
	public static event Action<Vector3> onClick;

	private static Vector3 s_RaycastHit = new Vector3();

	private static Vector3 s_Click = new Vector3();

	private static float s_UpdateTime = -1.0f;

	public static bool Raycast()
	{
		RaycastHit hit;
		if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
		{
			return false;
		}
		s_RaycastHit = hit.point;
		if (onCollisionEnter != null)
		{
			onCollisionEnter(s_RaycastHit);
		}
		return true;
	}

	public static bool Screen()
	{
		s_Click = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		if (onClick != null)
		{
			onClick(s_Click);
		}
		return true;
	}

	// Caches time to avoid multiple calls per frame.
	public static void Update()
	{
		if (s_UpdateTime == Time.time)
		{
			return;
		}
		s_UpdateTime = Time.time;
		if (!Input.GetMouseButtonDown(0))
		{
			return;
		}
		Raycast();
		Screen();
	}
}
