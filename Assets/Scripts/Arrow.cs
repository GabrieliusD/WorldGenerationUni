using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float arrowDamage;

    private void OnCollisionEnter(Collision other){
        ObjectParameter op = other.collider.GetComponent<ObjectParameter>();
        if(op != null)
        op.takeDamage(arrowDamage);

        Destroy(gameObject);
    }
}
