using Entitas.Unity;
using UnityEngine;

namespace Finegamedesign.Entitas
{
    public static class GameLinkUtils
    {
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
    }
}
