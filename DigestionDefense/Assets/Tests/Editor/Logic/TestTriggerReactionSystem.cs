using Entitas;
using NUnit.Framework;

namespace FineGameDesign.Entitas
{
    public sealed class TestTriggerReactionSystem : TestGameContextWithId
    {
        [Test]
        public void ReplaceReaction_CollectsSecondTime()
        {
            ICollector<GameEntity> collector = m_Context.CreateCollector(
                GameMatcher.Reaction.Added());

            GameEntity attractor = m_Context.CreateEntity();
            Assert.IsFalse(collector.collectedEntities.Contains(attractor), "Before first time");

            TriggerReactionSystem.ReplaceReaction(attractor, true);
            Assert.IsTrue(collector.collectedEntities.Contains(attractor), "First time");

            collector.ClearCollectedEntities();
            TriggerReactionSystem.ReplaceReaction(attractor, true);
            Assert.IsTrue(collector.collectedEntities.Contains(attractor), "Second time");

            collector.ClearCollectedEntities();
            TriggerReactionSystem.ReplaceReaction(attractor, false);
            Assert.IsFalse(collector.collectedEntities.Contains(attractor), "Removed but not added.");
        }
    }
}
