using Finegamedesign.Entitas;
using UnityEngine;

namespace Finegamedesign.DigestionDefense
{
    /// <remarks>
    /// In preferences, set script execution order before default.
    /// Otherwise, a dependent script might race on enable.
    /// </remarks>
    public sealed class DigestionDefenseEntitasController : MonoBehaviour
    {
        private static EntitasController s_Entitas;

        private static Contexts s_Contexts;

        private static SpawnListener s_SpawnListener;

        private void OnEnable()
        {
            if (s_Entitas == null)
            {
                s_Contexts = Contexts.sharedInstance;
                s_SpawnListener = new SpawnListener(s_Contexts.game);
                s_Entitas = new EntitasController(s_Contexts,
                    new DigestionDefenseSystems(s_Contexts));
            }

            s_Entitas.Initialize();

            s_SpawnListener.AddListener();
        }

        private void OnDisable()
        {
            s_SpawnListener.RemoveListener();

            s_Entitas.TearDown();
        }

        private void Update()
        {
            s_Entitas.Update();
        }
    }
}
