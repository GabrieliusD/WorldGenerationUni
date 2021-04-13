using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : BuildingBase
{

    public float radius;
    public float shootingSpeed;

    public float arrowSpeed;
    public GameObject projecticle;
    public GameObject shootingPoint;
    float elapsedTime;
    GameObject objectInRange;
    public override void Start()
    {
        base.Start();

    }

    void Update()
    {
        LookForEnemiesToAttack();
        if(objectInRange != null)
            Shoot();
    }

    public void Shoot()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime >= shootingSpeed)
        {
            Vector3 direction = objectInRange.transform.position+new Vector3(0,2,0)-shootingPoint.transform.position;
            GameObject arrow = Instantiate(projecticle, shootingPoint.transform.position, Quaternion.LookRotation(direction));
            arrow.GetComponent<Rigidbody>().AddForce((direction)*arrowSpeed);
            elapsedTime = 0;
        }
    }
    public void LookForEnemiesToAttack()
    {
        List<GameObject> inGameObjects = new List<GameObject>();
        if(tag == "Player")
        {
            inGameObjects = GameObjectTracker.Instance.GetEnemyGameObjects();
        }
        if(tag == "Enemy")
        {
            inGameObjects = GameObjectTracker.Instance.GetPlayerGameObjects();
        }

        foreach (GameObject item in inGameObjects)
        {
            float distance = Vector3.Distance(item.transform.position, transform.position);
            if(distance <= radius)
            {
                objectInRange = item;
                break;
            } else objectInRange = null;
        }
    }
}
