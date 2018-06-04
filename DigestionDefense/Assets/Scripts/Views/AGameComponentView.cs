using Entitas;
using Entitas.Unity;
using System;
using UnityEngine;

namespace Finegamedesign.Entitas
{
    public abstract class AGameComponentView<TComponent> : MonoBehaviour
        where TComponent : ACloneable, IComponent, new()
    {
        [SerializeField]
        protected TComponent m_Component;

        protected GameEntity m_Entity;

        private void Start()
        {
            Initialize();
        }

        private void OnDestroy()
        {
            TryUnlink();
        }

        protected virtual void Initialize()
        {
            TryLink();

            ReplaceComponent();
        }

        protected void TryLink()
        {
            if (m_Entity != null)
                return;

            m_Entity = GameLinkUtils.TryLink(gameObject);
        }

        protected void TryUnlink()
        {
            if (m_Entity == null)
                return;

            gameObject.Unlink();
        }

        protected void ReplaceComponent()
        {
            Debug.Assert(m_Component != null,
                "Component does not exist. Is " +
                    typeof(TComponent) + " component serializable?");

            TComponent clone;
            if (m_Component is ICloneable)
            {
                clone = (TComponent)m_Component.Clone();
            }
            else
            {
                clone = m_Component;
            }
            int componentIndex = Array.IndexOf(GameComponentsLookup.componentTypes, typeof(TComponent));
            m_Entity.ReplaceComponent(componentIndex, clone);
        }
    }
}
