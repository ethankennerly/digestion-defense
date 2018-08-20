using NUnit.Framework;

namespace FineGameDesign.Entitas
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

            Assert.IsTrue(ReceiverUtils.Filter(consumableReceiver, grape));
        }

        [Test]
        public void Filter_ExcludesNonConsumable()
        {
            GameEntity mountain = m_Context.CreateEntity();

            ReceiverComponent consumableReceiver = new ReceiverComponent();
            int consumableIndex = GameComponentsLookup.Consumable;
            consumableReceiver.filterComponentIndexes.Add(consumableIndex);

            Assert.IsFalse(ReceiverUtils.Filter(consumableReceiver, mountain));
        }

        [Test]
        public void Filter_IncludesGlucose()
        {
            GameEntity glucose = m_Context.CreateEntity();
            glucose.AddConsumable("Glucose");
            glucose.isGlucose = true;

            ReceiverComponent glucoseReceiver = new ReceiverComponent();
            int glucoseIndex = GameComponentsLookup.Glucose;
            glucoseReceiver.filterComponentIndexes.Add(glucoseIndex);

            Assert.IsTrue(ReceiverUtils.Filter(glucoseReceiver, glucose));
        }

        [Test]
        public void Filter_ExcludesNonGlucoseConsumable()
        {
            GameEntity grape = m_Context.CreateEntity();
            grape.AddConsumable("Grape");

            ReceiverComponent glucoseReceiver = new ReceiverComponent();
            int glucoseIndex = GameComponentsLookup.Glucose;
            glucoseReceiver.filterComponentIndexes.Add(glucoseIndex);

            Assert.IsFalse(ReceiverUtils.Filter(glucoseReceiver, grape));
        }
    }
}
