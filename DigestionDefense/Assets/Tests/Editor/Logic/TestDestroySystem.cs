using Entitas;
using NUnit.Framework;

namespace Finegamedesign.Entitas
{
    public sealed class TestDestroySystem : TestGameContextWithId
    {
        [Test]
        public void Execute_BeforeDestroy_IsNotEnabled()
        {
            var system = new DestroySystem(m_Contexts);

            GameEntity grape = m_Context.CreateEntity();

            Assert.IsTrue(grape.isEnabled);
            system.Execute();
            Assert.IsTrue(grape.isEnabled);
            grape.isBeforeDestroy = true;
            system.Execute();
            Assert.IsFalse(grape.isEnabled);
        }
    }
}
