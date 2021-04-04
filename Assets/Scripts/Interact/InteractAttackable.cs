using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractAttackable : Interactable
{
    ObjectParameter op;
    float damage;
    private void Start()
    {
        op = GetComponent<ObjectParameter>();

    }
    public void setDamage(float newDamage)
    {
        damage = newDamage;
    }
    public override void Interact()
    {
        op.takeDamage(damage);
    }

    private void Update()
    {
        
    }
}
