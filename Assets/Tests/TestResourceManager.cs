using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestResourceManager
    {
        [Test]
        public void TestGetSetWoodPlayer()
        {
            GameObject RM = new GameObject();
            
            ResourceManager rs = RM.AddComponent<ResourceManager>();
            //rs.ConfigureResourceSettings();
            rs.SetWood(1000, PlayerTypes.humanPlayer);
            Assert.AreEqual(1000,rs.GetWood(PlayerTypes.humanPlayer));

        }
        [Test]
        public void TestGetSetStonePlayer()
        {
            GameObject RM = new GameObject();

            ResourceManager rs = RM.AddComponent<ResourceManager>();
            rs.SetStone(1000, PlayerTypes.humanPlayer);
            Assert.AreEqual(1000, rs.GetStone(PlayerTypes.humanPlayer));
        }
        [Test]
        public void TestGetSetMetalPlayer()
        {
            GameObject RM = new GameObject();

            ResourceManager rs = RM.AddComponent<ResourceManager>();
            rs.SetMetal(1000, PlayerTypes.humanPlayer);
            Assert.AreEqual(1000, rs.GetMetal(PlayerTypes.humanPlayer));
        }
        [Test]
        public void TestGetSetGoldPlayer()
        {
            GameObject RM = new GameObject();

            ResourceManager rs = RM.AddComponent<ResourceManager>();
            rs.SetGold(1000, PlayerTypes.humanPlayer);
            Assert.AreEqual(1000, rs.GetGold(PlayerTypes.humanPlayer));
        }
        [Test]
        public void TestIncreaseStoragePlayer()
        {
            GameObject RM = new GameObject();

            ResourceManager rs = RM.AddComponent<ResourceManager>();
            rs.IncreaseMaxStorage(PlayerTypes.humanPlayer);
            Assert.AreEqual(800,rs.GetMaxWood(PlayerTypes.humanPlayer));
            Assert.AreEqual(350, rs.GetMaxStone(PlayerTypes.humanPlayer));
            Assert.AreEqual(150, rs.GetMaxMetal(PlayerTypes.humanPlayer));
        }
        [Test]
        public void TestDecreaseStoragePlayer()
        {
            GameObject RM = new GameObject();

            ResourceManager rs = RM.AddComponent<ResourceManager>();
            rs.IncreaseMaxStorage(PlayerTypes.humanPlayer);
            rs.DecreaseMaxStorage(PlayerTypes.humanPlayer);
            Assert.AreEqual(500, rs.GetMaxWood(PlayerTypes.humanPlayer));
            Assert.AreEqual(200, rs.GetMaxStone(PlayerTypes.humanPlayer));
            Assert.AreEqual(100, rs.GetMaxMetal(PlayerTypes.humanPlayer));
        }
        
        [Test]
        public void TestDepositResourcesPlayer()
        {
            GameObject RM = new GameObject();

            ResourceManager rs = RM.AddComponent<ResourceManager>();
            rs.SetWood(100,PlayerTypes.humanPlayer);
            rs.SetStone(100,PlayerTypes.humanPlayer);
            rs.SetMetal(0,PlayerTypes.humanPlayer);

            rs.DepositResource(ResourceType.Wood, 20, PlayerTypes.humanPlayer);
            rs.DepositResource(ResourceType.Stone, 40, PlayerTypes.humanPlayer);
            rs.DepositResource(ResourceType.Metal, 60, PlayerTypes.humanPlayer);

            Assert.AreEqual(120, rs.GetWood(PlayerTypes.humanPlayer));
            Assert.AreEqual(140, rs.GetStone(PlayerTypes.humanPlayer));
            Assert.AreEqual(60, rs.GetMetal(PlayerTypes.humanPlayer));
        }
        [Test]
        public void TestBuyResourcesPlayer()
        {
            GameObject RM = new GameObject();

            ResourceManager rs = RM.AddComponent<ResourceManager>();
            rs.SetWood(100, PlayerTypes.humanPlayer);
            rs.SetStone(100, PlayerTypes.humanPlayer);
            rs.SetMetal(0, PlayerTypes.humanPlayer);
            rs.SetGold(500,PlayerTypes.humanPlayer);

            rs.buyResource(ResourceType.Wood, 10, PlayerTypes.humanPlayer);
            rs.buyResource(ResourceType.Stone, 15, PlayerTypes.humanPlayer);
            rs.buyResource(ResourceType.Metal, 25, PlayerTypes.humanPlayer);

            Assert.AreEqual(110,rs.GetWood(PlayerTypes.humanPlayer));
            Assert.AreEqual(115, rs.GetStone(PlayerTypes.humanPlayer));
            Assert.AreEqual(25, rs.GetMetal(PlayerTypes.humanPlayer));
        }
        [Test]
        public void TestSellResourcesPlayer()
        {
            GameObject RM = new GameObject();

            ResourceManager rs = RM.AddComponent<ResourceManager>();
            rs.SetWood(100, PlayerTypes.humanPlayer);
            rs.SetStone(100, PlayerTypes.humanPlayer);
            rs.SetMetal(100, PlayerTypes.humanPlayer);
            rs.SetGold(500, PlayerTypes.humanPlayer);

            rs.sellResource(ResourceType.Wood, 10, PlayerTypes.humanPlayer);
            rs.sellResource(ResourceType.Stone, 15, PlayerTypes.humanPlayer);
            rs.sellResource(ResourceType.Metal, 25, PlayerTypes.humanPlayer);

            Assert.AreEqual(90, rs.GetWood(PlayerTypes.humanPlayer));
            Assert.AreEqual(85, rs.GetStone(PlayerTypes.humanPlayer));
            Assert.AreEqual(75, rs.GetMetal(PlayerTypes.humanPlayer));
        }
        [Test]
        public void TestPurchaseBuildingPlayer()
        {
            GameObject RM = new GameObject();

            ResourceManager rs = RM.AddComponent<ResourceManager>();
            rs.SetWood(100, PlayerTypes.humanPlayer);
            rs.SetStone(100, PlayerTypes.humanPlayer);
            rs.PurchaseBuilding(50,50,PlayerTypes.humanPlayer);

            Assert.AreEqual(50, rs.GetWood(PlayerTypes.humanPlayer));
            Assert.AreEqual(50, rs.GetStone(PlayerTypes.humanPlayer));

        }
        [Test]
        public void TestPurchaseSoldierPlayer()
        {
            GameObject RM = new GameObject();

            ResourceManager rs = RM.AddComponent<ResourceManager>();
            rs.SetGold(600, PlayerTypes.humanPlayer);

            rs.PurchaseSoldier(200, PlayerTypes.humanPlayer);

            Assert.AreEqual(400, rs.GetGold(PlayerTypes.humanPlayer));
        }
        [Test]
        public void TestGetSetWoodAI()
        {
            GameObject RM = new GameObject();

            ResourceManager rs = RM.AddComponent<ResourceManager>();
            rs.ConfigureResourceSettings();
            Assert.AreEqual(1000, rs.GetWood(PlayerTypes.AIPlayer));

        }
        [Test]
        public void TestGetSetStoneAI()
        {
            GameObject RM = new GameObject();

            ResourceManager rs = RM.AddComponent<ResourceManager>();
            rs.SetStone(1000, PlayerTypes.humanPlayer);
            Assert.AreEqual(1000, rs.GetStone(PlayerTypes.AIPlayer));
        }
        [Test]
        public void TestGetSetMetalAI()
        {
            GameObject RM = new GameObject();

            ResourceManager rs = RM.AddComponent<ResourceManager>();
            rs.SetStone(1000, PlayerTypes.humanPlayer);
            Assert.AreEqual(1000, rs.GetMetal(PlayerTypes.AIPlayer));
        }
        [Test]
        public void TestGetSetGoldAI()
        {
            GameObject RM = new GameObject();

            ResourceManager rs = RM.AddComponent<ResourceManager>();
            rs.SetStone(1000, PlayerTypes.humanPlayer);
            Assert.AreEqual(1000, rs.GetGold(PlayerTypes.AIPlayer));
        }
        [Test]
        public void TestDepositResourcesAI()
        {
            GameObject RM = new GameObject();

            ResourceManager rs = RM.AddComponent<ResourceManager>();
            rs.SetWood(100, PlayerTypes.AIPlayer);
            rs.SetStone(100, PlayerTypes.AIPlayer);
            rs.SetMetal(100, PlayerTypes.AIPlayer);

            rs.DepositResource(ResourceType.Wood, 20, PlayerTypes.AIPlayer);
            rs.DepositResource(ResourceType.Stone, 40, PlayerTypes.AIPlayer);
            rs.DepositResource(ResourceType.Metal, 60, PlayerTypes.AIPlayer);

            Assert.AreEqual(120, rs.GetWood(PlayerTypes.AIPlayer));
            Assert.AreEqual(140, rs.GetStone(PlayerTypes.AIPlayer));
            Assert.AreEqual(160, rs.GetMetal(PlayerTypes.AIPlayer));
        }
        [Test]
        public void TestBuyResourcesAI()
        {
            GameObject RM = new GameObject();

            ResourceManager rs = RM.AddComponent<ResourceManager>();
            rs.SetWood(100, PlayerTypes.AIPlayer);
            rs.SetStone(100, PlayerTypes.AIPlayer);
            rs.SetMetal(100, PlayerTypes.AIPlayer);
            rs.SetGold(500, PlayerTypes.AIPlayer);

            rs.buyResource(ResourceType.Wood, 10, PlayerTypes.AIPlayer);
            rs.buyResource(ResourceType.Stone, 15, PlayerTypes.AIPlayer);
            rs.buyResource(ResourceType.Metal, 25, PlayerTypes.AIPlayer);

            Assert.AreEqual(110, rs.GetWood(PlayerTypes.AIPlayer));
            Assert.AreEqual(115, rs.GetStone(PlayerTypes.AIPlayer));
            Assert.AreEqual(125, rs.GetMetal(PlayerTypes.AIPlayer));
        }
        [Test]
        public void TestSellResourcesAI()
        {
            GameObject RM = new GameObject();

            ResourceManager rs = RM.AddComponent<ResourceManager>();
            rs.SetWood(100, PlayerTypes.AIPlayer);
            rs.SetStone(100, PlayerTypes.AIPlayer);
            rs.SetMetal(100, PlayerTypes.AIPlayer);
            rs.SetGold(500, PlayerTypes.AIPlayer);

            rs.sellResource(ResourceType.Wood, 10, PlayerTypes.AIPlayer);
            rs.sellResource(ResourceType.Stone, 15, PlayerTypes.AIPlayer);
            rs.sellResource(ResourceType.Metal, 25, PlayerTypes.AIPlayer);

            Assert.AreEqual(90, rs.GetWood(PlayerTypes.AIPlayer));
            Assert.AreEqual(85, rs.GetStone(PlayerTypes.AIPlayer));
            Assert.AreEqual(75, rs.GetMetal(PlayerTypes.AIPlayer));
        }
        [Test]
        public void TestPurchaseBuildingAI()
        {
            GameObject RM = new GameObject();

            ResourceManager rs = RM.AddComponent<ResourceManager>();
            rs.SetWood(100, PlayerTypes.AIPlayer);
            rs.SetStone(100, PlayerTypes.AIPlayer);
            rs.PurchaseBuilding(50, 50, PlayerTypes.AIPlayer);

            Assert.AreEqual(50, rs.GetWood(PlayerTypes.AIPlayer));
            Assert.AreEqual(50, rs.GetStone(PlayerTypes.AIPlayer));
        }
        [Test]
        public void TestPurchaseSoldierAI()
        {
            GameObject RM = new GameObject();

            ResourceManager rs = RM.AddComponent<ResourceManager>();
            rs.SetGold(600, PlayerTypes.AIPlayer);

            rs.PurchaseSoldier(200, PlayerTypes.AIPlayer);

            Assert.AreEqual(400, rs.GetGold(PlayerTypes.AIPlayer));
        }
    }
}
