using Entitas;
using NUnit.Framework;

namespace Finegamedesign.Entitas
{
    public sealed class TestAccepterReactionSystem
    {
        private Contexts m_Contexts;
        private GameContext m_Context;

        [SetUp]
        public void SetUp()
        {
            m_Contexts = Contexts.sharedInstance;
            ContextUtils.Subscribe(m_Contexts, true);
            m_Context = m_Contexts.game;
        }

        [TearDown]
        public void TearDown()
        {
            ContextUtils.Subscribe(m_Contexts, false);
            m_Contexts = null;
            m_Context = null;
        }

        /// <summary>
        /// [ ] TODO
        /// 1. [ ] Example test steps:
        ///     1. [x] Target has receiver.
        ///     1. [ ] Receiver accepts health.
        ///     1. [ ] Occupant has health and quantity 1.
        ///     1. [ ] Target has trigger and accepter.
        ///     1. [ ] Source has receiver with health and quantity -2.
        ///     1. [ ] Source has transmitter from its receiver.
        ///     1. [ ] Publish trigger of source and target.
        ///     1. [ ] Execute trigger systems.
        ///     1. [ ] Expect target receiver occupant has health and quantity -1.
        /// <summary>
        [Test]
        public void AcceptsNegativeHealth()
        {
            var target = m_Context.CreateEntity();
            var receiver = new ReceiverComponent();
            target.AddComponent(GameComponentsLookup.Receiver, receiver);
            var source = m_Context.CreateEntity();
            source.AddTrigger(source.id.value, target.id.value);

            Assert.AreEqual("DONE", "TODO");
        }
    }
}
