using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestPlayerWorkerManager
    {
        [Test]
        public void TestIncreseWoodWorkers()
        {
            GameObject placeholder = new GameObject();
            WorkerManager wm = placeholder.AddComponent<WorkerManager>();
            wm.WoodHutBuild();

            int count = wm.maxWoodWorkers;

            Assert.AreEqual(1, count);

        }
        [Test]
        public void TestIncreaseStoneWorkers()
        {
            GameObject placeholder = new GameObject();
            WorkerManager wm = placeholder.AddComponent<WorkerManager>();
            wm.StoneHutBuild();

            int count = wm.maxStoneWorkers;

            Assert.AreEqual(1, count);
        }
        [Test]
        public void TestIncreaseMetalWorkers()
        {
            GameObject placeholder = new GameObject();
            WorkerManager wm = placeholder.AddComponent<WorkerManager>();
            wm.MetalHutBuild();

            int count = wm.maxMetalWorkers;

            Assert.AreEqual(1, count);
        }
        [Test]
        public void TestDecreaseWoodWorkers()
        {
            GameObject placeholder = new GameObject();
            WorkerManager wm = placeholder.AddComponent<WorkerManager>();
            wm.WoodHutBuild();
            wm.WoodHutBuild();
            wm.woodHutDestroyed();
            int count = wm.maxWoodWorkers;

            Assert.AreEqual(1, count);
        }
        [Test]
        public void TestDecreaseStoneWorkers()
        {
            GameObject placeholder = new GameObject();
            WorkerManager wm = placeholder.AddComponent<WorkerManager>();
            wm.StoneHutBuild();
            wm.StoneHutBuild();
            wm.StoneHutDestroyed();
            int count = wm.maxStoneWorkers;

            Assert.AreEqual(1, count);
        }
        [Test]
        public void TestDecreaseMetalWorkers()
        {
            GameObject placeholder = new GameObject();
            WorkerManager wm = placeholder.AddComponent<WorkerManager>();
            wm.MetalHutBuild();
            wm.MetalHutBuild();
            wm.MetalHutDestroyed();
            int count = wm.maxMetalWorkers;

            Assert.AreEqual(1, count);
        }
        [Test]
        public void TestAllowWoodWorker()
        {
            GameObject placeholder = new GameObject();
            WorkerManager wm = placeholder.AddComponent<WorkerManager>();
            wm.WoodHutBuild();


            Assert.True(wm.AllowWorker(ResourceType.Wood));
        }
        [Test]
        public void TestAllowStoneWorker()
        {
            GameObject placeholder = new GameObject();
            WorkerManager wm = placeholder.AddComponent<WorkerManager>();
            wm.StoneHutBuild();


            Assert.True(wm.AllowWorker(ResourceType.Stone));
        }
        [Test]
        public void TestAllowMetalWorker()
        {
            GameObject placeholder = new GameObject();
            WorkerManager wm = placeholder.AddComponent<WorkerManager>();
            wm.MetalHutBuild();


            Assert.True(wm.AllowWorker(ResourceType.Metal));
        }
        [Test]
        public void TestNotAllowWoodWorker()
        {
            GameObject placeholder = new GameObject();
            WorkerManager wm = placeholder.AddComponent<WorkerManager>();

            Assert.False(wm.AllowWorker(ResourceType.Wood));
        }
        [Test]
        public void TestNotAllowStoneWorker()
        {
            GameObject placeholder = new GameObject();
            WorkerManager wm = placeholder.AddComponent<WorkerManager>();

            Assert.False(wm.AllowWorker(ResourceType.Stone));
        }
        [Test]
        public void TestNotAllowMetalWorker()
        {
            GameObject placeholder = new GameObject();
            WorkerManager wm = placeholder.AddComponent<WorkerManager>();

            Assert.False(wm.AllowWorker(ResourceType.Metal));
        }
    }
}
