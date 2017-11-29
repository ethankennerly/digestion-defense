using Entitas;
using UnityEngine;

public sealed class GameController : MonoBehaviour
{
    private Systems m_Systems;

    private void Start()
    {
        // get a reference to the contexts
        var contexts = Contexts.sharedInstance;

        // create the systems by creating individual features
        m_Systems = new Feature("Systems")
            .Add(new ClickPointInputSystem(contexts));

        // call Initialize() on all of the IInitializeSystems
        m_Systems.Initialize();
    }

    private void Update()
    {
        // call Execute() on all the IExecuteSystems and
        // ReactiveSystems that were triggered last frame
        m_Systems.Execute();
        // call cleanup() on all the ICleanupSystems
        m_Systems.Cleanup();
    }
}
