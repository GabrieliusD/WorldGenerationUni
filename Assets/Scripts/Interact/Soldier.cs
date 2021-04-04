using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : UnitBase
{
    public enum SoldierState{ Idle, Attacking, Patrol, None};

    SoldierState soldierState = SoldierState.Patrol;
    // Start is called before the first frame update
    float attackDamage = 5.0f;
    float attackDistance = 5.0f;
    public float attackSpeed = 2.0f;
    float timeElapsed;
    Animator animator;
    public override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }
    public Interactable GetInteractable()
    {
        return focus;
    }

    // Update is called once per frame
    void Update()
    {
        if(focus != null && body.velocity.magnitude < 0)
            IssuePath(focus.transform.position);
        DestoryUnit();
        if(focus!= null)
        {
            float distance =  Vector3.Distance(this.transform.position, focus.transform.position);
            animator.SetFloat("distance", distance);
            if(distance <= attackDistance)
            {
                timeElapsed += Time.deltaTime;
                if(timeElapsed >= attackSpeed)
                {
                    DamageFocus();
                    timeElapsed = 0f;
                }
            }

        }
        checkState();

    }

    public void DamageFocus()
    {
        ((InteractAttackable)focus).setDamage(attackDamage);
        focus.Interact();
    }

    public void FindNearestUnitToAttack()
    {
        InvokeRepeating("SearchEnemies", 0.5f, 1.0f);
    }

    public void StopSearchingForUnits()
    {
        CancelInvoke("SearchEnemies");
    }

    void SearchEnemies()
    {
        List<GameObject> isPlayerObject = new List<GameObject>();
        Collider[] objectsInRadius = Physics.OverlapSphere(this.transform.position, 100);
        foreach (var item in objectsInRadius)
        {
            if(item.tag == "Player")
            {
                isPlayerObject.Add(item.gameObject);
            }
        }
        float closest = Mathf.Infinity;
        GameObject closestObject = null;
        foreach (var item in isPlayerObject)
        {
            float distance = Vector3.Distance(this.transform.position, item.transform.position);
            if(distance < closest)
            {
                closest = distance;
                closestObject = item;
            }
        }
        if(closestObject != null)
        focus = closestObject.GetComponent<InteractAttackable>();
        animator.SetFloat("distance", closest);
    }


    public float attack()
    {
        return attackDamage;
    }

    public void checkState()
    {
        if(body.velocity.magnitude > 0)
            soldierState = SoldierState.Patrol;
        if(focus == null && body.velocity.magnitude <= 0)
            soldierState = SoldierState.Idle;
        if(focus != null && body.velocity.magnitude > 0)
            soldierState = SoldierState.Attacking;
    }

    public SoldierState GetSoldierState()
    {
        return soldierState;
    }
}
