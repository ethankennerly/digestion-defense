using Entitas;
using System.Collections.Generic;

namespace Finegamedesign.Entitas
{
    public sealed class TriggerExitLoopingDisabledSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext m_Context;

        public TriggerExitLoopingDisabledSystem(Contexts contexts) : base(contexts.game)
        {
            m_Context = contexts.game;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                GameMatcher.TriggerExit.Added()
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
                var agent = looper.navAgent.agent;
                agent.loopingEnabled = false;
            }
        }
    }
}
