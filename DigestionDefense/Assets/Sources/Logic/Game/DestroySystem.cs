using Entitas;
using System.Collections.Generic;

namespace Finegamedesign.Entitas
{
    public sealed class DestroySystem : ReactiveSystem<GameEntity>
    {
        public DestroySystem(Contexts contexts) : base(contexts.game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                GameMatcher.BeforeDestroy.Added()
            );
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities)
            {
                entity.Destroy();
            }
        }
    }
}
