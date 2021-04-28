using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestObjectTracker
    {
        [Test]
        public void TestObjectTrackerEnemyCount()
        {
            GameObject temp = new GameObject();
            temp.AddComponent<GameObjectTracker>();

            GameObjectTracker got = temp.GetComponent<GameObjectTracker>();
            
            for (int i = 0; i < 5; i++)
            {
                GameObject obj1 = new GameObject();
                obj1.tag = "Player";
                got.AddObject(obj1);
            }
            for (int i = 0; i < 3; i++)
            {
                GameObject obj1 = new GameObject();
                obj1.tag = "Enemy";
                got.AddObject(obj1);
            }

            int actual = got.GetEnemyGameObjects().Count;

            Assert.AreEqual(3,actual);
            
        }
        [Test]
        public void TestObjectTrackerPlayerCount()
        {
            GameObject temp = new GameObject();
            temp.AddComponent<GameObjectTracker>();

            GameObjectTracker got = temp.GetComponent<GameObjectTracker>();

            for (int i = 0; i < 5; i++)
            {
                GameObject obj1 = new GameObject();
                obj1.tag = "Player";
                got.AddObject(obj1);
            }
            for (int i = 0; i < 3; i++)
            {
                GameObject obj1 = new GameObject();
                obj1.tag = "Enemy";
                got.AddObject(obj1);
            }

            int actual = got.GetPlayerGameObjects().Count;

            Assert.AreEqual(5, actual);

        }
        [Test]
        public void TestObjectTrackerRemoveEnemyObject()
        {
            GameObject temp = new GameObject();
            temp.AddComponent<GameObjectTracker>();

            GameObjectTracker got = temp.GetComponent<GameObjectTracker>();
            List<GameObject> tempEnemies = new List<GameObject>();
            for (int i = 0; i < 5; i++)
            {
                GameObject obj1 = new GameObject();
                obj1.tag = "Player";
                got.AddObject(obj1);
            }
            for (int i = 0; i < 3; i++)
            {
                GameObject obj1 = new GameObject();
                obj1.tag = "Enemy";
                tempEnemies.Add(obj1);
                got.AddObject(obj1);
            }
            got.RemoveObject(tempEnemies[1]);
            int actual = got.GetEnemyGameObjects().Count;

            Assert.AreEqual(2, actual);

        }
        [Test]
        public void TestObjectTrackerRemovePlayerObject()
        {
            GameObject temp = new GameObject();
            temp.AddComponent<GameObjectTracker>();

            GameObjectTracker got = temp.GetComponent<GameObjectTracker>();
            List<GameObject> tempPlayers = new List<GameObject>();
            for (int i = 0; i < 5; i++)
            {
                GameObject obj1 = new GameObject();
                obj1.tag = "Player";
                got.AddObject(obj1);
                tempPlayers.Add(obj1);
            }
            for (int i = 0; i < 3; i++)
            {
                GameObject obj1 = new GameObject();
                obj1.tag = "Enemy";
                got.AddObject(obj1);
            }
            got.RemoveObject(tempPlayers[1]);
            int actual = got.GetPlayerGameObjects().Count;

            Assert.AreEqual(4, actual);

        }

    }
}
