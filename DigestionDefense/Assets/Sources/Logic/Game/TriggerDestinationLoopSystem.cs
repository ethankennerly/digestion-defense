using Entitas;
using System.Collections.Generic;

namespace Finegamedesign.Entitas
{
    public sealed class TriggerDestinationLoopSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext m_Context;

        public TriggerDestinationLoopSystem(Contexts contexts) : base(contexts.game)
        {
            m_Context = contexts.game;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                GameMatcher.Reaction.Added()
            );
        }

        protected override bool Filter(GameEntity entity)
        {
            if (!entity.hasTrigger)
                return false;

            GameEntity looper = m_Context.GetEntityWithId(entity.trigger.sourceId);
            return looper.isDestinationLoopable;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity triggerEntity in entities)
            {
                var trigger = triggerEntity.trigger;
                GameEntity looper = m_Context.GetEntityWithId(trigger.sourceId);

                looper.navAgent.agent.isLoopingPotentialDestinations = true;
            }
        }
    }
}
