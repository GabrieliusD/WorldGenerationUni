using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    public PlayerTypes PlayerType;
    protected float health = 20.0f;
    public Interactable focus;
    public Interactable lastFocus;
    protected Transform target;
    //public Transform target;
    public float speed = 5.0f;
    protected Vector3[] path;
    int targetIndex;
    protected Animator animator;

    public Rigidbody body;

    public bool moving;


    public virtual void Start()
    {
        body = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        GameObjectTracker.Instance.AddObject(this.gameObject);
    }
    
    public void takeDamage(float damage)
    {
        health-= damage;
        Debug.Log("taking damage health is: " + health);
    }
    public void DestoryUnit()
    {
        if(health <= 0)
        {
            Destroy(this.gameObject);
            Debug.Log("Soldier destroyed");
        }
    }

    public void SetFocus(Interactable newFocus)
    {
        if(newFocus != focus)
        {
            if(focus != null)
                focus.DeFocused(transform);
                
            focus = newFocus;
            TrackTarget(focus);

        }

        newFocus.OnFocused(transform);
    }

    public virtual void RemoveFocus()
    {
        if(focus != null)
        {
            focus.DeFocused(transform);
            //WorkerManager.Instance.DecreaseCurrentWorkers(unitStorage.GetResourceType());
        }
        focus = null;
        StopTracking();
    }
    public void IssuePath(Vector3 target)
    {
        if(target != null && this != null)
        PathRequest.RequestPath(transform.position, target, OnPathFound);
    }

    public void TrackTarget(Interactable newTarget)
    {
        
        target = newTarget.transform;
        IssuePath(target.position);
    }

    public void StopTracking()
    {
        target = null;
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if(pathSuccessful)
        {
                if(newPath.Length != 0)
                {
                targetIndex = 0;
                path = newPath;
                StopCoroutine("FollowPath");
                StartCoroutine("FollowPath");
                }
            
            
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];

        while(true)
        {
            Vector2 pPos = new Vector2(transform.position.x, transform.position.z);
            Vector2 cWay = new Vector2(currentWaypoint.x, currentWaypoint.z);
            float marginOfError = Vector2.Distance(pPos, cWay);
            
            animator.SetBool("moving", true);
            
            if(pPos == cWay)
            {
                targetIndex++;
                if(targetIndex >= path.Length)
                {
                    animator.SetBool("moving", false);
                    moving = false;
                    yield break;

                }
                currentWaypoint = path[targetIndex];
            }
            Vector3 TargetPos = new Vector3(currentWaypoint.x, transform.position.y, currentWaypoint.z);
            transform.position = Vector3.MoveTowards(transform.position, TargetPos, speed*Time.deltaTime);
            Vector3 relative = TargetPos - transform.position;
            transform.rotation = Quaternion.LookRotation(relative, Vector3.up);
            moving = true;
            yield return null;
        }
    }

    public void OnDrawGizmos()
    {
        if(path != null)
        {
            for(int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if(i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                } else
                {
                    Gizmos.DrawLine(path[i-1], path[i]);
                }
            }
        }
    }

    private void OnDestroy()
    {
        GameObjectTracker.Instance.RemoveObject(this.gameObject);

    }
}
