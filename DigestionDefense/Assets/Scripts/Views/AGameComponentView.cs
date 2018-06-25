using Entitas;
using Entitas.Unity;
using System;
using UnityEngine;

namespace Finegamedesign.Entitas
{
    public abstract class AGameComponentView<TComponent> : MonoBehaviour
        where TComponent : IComponent, new()
    {
        [SerializeField]
        protected TComponent m_Component = new TComponent();

        public TComponent Component
        {
            get { return m_Component; }
        }

        protected GameEntity m_Entity;

        protected bool m_ReplaceComponentOnInitialize = true;

        protected virtual void OnEnable()
        {
            Initialize();
        }

        protected virtual void OnDisable()
        {
            TryUnlink();
        }

        public virtual void Initialize()
        {
            TryLink();

            if (m_ReplaceComponentOnInitialize)
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

            m_Entity = null;

            var link = gameObject.GetEntityLink();
            if (link == null || link.entity == null)
                return;

            gameObject.Unlink();
        }

        /// <summary>
        /// If component is not cloneable, then it is the component's responsibility to refresh
        /// values that depend on entity ID.
        /// Otherwise a recycled entity or view may associate stale values from a previous incarnation.
        /// </summary>
        protected void ReplaceComponent()
        {
            Debug.Assert(m_Component != null,
                "Component does not exist. Is " +
                    typeof(TComponent) + " component serializable?");

            TComponent clone;
            if (m_Component is ICloneable)
            {
                clone = (TComponent)((ICloneable)m_Component).Clone();
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
