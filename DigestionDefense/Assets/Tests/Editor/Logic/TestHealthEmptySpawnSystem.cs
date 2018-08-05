using Entitas;
using NUnit.Framework;

namespace Finegamedesign.Entitas
{
    public sealed class TestHealthEmptyDestroySystem : TestGameContextWithId
    {
        [Test]
        public void TODO_Execute_BeforeDestroy()
        {
            var system = new HealthEmptyDestroySystem(m_Contexts);

            GameEntity grape = m_Context.CreateEntity();
            grape.AddHealth(0);

            system.Execute();
            Assert.IsTrue(grape.isBeforeDestroy);
        }
    }
}
