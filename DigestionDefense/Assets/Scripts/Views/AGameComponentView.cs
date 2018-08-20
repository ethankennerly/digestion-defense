using Entitas;
using Entitas.Unity;
using System;
using UnityEngine;

namespace FineGameDesign.Entitas
{
    /// <remarks>
    /// Abstract game component view is convenient to wire properties in the Unity editor.
    /// The tradeoff is that during runtime, like any MonoBehaviour,
    /// this behaviour adds loading overhead per object.
    /// </remarks>
    public abstract class AGameComponentView<TComponent> : MonoBehaviour
        where TComponent : IComponent, new()
    {
        [SerializeField]
        protected TComponent m_Component = new TComponent();

        private int m_ComponentIndex = Array.IndexOf(GameComponentsLookup.componentTypes, typeof(TComponent));

        public TComponent Component
        {
            get { return m_Component; }
        }

        protected GameEntity m_Entity;

        protected bool m_ReplaceComponentOnInitialize = true;

        private EntityEvent m_OnDestroyEntity;

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

            if (m_OnDestroyEntity == null)
                m_OnDestroyEntity = DestroyObject;

            m_Entity.OnDestroyEntity -= m_OnDestroyEntity;
            m_Entity.OnDestroyEntity += m_OnDestroyEntity;
        }

        protected void TryUnlink()
        {
            if (m_Entity == null)
                return;

            m_Entity.OnDestroyEntity -= m_OnDestroyEntity;
            m_Entity = null;

            var link = gameObject.GetEntityLink();
            if (link == null || link.entity == null)
                return;

            gameObject.Unlink();
        }

        protected virtual void DestroyObject(IEntity entity)
        {
            if (gameObject == null)
                return;

            UnityEngine.Object.Destroy(gameObject);
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
            m_Entity.ReplaceComponent(m_ComponentIndex, clone);
        }

        protected void TryRemoveComponent()
        {
            if (!m_Entity.HasComponent(m_ComponentIndex))
                return;

            m_Entity.RemoveComponent(m_ComponentIndex);
        }
    }
}
