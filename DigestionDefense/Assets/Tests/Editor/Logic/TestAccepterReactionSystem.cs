using Entitas;
using NUnit.Framework;

namespace Finegamedesign.Entitas
{
    public sealed class TestAccepterReactionSystem
    {
        private Contexts m_Contexts;
        private GameContext m_Context;

        [SetUp]
        public void SetUpId()
        {
            m_Contexts = Contexts.sharedInstance;
            ContextUtils.Subscribe(m_Contexts, true);
            m_Context = m_Contexts.game;
        }

        [TearDown]
        public void TearDownId()
        {
            ContextUtils.Subscribe(m_Contexts, false);
            m_Contexts = null;
            m_Context = null;
        }

        /// <summary>
        /// [ ] TODO
        /// 1. [ ] Example test steps:
        ///     1. [x] Target has receiver.
        ///     1. [x] Receiver accepts health.
        ///     1. [x] Occupant has health and quantity 1.
        ///     1. [x] Target has trigger and accepter.
        ///     1. [ ] Source has receiver with health and quantity -2.
        ///     1. [ ] Source has transmitter from its receiver.
        ///     1. [x] Publish trigger of source and target.
        ///     1. [ ] Execute trigger systems.
        ///     1. [ ] Expect target receiver occupant has health and quantity -1.
        /// <summary>
        [Test]
        public void TODO_AcceptsNegativeHealth()
        {
            var healthMeter = m_Context.CreateEntity();
            healthMeter.isHealth = true;
            healthMeter.AddQuantity(1);

            var receiver = new ReceiverComponent();
            receiver.filterComponentIndexes.Add(GameComponentsLookup.Health);
            receiver.occupantIds[0] = healthMeter.id.value;

            var target = m_Context.CreateEntity();
            target.AddComponent(GameComponentsLookup.Receiver, receiver);
            target.isAccepter = true;

            var missile = m_Context.CreateEntity();
            missile.isHealth = true;
            missile.AddQuantity(-2);

            var thrower = new ReceiverComponent();
            thrower.filterComponentIndexes.Add(GameComponentsLookup.Health);
            thrower.occupantIds[0] = missile.id.value;

            var attacker = m_Context.CreateEntity();
            attacker.AddComponent(GameComponentsLookup.Receiver, thrower);

            var transmitter = new TransmitterComponent();
            transmitter.inputIds = new int[]{attacker.id.value};
            attacker.AddComponent(GameComponentsLookup.Transmitter, transmitter);

            target.AddTrigger(attacker.id.value, target.id.value);

            // TODO: Execute system.

            Assert.AreEqual(-1, healthMeter.quantity.value, "health meter");
        }
    }
}
