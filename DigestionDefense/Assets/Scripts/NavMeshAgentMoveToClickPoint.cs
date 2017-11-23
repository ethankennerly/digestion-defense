using UnityEngine;
using UnityEngine.AI;

// Adapted from
// https://docs.unity3d.com/Manual/nav-MoveToClickPoint.html
[RequireComponent(typeof(NavMeshAgent))]
public sealed class NavMeshAgentMoveToClickPoint : MonoBehaviour
{
    private NavMeshAgent m_Agent;

    private Vector3 m_ClickPoint;

    private void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (!ClickPoint.Raycast(out m_ClickPoint))
	{
		return;
	}
	m_Agent.destination = m_ClickPoint;
    }
}
