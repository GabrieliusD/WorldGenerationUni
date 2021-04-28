using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestUnitStorage
    {
        // A Test behaves as an ordinary method
        [Test]
        public void TestConstructor()
        {
            UnitStorage uStorage = new UnitStorage(30);
            int result = uStorage.getCapacity();

            Assert.AreEqual(30, result);

        }
        [Test]
        public void TestSetResourceType()
        {
            ResourceType expected = ResourceType.Wood;
            UnitStorage uStorage = new UnitStorage(30);
            uStorage.SetResourceType(expected);

            ResourceType actual = uStorage.GetResourceType();

            Assert.AreEqual(expected,actual);

        }
        [Test]
        public void TestGetResourceType()
        {
            ResourceType expected = ResourceType.Metal;
            UnitStorage uStorage = new UnitStorage(30);
            uStorage.SetResourceType(expected);

            ResourceType actual = uStorage.GetResourceType();

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestGather()
        {
            int expected = 30;
            UnitStorage uStorage = new UnitStorage(expected);
            uStorage.Gather(50);
            int actual = uStorage.getStorage();

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestGatherDoNotExceedCapacity()
        {
            int expected = 30;
            UnitStorage uStorage = new UnitStorage(30);
            uStorage.Gather(40);
            int actual = uStorage.getStorage();

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestGetStorage()
        {
            int expected = 10;
            UnitStorage uStorage = new UnitStorage(20);
            uStorage.Gather(10);

            int actual = uStorage.getStorage();

            Assert.AreEqual(expected,actual);
        }
        [Test]
        public void TestDepositStorage()
        {
            int expected = 0;
            UnitStorage uStorage = new UnitStorage(20);
            uStorage.Gather(20);

            uStorage.DepositStorage();

            int actual = uStorage.getStorage();

            Assert.AreEqual(expected,actual);
        }
        [Test]
        public void TestCheckWhenFull()
        {
            
            UnitStorage uStorage = new UnitStorage(10);
            uStorage.Gather(20);

            Assert.True(uStorage.checkFull());
        }
        [Test]
        public void TestCheckWhenNotFull()
        {

            UnitStorage uStorage = new UnitStorage(40);
            uStorage.Gather(20);

            Assert.False(uStorage.checkFull());
        }
        [Test]
        public void TestCheckWhenResourceTypeChanged()
        {
            UnitStorage uStorage = new UnitStorage(40);
            uStorage.SetResourceType(ResourceType.Wood);
            uStorage.Gather(20);
            uStorage.SetResourceType(ResourceType.Metal);

            int actual = uStorage.getStorage();
            Assert.AreEqual(0,actual);
        }


    }
}
