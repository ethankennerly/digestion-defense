using FineGameDesign.Utils;
using UnityEngine;

namespace FineGameDesign.Entitas
{
    public sealed class SpawnListener : ISpawnListener
    {
        private readonly GameContext m_Context;
        private int m_PublisherId = -1;

        public SpawnListener(GameContext context)
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

            publisher.AddSpawnListener(this);
        }

        public void RemoveListener()
        {
            GameEntity publisher = m_Context.GetEntityWithId(m_PublisherId);
            if (publisher == null)
                return;

            if (!publisher.hasSpawnListener)
                return;

            publisher.RemoveSpawnListener(this);
        }

        public void OnSpawn(GameEntity entity, GameObject prefab)
        {
            GameObject origin = GameLinkUtils.GetObject(entity.id.value);
            DebugUtil.Assert(origin != null,
                "Expected link at spawn entity=" + entity + ". Spawning at world origin. prefab=" + prefab);
            if (origin == null)
            {
                UnityEngine.Object.Instantiate(prefab);
                return;
            }

            GameObject clone = UnityEngine.Object.Instantiate(prefab, origin.transform);
            clone.transform.SetParent(null, true);
        }
    }
}
