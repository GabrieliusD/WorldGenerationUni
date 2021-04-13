using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : UnitBase
{
    public enum SoldierState{ Idle, Attacking, Patrol, None};

    SoldierState soldierState = SoldierState.Patrol;
    // Start is called before the first frame update
    float attackDamage = 5.0f;
    float attackDistance = 6.0f;
    public float attackSpeed = 2.0f;
    float timeElapsed;
    bool attacking = false;
    public float searchDistance = 20.0f;
    public override void Start()
    {
        base.Start();
    }
    public Interactable GetInteractable()
    {
        return focus;
    }


    // Update is called once per frame
    void Update()
    {
        if(focus!= null)
        {
            float distance =  Vector3.Distance(this.transform.position, focus.transform.position);
            animator.SetFloat("distance", distance);
            if(distance <= attackDistance)
            {
                attacking = true;
                timeElapsed += Time.deltaTime;
                if(timeElapsed >= attackSpeed)
                {
                    DamageFocus();
                    animator.SetBool("attack", true);
                    timeElapsed = 0f;
                }
            } 
            else {
                attacking = false;
                animator.SetBool("attack", false);

            }

        }
        if(focus != null && !moving && !attacking)
            IssuePath(focus.transform.position);
        if(focus == null){
                attacking = false;
                animator.SetBool("attack", false);

            }

        if(tag == "Player")
        AttackClosesEnemy();
        checkState();
        


    }

    private void FixedUpdate()
    {
        
    }

    public void DamageFocus()
    {
        ((InteractAttackable)focus).setDamage(attackDamage);
        focus.Interact();
    }

    public void FindNearestUnitToAttack()
    {
        if(tag == "Enemy")
        InvokeRepeating("SearchEnemies", 0.5f, 1.0f);
    }

    public void StopSearchingForUnits()
    {
        if(tag == "Enemy")
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

    void AttackClosesEnemy()
    {
        List<GameObject> players = GameObjectTracker.Instance.GetEnemyGameObjects();

        List<GameObject> inDistance = new List<GameObject>();
        foreach (var player in players)
        {
            if(player!=null)
            {
                float distance = Vector3.Distance(this.transform.position, player.transform.position);
                if(distance <= searchDistance)
                {
                    inDistance.Add(player);
                }
            }
        }


        float closest = Mathf.Infinity;
        GameObject closestObject = null;

        foreach (var item in inDistance)
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
    }


    public float attack()
    {
        return attackDamage;
    }

    public void checkState()
    {
        if(moving)
            soldierState = SoldierState.Patrol;
        if(focus == null && !moving && !attacking)
            soldierState = SoldierState.Idle;
        if(focus != null && moving && attacking)
            soldierState = SoldierState.Attacking;
    }

    public SoldierState GetSoldierState()
    {
        return soldierState;
    }
}
