using Entitas;
using System.Collections.Generic;

namespace FineGameDesign.Entitas
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
            if (!entity.hasTriggerEnter)
                return false;

            return entity.isDestinationLoopable;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity looper in entities)
            {
                var trigger = looper.triggerEnter;
                GameEntity other = m_Context.GetEntityWithId(trigger.otherId);
                var agent = looper.navAgent.agent;
                agent.loopingEnabled = other != null;
                if (!agent.loopingEnabled)
                    continue;

                agent.isLoopingPotentialDestinations = true;
            }
        }
    }
}
