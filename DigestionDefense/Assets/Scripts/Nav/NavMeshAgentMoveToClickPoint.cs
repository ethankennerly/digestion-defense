using FineGameDesign.Utils;
using UnityEngine;
using UnityEngine.AI;

// Adapted from
// https://docs.unity3d.com/Manual/nav-MoveToClickPoint.html
[RequireComponent(typeof(NavMeshAgent))]
public sealed class NavMeshAgentMoveToClickPoint : MonoBehaviour
{
    private NavMeshAgent m_Agent;

    private void OnEnable()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        ClickInputSystem.instance.onCollisionPoint += UpdateDestination;
    }

    private void OnDisable()
    {
        ClickInputSystem.instance.onCollisionPoint -= UpdateDestination;
    }

    private void Update()
    {
        ClickInputSystem.instance.Update();
    }

    private void UpdateDestination(Vector3 destination)
    {
        m_Agent.destination = destination;
    }
}
