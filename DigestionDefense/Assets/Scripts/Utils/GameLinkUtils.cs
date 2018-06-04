using Entitas.Unity;
using UnityEngine;

namespace Finegamedesign.Entitas
{
    public static class GameLinkUtils
    {
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

            GameEntity entity = GameLinkUtils.GetEntity(entityObject);
            if (entity == null)
                return -1;

            return entity.id.value;
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
            return (GameEntity)linkedObject.Link(context.CreateEntity(), context).entity;
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
