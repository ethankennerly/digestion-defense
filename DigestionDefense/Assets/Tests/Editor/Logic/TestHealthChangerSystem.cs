using Entitas;
using NUnit.Framework;

namespace Finegamedesign.Entitas
{
    public sealed class TestHealthChangerSystem : TestGameContextWithId
    {
        [Test]
        public void TODO_Execute_NegativeHealth()
        {
            // var changer = new HealthChangerSystem(m_Contexts);

            GameEntity grape = m_Context.CreateEntity();
            // grape.AddHealth(1);

            GameEntity tooth = m_Context.CreateEntity();
            // tooth.AddHealthChanger(-3);

            // grape.ReplaceTriggerEnter(tooth.id.value);

            // changer.Execute();
            // Assert.AreEqual(grape.health.value, -2, "Tooth triggers grape has effect on grape health.");
            // Assert.IsFalse(tooth.hasHealth, "Tooth triggers grape has no effect on tooth health.");

            // tooth.ReplaceTriggerEnter(grape.id.value);

            // changer.Execute();
            // Assert.AreEqual(grape.health.value, -5, "After execute grape triggers tooth.");
            // Assert.IsFalse(tooth.hasHealth, "Grape triggers tooth has no effect on tooth health.");

            Assert.AreEqual("Done", "TODO");
        }
    }
}
