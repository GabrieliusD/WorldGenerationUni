using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestMarketUi
    {
        [Test]
        public void TestButtonPressedForPurchaseWood()
        {
            GameObject temp = new GameObject();

            ResourceManager rs = temp.AddComponent<ResourceManager>();

            rs.SetWood(0,PlayerTypes.humanPlayer);
            rs.SetGold(1000,PlayerTypes.humanPlayer);
            MarketUI market = temp.AddComponent<MarketUI>();
            market.woodButtonPressed();
            market.buyResource();
            int actual = rs.GetWood(PlayerTypes.humanPlayer);
            Assert.AreEqual(10,actual);

        }
        [Test]
        public void TestButtonPressedForPurchaseStone()
        {
            GameObject temp = new GameObject();

            ResourceManager rs = temp.AddComponent<ResourceManager>();
            rs.SetStone(0, PlayerTypes.humanPlayer);
            rs.SetGold(1000, PlayerTypes.humanPlayer);
            MarketUI market = temp.AddComponent<MarketUI>();
            market.stoneButtonPressed();
            market.buyResource();
            int actual = rs.GetStone(PlayerTypes.humanPlayer);
            Assert.AreEqual(10, actual);
        }
        [Test]
        public void TestButtonPressedForPurchaseMetal()
        {
            GameObject temp = new GameObject();

            ResourceManager rs = temp.AddComponent<ResourceManager>();

            rs.SetMetal(0, PlayerTypes.humanPlayer);
            rs.SetGold(1000, PlayerTypes.humanPlayer);
            MarketUI market = temp.AddComponent<MarketUI>();
            market.metalButtonPressed();
            market.buyResource();
            int actual = rs.GetMetal(PlayerTypes.humanPlayer);
            Assert.AreEqual(10, actual);
        }
        [Test]
        public void TestCheckDoNotSellIfNoResources()
        {
            GameObject temp = new GameObject();

            ResourceManager rs = temp.AddComponent<ResourceManager>();

            rs.SetMetal(0, PlayerTypes.humanPlayer);
            rs.SetWood(0,PlayerTypes.humanPlayer);
            rs.SetStone(0, PlayerTypes.humanPlayer);

            MarketUI market = temp.AddComponent<MarketUI>();
            market.woodButtonPressed();
            market.sellResource();
            market.stoneButtonPressed();
            market.sellResource();
            market.metalButtonPressed();
            market.sellResource();

            int actual = rs.GetMetal(PlayerTypes.humanPlayer);
            Assert.AreEqual(0, actual);
            actual = rs.GetWood(PlayerTypes.humanPlayer);
            Assert.AreEqual(0, actual);
            actual = rs.GetStone(PlayerTypes.humanPlayer);
            Assert.AreEqual(0, actual);
        }

        [Test]
        public void TestButtonPressedForSellWood()
        {
            GameObject temp = new GameObject();

            ResourceManager rs = temp.AddComponent<ResourceManager>();

            rs.SetWood(20, PlayerTypes.humanPlayer);
            MarketUI market = temp.AddComponent<MarketUI>();
            market.woodButtonPressed();
            market.sellResource();
            int actual = rs.GetWood(PlayerTypes.humanPlayer);
            Assert.AreEqual(10, actual);
        }
        [Test]
        public void TestButtonPressedForSellStone()
        {
            GameObject temp = new GameObject();

            ResourceManager rs = temp.AddComponent<ResourceManager>();

            rs.SetStone(20, PlayerTypes.humanPlayer);
            MarketUI market = temp.AddComponent<MarketUI>();
            market.stoneButtonPressed();
            market.sellResource();
            int actual = rs.GetStone(PlayerTypes.humanPlayer);
            Assert.AreEqual(10, actual);
        }
        [Test]
        public void TestButtonPressedForSellMetal()
        {
            GameObject temp = new GameObject();

            ResourceManager rs = temp.AddComponent<ResourceManager>();

            rs.SetMetal(20, PlayerTypes.humanPlayer);
            MarketUI market = temp.AddComponent<MarketUI>();
            market.metalButtonPressed();
            market.sellResource();
            int actual = rs.GetMetal(PlayerTypes.humanPlayer);
            Assert.AreEqual(10, actual);
        }

    }
}
