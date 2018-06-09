using Entitas;
using UnityEngine;

namespace Finegamedesign.Entitas
{
    public sealed class EntitasController : MonoBehaviour
    {
        private Systems m_Systems;

        private void Awake()
        {
            // get a reference to the contexts
            var contexts = Contexts.sharedInstance;

            ContextUtils.Subscribe(contexts, true);

            // create the systems by creating individual features
            m_Systems = new PetriGameSystems(contexts);
                // .Add(new ClickPointInputSystem(contexts));

            // call Initialize() on all of the IInitializeSystems
            m_Systems.Initialize();
        }

        private void Update()
        {
            if (m_Systems == null)
                return;

            // call Execute() on all the IExecuteSystems and
            // ReactiveSystems that were triggered last frame
            m_Systems.Execute();
            // call cleanup() on all the ICleanupSystems
            m_Systems.Cleanup();
        }
    }
}
