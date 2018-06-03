using UnityEngine;

namespace Finegamedesign.Entitas
{
    public abstract class AComponentView<TComponent> : MonoBehaviour
        where TComponent : new()
    {
        [SerializeField]
        private TComponent m_Component;
    }
}
