using Entitas;
using System.Collections.Generic;

namespace FineGameDesign.Entitas
{
    public sealed class HealthEmptyDestroySystem : ReactiveSystem<GameEntity>
    {
        public HealthEmptyDestroySystem(Contexts contexts) : base(contexts.game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                GameMatcher.Health.Added()
            );
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.health.value <= 0 &&
                !entity.isBeforeDestroy;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities)
            {
                entity.isBeforeDestroy = true;
            }
        }
    }
}
