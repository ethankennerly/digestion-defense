using Entitas;
using FineGameDesign.Utils;
using System.Collections.Generic;

namespace FineGameDesign.Entitas
{
    public sealed class TriggerReactionSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext m_Context;

        public TriggerReactionSystem(Contexts contexts) : base(contexts.game)
        {
            m_Context = contexts.game;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                GameMatcher.TriggerEnter.Added()
            );
        }

        protected override bool Filter(GameEntity entity)
        {
            var trigger = entity.triggerEnter;
            GameEntity other = m_Context.GetEntityWithId(trigger.otherId);
            if (other == null)
                return false;

            if (!entity.hasReceiver)
                return false;

            return ReceiverUtils.Filter(entity.receiver, other);
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity triggerEntity in entities)
            {
                ReplaceReaction(triggerEntity, true);
            }
        }

        public static void ReplaceReaction(GameEntity entity, bool isReaction)
        {
            entity.isReaction = !isReaction;
            entity.isReaction = isReaction;
        }
    }
}
