using Finegamedesign.Utils;
using UnityEngine;

namespace Finegamedesign.Entitas
{
    public sealed class BeforeDestroyListener : IBeforeDestroyListener
    {
        private readonly GameContext m_Context;
        private int m_PublisherId = -1;

        public BeforeDestroyListener(GameContext context)
        {
            m_Context = context;
        }

        public void AddListener()
        {
            RemoveListener();

            GameEntity publisher = m_Context.GetEntityWithId(m_PublisherId);
            if (publisher == null)
            {
                publisher = m_Context.CreateEntity();
                m_PublisherId = publisher.id.value;
            }

            publisher.AddBeforeDestroyListener(this);
        }

        public void RemoveListener()
        {
            GameEntity publisher = m_Context.GetEntityWithId(m_PublisherId);
            if (publisher == null)
                return;

            if (!publisher.hasBeforeDestroyListener)
                return;

            publisher.RemoveBeforeDestroyListener(this);
        }

        public void OnBeforeDestroy(GameEntity entity)
        {
            GameObject entityObject = GameLinkUtils.GetObject(entity.id.value);
            DebugUtil.Assert(entityObject != null,
                "Expected link at before destroy entity=" + entity);
            if (entityObject == null)
            {
                return;
            }

            UnityEngine.Object.Destroy(entityObject);
        }
    }
}
