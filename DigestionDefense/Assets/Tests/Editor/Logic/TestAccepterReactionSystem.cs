using Entitas;
using NUnit.Framework;

namespace Finegamedesign.Entitas
{
    public sealed class TestAccepterReactionSystem
    {
        /// <summary>
        /// [ ] TODO
        /// 1. [ ] Example test steps:
        ///     1. [ ] Receiver accepts health.
        ///     1. [ ] Occupant has health and quantity 1.
        ///     1. [ ] Target has receiver.
        ///     1. [ ] Target has trigger and accepter.
        ///     1. [ ] Source has receiver with health and quantity -2.
        ///     1. [ ] Publish trigger of source and target.
        ///     1. [ ] Execute trigger systems.
        ///     1. [ ] Expect target receiver occupant has health and quantity -1.
        /// <summary>
        [Test]
        public void AcceptsNegativeHealth()
        {
            GameContext context = Contexts.sharedInstance.game;
            var target = context.CreateEntity();
            var receiver = new ReceiverComponent();
            target.AddComponent(GameComponentsLookup.Receiver, receiver);

            Assert.AreEqual("DONE", "TODO");
        }
    }
}
