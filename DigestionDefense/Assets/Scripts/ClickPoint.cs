using UnityEngine;

public static class ClickPoint
{
    public static bool Raycast(out Vector3 point)
    {
        point = default(Vector3);
        if (!Input.GetMouseButtonDown(0))
        {
            return false;
        }
        RaycastHit hit;
        if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            return false;
        }
        point = hit.point;
	return true;
    }
}
