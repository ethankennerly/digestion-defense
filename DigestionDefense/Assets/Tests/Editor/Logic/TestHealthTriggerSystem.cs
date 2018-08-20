using Entitas;
using NUnit.Framework;

namespace FineGameDesign.Entitas
{
    public sealed class TestHealthTriggerSystem : TestGameContextWithId
    {
        [Test]
        public void Execute_NegativeHealthChanger()
        {
            var system = new HealthTriggerSystem(m_Contexts);

            GameEntity grape = m_Context.CreateEntity();
            grape.AddHealth(1);

            GameEntity tooth = m_Context.CreateEntity();
            tooth.AddHealthChanger(-3);

            grape.ReplaceTriggerEnter(tooth.id.value);

            system.Execute();
            Assert.AreEqual(grape.health.value, -2, "Tooth triggers grape has effect on grape health.");
            Assert.IsFalse(tooth.hasHealth, "Tooth triggers grape has no effect on tooth health.");

            tooth.ReplaceTriggerEnter(grape.id.value);

            system.Execute();
            Assert.AreEqual(grape.health.value, -5, "After execute grape triggers tooth.");
            Assert.IsFalse(tooth.hasHealth, "Grape triggers tooth has no effect on tooth health.");
        }
    }
}
