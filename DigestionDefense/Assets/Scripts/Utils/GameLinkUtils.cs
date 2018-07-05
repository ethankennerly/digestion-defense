using Entitas.Unity;
using Finegamedesign.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace Finegamedesign.Entitas
{
    public static class GameLinkUtils
    {
        public const int kNone = -1;

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
                return kNone;

            GameEntity entity = GetEntity(entityObject);
            if (entity == null)
                return kNone;

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
        /// <remarks>
        /// MonoBehaviours are slow to get/add/destroy.
        /// So if many objects are linked, a faster approach is a dictionary of game objects to entities.
        /// </remarks>
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
        /// Parents child object to parent objects if both entities are linked to game objects.
        /// </summary>
        /// <returns>
        /// If parent and child are linked to game objects.
        /// </returns>
        public static bool TryAddChild(int parentEntityId, int childEntityId,
            bool silentIfChildIsUnlinked = false)
        {
            GameObject parentObject = GetObject(parentEntityId);
            DebugUtil.Assert(parentObject != null,
                "GameLinkUtils.TryAddChild: Did you expect parent ID " + parentEntityId +
                " to be linked to a game object? Child ID " + childEntityId);
            if (parentObject == null)
                return false;

            GameObject childObject = GetObject(childEntityId);
            if (!silentIfChildIsUnlinked)
                DebugUtil.Assert(childObject != null,
                    "GameLinkUtils.TryAddChild: Did you expect child ID " + childEntityId +
                    " to be linked to a game object? Parent ID " + parentEntityId);
            if (childObject == null)
                return false;

            SceneNodeView.AddChild(parentObject, childObject);
            return true;
        }

        /// <summary>
        /// Parents each child object to the parent object if the parent and child are linked to game objects.
        /// </summary>
        /// <returns>
        /// If parent and all children are linked to game objects.
        /// </returns>
        public static bool TryAddChildren(int parentEntityId, int[] childEntityIds,
            bool silentIfChildIsUnlinked = false)
        {
            bool areAllLinked = true;
            foreach (int childEntityId in childEntityIds)
            {
                if (childEntityId == kNone)
                    continue;

                if (!TryAddChild(parentEntityId, childEntityId, silentIfChildIsUnlinked))
                    areAllLinked = false;
            }

            return areAllLinked;
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
                return kNone;

            return entity.id.value;
        }

        /// <summary>
        /// Links previous entity if still linked.
        /// Otherwise, creates a new entity and links it.
        /// </summary>
        ///
        /// <returns>
        /// Entity ID of each linked object.
        /// -1 instead of entity ID if the object was destroyed.
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
