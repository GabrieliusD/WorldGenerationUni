using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectTracker : MonoBehaviour
{
    List<GameObject> Enemies = new List<GameObject>();
    List<GameObject> Players = new List<GameObject>();

    public static GameObjectTracker Instance{get; private set;}
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    void AddEnemyObject(GameObject enemy)
    {
        Enemies.Add(enemy);
    }

    void AddPlayerObject(GameObject player)
    {
        Players.Add(player);
    }

    void RemoveEnemyObject(GameObject enemy)
    {
        Enemies.Remove(enemy);
    }

    void RemovePlayerObject(GameObject player)
    {
        Players.Remove(player);
    }

    public List<GameObject> GetEnemyGameObjects()
    {
        return Enemies;
    }
    public List<GameObject> GetPlayerGameObjects()
    {
        return Players;
    }

    public void AddObject(GameObject obj)
    {
        if(obj.tag == "Player")
        {
            AddPlayerObject(obj);
        }
        if(obj.tag == "Enemy")
        {
            AddEnemyObject(obj);
        }
    }

    public void RemoveObject(GameObject obj)
    {
        if(obj.tag == "Player")
        {
            RemovePlayerObject(obj);
        }
        if(obj.tag == "Enemy")
        {
            RemoveEnemyObject(obj);
        }
    }
}
