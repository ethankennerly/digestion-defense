using Entitas;
using NUnit.Framework;

namespace Finegamedesign.Entitas
{
    public sealed class TestHealthEmptySpawnSystem : TestGameContextWithId
    {
        [Test]
        public void TODO_Execute_BeforeDestroy()
        {
            /*
            var system = new HealthEmptySpawnSystem(m_Contexts);

            GameEntity grape = m_Context.CreateEntity();
            grape.AddHealth(0);

            system.Execute();
            Assert.IsTrue(grape.isBeforeDestroy);
             */
            Assert.AreEqual("Done", "TODO");
        }

        [Test]
        public void TODO_Execute_SpawnPrefab()
        {
            /*
            var system = new HealthEmptySpawnSystem(m_Contexts);

            GameObject sucrosePrefab = new UnityEngine.GameObject();
            sucrosePrefab.name = "Sucrose";
            GameEntity grape = m_Context.CreateEntity();
            grape.AddDestroySpawner(sucrosePrefab);
            grape.AddHealth(0);

            system.Execute();
            Assert.IsTrue(grape.hasSpawn);
            Assert.AreEqual(sucrosePrefab, grape.spawn.prefab);
             */
            Assert.AreEqual("Done", "TODO");
        }
    }
}
