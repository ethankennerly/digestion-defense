using Entitas;
using UnityEngine;

namespace Finegamedesign.Entitas
{
    /// <remarks>
    /// In preferences, set script execution order before default.
    /// Otherwise, a dependent script might race on enable.
    /// </remarks>
    public sealed class EntitasController : MonoBehaviour
    {
        private static Systems s_Systems;

        private static Contexts s_Contexts;

        private void OnEnable()
        {
            if (s_Contexts == null)
            {
                s_Contexts = Contexts.sharedInstance;
                ContextUtils.Subscribe(s_Contexts, true);
            }

            if (s_Systems == null)
                s_Systems = new PetriGameSystems(s_Contexts);

            s_Systems.Initialize();
        }

        private void OnDisable()
        {
            if (s_Systems != null)
                s_Systems.TearDown();
        }

        private void Update()
        {
            if (s_Systems == null)
                return;

            s_Systems.Execute();
            s_Systems.Cleanup();
        }
    }
}
