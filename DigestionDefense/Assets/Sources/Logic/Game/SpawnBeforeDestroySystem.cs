using Entitas;
using System.Collections.Generic;

namespace Finegamedesign.Entitas
{
    public sealed class SpawnBeforeDestroySystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext m_Context;

        public SpawnBeforeDestroySystem(Contexts contexts) : base(contexts.game)
        {
            m_Context = contexts.game;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                GameMatcher.Id.Added()
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
                m_Context.CreateEntity();
            }
        }
    }
}
