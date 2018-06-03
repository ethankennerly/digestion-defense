using Entitas;
using System;

namespace Finegamedesign.Entitas
{
    public static class ContextUtils
    {
        public static void Subscribe(bool isSubscribing)
        {
            Subscribe(typeof(IdComponent), ReplaceId, isSubscribing);
        }

        private static void ReplaceId(IContext context, IEntity entity)
        {
            IIdEntity idEntity = (IIdEntity)entity;
            idEntity.ReplaceId(entity.creationIndex);
        }

        /// <summary>
        /// Guards against double-listening
        /// Copied from:
        /// https://github.com/sschmid/Entitas-CSharp/wiki/FAQ#q-should-i-store-references-to-entities-inside-components
        /// </summary>
        public static void Subscribe(Type componentType, ContextEntityChanged onEntityCreated, bool isSubscribing)
        {
            Contexts contexts = Contexts.sharedInstance;
            foreach (var context in contexts.allContexts)
            {
                Type[] types = context.contextInfo.componentTypes;
                if (Array.IndexOf(types, componentType) < 0)
                {
                    continue;
                }
                if (isSubscribing)
                {
                    context.OnEntityCreated -= onEntityCreated;
                    context.OnEntityCreated += onEntityCreated;
                }
                else
                {
                    context.OnEntityCreated -= onEntityCreated;
                }
            }
        }
    }
}
