using UnityEngine;
using UnityEngine.AI;

// Adapted from
// https://docs.unity3d.com/Manual/nav-MoveToClickPoint.html
[RequireComponent(typeof(NavMeshAgent))]
public sealed class NavMeshAgentMoveToClickPoint : MonoBehaviour
{
    private NavMeshAgent m_Agent;

    private void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        UpdateSetDestination();
    }

    private void UpdateSetDestination()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }
        RaycastHit hit;
        if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            return;
        }
        m_Agent.destination = hit.point;
    }
}
