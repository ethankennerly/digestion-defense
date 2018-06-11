using Entitas.Unity;
using Finegamedesign.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace Finegamedesign.Entitas
{
    public static class GameLinkUtils
    {
        private static readonly Dictionary<int, GameObject> s_EntityIdToObjects = new Dictionary<int, GameObject>();

        /// <returns>
        /// Null if not linked.
        /// </returns>
        public static GameEntity GetEntity(GameObject linkedObject)
        {
            if (linkedObject == null)
                return null;

            EntityLink link = linkedObject.GetEntityLink();
            if (link == null || link.entity == null)
                return null;

            GameEntity entity = (GameEntity)link.entity;
            return entity;
        }

        /// <returns>
        /// -1 if not linked.
        /// </returns>
        public static int GetId(GameObject entityObject)
        {
            if (entityObject == null)
                return -1;

            GameEntity entity = GetEntity(entityObject);
            if (entity == null)
                return -1;

            return entity.id.value;
        }

        public static GameEntity[] GetEntitiesWithIds(GameContext context, int[] entityIds)
        {
            int numEntities = entityIds.Length;
            GameEntity[] entities = new GameEntity[numEntities];
            for (int index = 0; index < numEntities; ++index)
            {
                entities[index] = context.GetEntityWithId(entityIds[index]);
                DebugUtil.Assert(entities[index] != null,
                    "GameLinkUtils.GetEntitiesWithIds: Expected to find entity ID " + entityIds[index]);
            }
            return entities;
        }

        /// <returns>
        /// Null if game object doesn't exist or was destroyed.
        /// Returns previous entity if still linked.
        /// Otherwise, creates a new entity and links it.
        /// </returns>
        public static GameEntity TryLink(GameObject linkedObject)
        {
            if (linkedObject == null)
                return null;

            GameEntity entity = GetEntity(linkedObject);
            if (entity != null)
                return entity;

            var context = Contexts.sharedInstance.game;
            entity = (GameEntity)linkedObject.Link(context.CreateEntity(), context).entity;
            s_EntityIdToObjects[entity.id.value] = linkedObject;
            return entity;
        }

        public static GameObject GetObject(int entityId)
        {
            GameObject linkedObject = null;
            s_EntityIdToObjects.TryGetValue(entityId, out linkedObject);
            return linkedObject;
        }

        public static GameObject GetObject(GameEntity entity)
        {
            return GetObject(entity.id.value);
        }

        /// <summary>
        /// Parents child object to parent objects if they are both linked.
        /// </summary>
        /// <returns>
        /// If both were linked.
        /// </returns>
        public static bool TryAddChild(int parentEntityId, int childEntityId, bool silentNull = false)
        {
            GameObject parentObject = GetObject(parentEntityId);
            if (parentObject == null)
            {
                if (!silentNull && childEntityId != ReceiverComponent.kNone)
                    DebugUtil.Log("ReceiverComponentView.OnReceiver: Did you expect parent ID " + parentEntityId +
                        " to be linked to a game object? Child ID " + childEntityId);
                return false;
            }

            GameObject childObject = GetObject(childEntityId);
            if (childObject == null)
            {
                if (!silentNull && childEntityId != ReceiverComponent.kNone)
                    DebugUtil.Log("ReceiverComponentView.OnReceiver: Did you expect child ID " + childEntityId +
                        " to be linked to a game object? Parent ID " + parentEntityId);
                return false;
            }

            SceneNodeView.AddChild(parentObject, childObject);
            return true;
        }

        /// <summary>
        /// Links previous entity if still linked.
        /// Otherwise, creates a new entity and links it.
        /// </summary>
        ///
        /// <returns>
        /// Entity ID of each linked object.
        /// -1 if the object was destroyed.
        /// </returns>
        public static int TryLinkId(GameObject linkedObject)
        {
            GameEntity entity = TryLink(linkedObject);
            if (entity == null)
                return -1;

            return entity.id.value;
        }

        /// <summary>
        /// Links previous entity if still linked.
        /// Otherwise, creates a new entity and links it.
        /// </summary>
        ///
        /// <returns>
        /// Entity ID of each linked object.
        /// -1 if the object was destroyed.
        /// </returns>
        public static int[] TryLinkIds(GameObject[] linkedObjects)
        {
            if (linkedObjects == null)
                return null;

            int numObjects = linkedObjects.Length;
            int[] entityIds = new int[numObjects];
            for (int index = 0; index < numObjects; ++index)
            {
                entityIds[index] = TryLinkId(linkedObjects[index]);
            }

            return entityIds;
        }
    }
}
