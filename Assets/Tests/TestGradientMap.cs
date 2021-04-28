using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestGradientMap
    {
        [Test]
        public void TestCreateCircleMap()
        {   
            int width = 256;
            int height = 128;    
            GradientMap.GenerateCircleGradient(width,height, 40);
            float[,] mask = GradientMap.GetMask();

            Assert.AreEqual(width,mask.GetLength(0));
            Assert.AreEqual(height, mask.GetLength(1));
        }
        [Test]
        public void TestCreateIslandMask()
        {
            int width = 400;
            int height = 200;
            GradientMap.GenerateCircleGradient(width, height, 40);
            float[,] mask = GradientMap.GetMask();

            Assert.AreEqual(width, mask.GetLength(0));
            Assert.AreEqual(height, mask.GetLength(1));
        }
        [Test]
        public void TestCreateSquareMap()
        {
            int width = 512;
            int height = 1028;
            GradientMap.GenerateCircleGradient(width, height, 40);
            float[,] mask = GradientMap.GetMask();

            Assert.AreEqual(width, mask.GetLength(0));
            Assert.AreEqual(height, mask.GetLength(1));
        }
        [Test]
        public void TestSampleGradientCentre()
        {
            int width = 400;
            int height = 400;
            GradientMap.GenerateCircleGradient(width, height, 40);
            float[,] mask = GradientMap.GetMask();
            float expected = mask[200,200];
            float actual = GradientMap.sampleGradient(0.5f,0.5f);

        }

    }
}
