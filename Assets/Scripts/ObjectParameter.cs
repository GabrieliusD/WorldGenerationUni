using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectParameter : MonoBehaviour
{
    public float health;

    void Update()
    {
        if(health <= 0)
        Destroy(this.gameObject);
    }

    public void takeDamage(float amount)
    {
        health -= amount;
    }

}
