using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestEnemyWorkerManager
    {
        [Test]
        public void TestIncreseWoodWorkers()
        {
            GameObject placeholder = new GameObject();
            EnemyWorkerManager ewm = placeholder.AddComponent<EnemyWorkerManager>();
            ewm.WoodHutBuild();

            int count = ewm.maxWoodWorkers;

            Assert.AreEqual(1, count);

        }
        [Test]
        public void TestIncreaseStoneWorkers()
        {
            GameObject placeholder = new GameObject();
            EnemyWorkerManager ewm = placeholder.AddComponent<EnemyWorkerManager>();
            ewm.StoneHutBuild();

            int count = ewm.maxStoneWorkers;

            Assert.AreEqual(1, count);
        }
        [Test]
        public void TestIncreaseMetalWorkers()
        {
            GameObject placeholder = new GameObject();
            EnemyWorkerManager ewm = placeholder.AddComponent<EnemyWorkerManager>();
            ewm.MetalHutBuild();

            int count = ewm.maxMetalWorkers;

            Assert.AreEqual(1, count);
        }
        [Test]
        public void TestDecreaseWoodWorkers()
        {
            GameObject placeholder = new GameObject();
            EnemyWorkerManager ewm = placeholder.AddComponent<EnemyWorkerManager>();
            ewm.WoodHutBuild();
            ewm.WoodHutBuild();
            ewm.woodHutDestroyed();
            int count = ewm.maxWoodWorkers;

            Assert.AreEqual(1, count);
        }
        [Test]
        public void TestDecreaseStoneWorkers()
        {
            GameObject placeholder = new GameObject();
            EnemyWorkerManager ewm = placeholder.AddComponent<EnemyWorkerManager>();
            ewm.StoneHutBuild();
            ewm.StoneHutBuild();
            ewm.StoneHutDestroyed();
            int count = ewm.maxStoneWorkers;

            Assert.AreEqual(1, count);
        }
        [Test]
        public void TestDecreaseMetalWorkers()
        {
            GameObject placeholder = new GameObject();
            EnemyWorkerManager ewm = placeholder.AddComponent<EnemyWorkerManager>();
            ewm.MetalHutBuild();
            ewm.MetalHutBuild();
            ewm.MetalHutDestroyed();
            int count = ewm.maxMetalWorkers;

            Assert.AreEqual(1, count);
        }
        [Test]
        public void TestAllowWoodWorker()
        {
            GameObject placeholder = new GameObject();
            EnemyWorkerManager ewm = placeholder.AddComponent<EnemyWorkerManager>();
            ewm.WoodHutBuild();
 

            Assert.True(ewm.AllowWorker(ResourceType.Wood));
        }
        [Test]
        public void TestAllowStoneWorker()
        {
            GameObject placeholder = new GameObject();
            EnemyWorkerManager ewm = placeholder.AddComponent<EnemyWorkerManager>();
            ewm.StoneHutBuild();


            Assert.True(ewm.AllowWorker(ResourceType.Stone));
        }
        [Test]
        public void TestAllowMetalWorker()
        {
            GameObject placeholder = new GameObject();
            EnemyWorkerManager ewm = placeholder.AddComponent<EnemyWorkerManager>();
            ewm.MetalHutBuild();


            Assert.True(ewm.AllowWorker(ResourceType.Metal));
        }
        [Test]
        public void TestNotAllowWoodWorker()
        {
            GameObject placeholder = new GameObject();
            EnemyWorkerManager ewm = placeholder.AddComponent<EnemyWorkerManager>();

            Assert.False(ewm.AllowWorker(ResourceType.Wood));
        }
        [Test]
        public void TestNotAllowStoneWorker()
        {
            GameObject placeholder = new GameObject();
            EnemyWorkerManager ewm = placeholder.AddComponent<EnemyWorkerManager>();

            Assert.False(ewm.AllowWorker(ResourceType.Stone));
        }
        [Test]
        public void TestNotAllowMetalWorker()
        {
            GameObject placeholder = new GameObject();
            EnemyWorkerManager ewm = placeholder.AddComponent<EnemyWorkerManager>();

            Assert.False(ewm.AllowWorker(ResourceType.Metal));
        }
    }
}
