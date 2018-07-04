using Entitas;
using Finegamedesign.Utils;
using System.Collections.Generic;

namespace Finegamedesign.Entitas
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
                GameMatcher.Trigger
            );
        }

        protected override bool Filter(GameEntity entity)
        {
            var trigger = entity.trigger;
            GameEntity target = m_Context.GetEntityWithId(trigger.targetId);
            if (target == null)
                return false;

            GameEntity source = m_Context.GetEntityWithId(trigger.sourceId);
            DebugUtil.Assert(source != null,
                "Filter expected trigger source was defined. " +
                "Trigger source ID=" + trigger.sourceId + " target=" + target);
            if (source == null)
                return false;

            DebugUtil.Assert(source.hasReceiver,
                "Filter expected trigger source has receiver. " +
                "Trigger source ID=" + source + " target=" + target);
            if (!source.hasReceiver)
                return false;

            return ReceiverUtils.Filter(source.receiver, target);
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity triggerEntity in entities)
                triggerEntity.isReaction = true;
        }
    }
}
