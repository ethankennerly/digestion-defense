using NUnit.Framework;

namespace Finegamedesign.Entitas
{
    public sealed class TestReceiverUtils : TestGameContextWithId
    {
        [Test]
        public void Filter_IncludesConsumable()
        {
            GameEntity grape = m_Context.CreateEntity();
            grape.AddConsumable("Grape");

            ReceiverComponent consumableReceiver = new ReceiverComponent();
            int consumableIndex = GameComponentsLookup.Consumable;
            consumableReceiver.filterComponentIndexes.Add(consumableIndex);

            Assert.IsTrue(ReceiverUtils.Filter(consumableReceiver, grape),
                "Consumable");
        }

        /// 1. [ ] Candidate: none. Receiver: consumable. Filter excludes.

        /// 1. [ ] Candidate: consumable, glucose. Receiver: glucose. Filter includes.

        /// 1. [ ] Candidate: consumable. Receiver: glucose. Filter excludes.
    }
}
