using Entitas;
using NUnit.Framework;
using UnityEngine;

namespace FineGameDesign.Entitas
{
    public sealed class TestSpawnBeforeDestroySystem : TestGameContextWithId
    {
        [Test]
        public void Execute_SpawnPrefab()
        {
            var system = new SpawnBeforeDestroySystem(m_Contexts);

            var sucrosePrefab = new GameObject();
            sucrosePrefab.name = "Sucrose";
            GameEntity grape = m_Context.CreateEntity();
            grape.AddSpawnBeforeDestroy(sucrosePrefab);

            system.Execute();
            Assert.IsFalse(grape.hasSpawn);

            grape.isBeforeDestroy = true;
            system.Execute();
            Assert.IsTrue(grape.hasSpawn);
            Assert.AreEqual(sucrosePrefab, grape.spawn.prefab);
        }
    }
}
