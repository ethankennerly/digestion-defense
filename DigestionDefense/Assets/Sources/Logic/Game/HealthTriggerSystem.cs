using Entitas;
using System.Collections.Generic;

namespace Finegamedesign.Entitas
{
    public sealed class HealthTriggerSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext m_Context;

        public HealthTriggerSystem(Contexts contexts) : base(contexts.game)
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
            GameEntity other = m_Context.GetEntityWithId(entity.triggerEnter.otherId);
            if (other == null)
                return false;

            if (entity.hasHealth && other.hasHealthChanger)
                return true;

            if (other.hasHealth && entity.hasHealthChanger)
                return true;

            return false;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities)
            {
            }
        }
    }
}
