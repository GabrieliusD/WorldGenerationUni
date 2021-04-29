using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestGrid
    {
        [Test]
        public void TestCreateGrid()
        {
            GameObject placeholder = new GameObject();

            Grid grid = placeholder.AddComponent<Grid>();

            grid.gridWorldSize = new Vector2(20, 20);
            grid.nodeRadius = 1;
            grid.SetUp();
            grid.CreateGrid(5.0f);

            Assert.AreEqual(100, grid.MaxSize);
        }
        [Test]
        public void TestSetNodeToUnwakable()
        {
            GameObject placeholder = new GameObject();

            Grid grid = placeholder.AddComponent<Grid>();

            grid.gridWorldSize = new Vector2(20, 20);
            grid.nodeRadius = 1;
            grid.SetUp();
            grid.CreateGrid(5.0f);

            Node node = grid.NodeFromWorldPoint(new Vector3(0.5f,0.5f,0.5f));

            node.walkable = false;

            Node node2 = grid.NodeFromWorldPoint(new Vector3(0.5f, 0.5f, 0.5f));

            Assert.False(node2.walkable);


        }
        [Test]
        public void TestNodeIsWakable()
        {
            GameObject placeholder = new GameObject();
            GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            plane.transform.position = new Vector3(0,-2,0);
            plane.transform.localScale = new Vector3(10,1,10);
            Grid grid = placeholder.AddComponent<Grid>();

            grid.gridWorldSize = new Vector2(20, 20);
            grid.nodeRadius = 1;
            grid.SetUp();
            grid.CreateGrid(5.0f);

            GameObject tempObject = new GameObject();
            tempObject.transform.position = new Vector3(1,1,1);
            Bounds bound = tempObject.AddComponent<BoxCollider>().bounds;
            bound.size = new Vector3(5,5,5);

            //grid.SetNodeUnwakable(tempObject);

            bool result = grid.checkNodesAreEmpty(tempObject);

            Assert.True(result);


        }

        [Test]
        public void TestSetNodesUnwakalbeInDiameter()
        {
            GameObject placeholder = new GameObject();
            GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            plane.transform.position = new Vector3(0, -2, 0);
            plane.transform.localScale = new Vector3(10, 1, 10);
            Grid grid = placeholder.AddComponent<Grid>();

            grid.gridWorldSize = new Vector2(20, 20);
            grid.nodeRadius = 1;
            grid.SetUp();
            grid.CreateGrid(5.0f);

            GameObject tempObject = new GameObject();
            tempObject.transform.position = new Vector3(1,1,1);
            Bounds bound = tempObject.AddComponent<BoxCollider>().bounds;
            bound.size = new Vector3(5,5,5);

            grid.SetNodeUnwakable(tempObject);

            bool result = grid.checkNodesAreEmpty(tempObject);

            Assert.False(result);
        }
        [Test]
        public void TestcheckNodesAreEmptyInRadius()
        {
            GameObject placeholder = new GameObject();
            GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            plane.transform.position = new Vector3(0, -2, 0);
            plane.transform.localScale = new Vector3(10, 1, 10);
            Grid grid = placeholder.AddComponent<Grid>();

            grid.gridWorldSize = new Vector2(20, 20);
            grid.nodeRadius = 1;
            grid.SetUp();
            grid.CreateGrid(5.0f);

            bool result = grid.checkNodesAreEmpty(new Vector3(2,2,2), 1);
            Assert.True(result); 
        }



    }
}
