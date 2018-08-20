using Entitas;
using System.Collections.Generic;

namespace FineGameDesign.Entitas
{
    public sealed class SpawnBeforeDestroySystem : ReactiveSystem<GameEntity>
    {
        public SpawnBeforeDestroySystem(Contexts contexts) : base(contexts.game)
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
            return entity.hasSpawnBeforeDestroy;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities)
            {
                entity.ReplaceSpawn(entity.spawnBeforeDestroy.prefab);
            }
        }
    }
}
