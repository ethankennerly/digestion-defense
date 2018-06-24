using Entitas;
using Finegamedesign.Nav;
using Finegamedesign.Utils;
using System.Collections.Generic;

namespace Finegamedesign.Entitas
{
    public sealed class TriggerNavTargetSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext m_Context;

        public TriggerNavTargetSystem(Contexts contexts) : base(contexts.game)
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
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity triggerEntity in entities)
            {
                var trigger = triggerEntity.trigger;
                GameEntity attractor = m_Context.GetEntityWithId(trigger.sourceId);
                GameEntity traveler = m_Context.GetEntityWithId(trigger.targetId);
                if (traveler == null)
                    continue;

                if (!traveler.hasNavAgent)
                    traveler.AddNavAgent(new NavTilemapAgent());

                DebugUtil.LogError(this + ".Execute: TODO. attractor=" + attractor +
                    " traveler=" + traveler);
            }
        }
    }
}
