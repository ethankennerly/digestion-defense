using UnityEngine;

namespace Finegamedesign.Entitas
{
    /// <remarks>
    /// In preferences, set script execution order before default.
    /// Otherwise, a dependent script might race on enable.
    /// </remarks>
    public sealed class DigestionDefenseEntitasController : MonoBehaviour
    {
        private static EntitasController s_Entitas;

        private void OnEnable()
        {
            if (s_Entitas == null)
            {
                s_Entitas = new EntitasController(Contexts.sharedInstance,
                    new DigestionDefenseSystems(Contexts.sharedInstance));
            }

            s_Entitas.Initialize();
        }

        private void OnDisable()
        {
            s_Entitas.TearDown();
        }

        private void Update()
        {
            s_Entitas.Update();
        }
    }
}
