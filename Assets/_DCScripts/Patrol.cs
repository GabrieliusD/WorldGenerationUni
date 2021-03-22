using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : NPCBaseFSM {

	GameObject[] waypoints;
	int currentWP;

	void Awake()
	{
		waypoints = GameObject.FindGameObjectsWithTag("waypoint");
	}	

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateEnter(animator,stateInfo,layerIndex);
		currentWP = 0;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		if(waypoints.Length == 0) return;
        float dist = Vector3.Distance(waypoints[currentWP].transform.position, NPC.transform.position);

        if (dist < accuracy)
		{
			currentWP++;
			if(currentWP >= waypoints.Length)
			{
				currentWP = 0;
			}	
		}

        //rotate towards target

        Quaternion rot =
            FaceObject(new Vector2(waypoints[currentWP].transform.position.x, waypoints[currentWP].transform.position.y), new Vector2(NPC.transform.position.x, NPC.transform.position.y), FacingDirection.UP);

        NPC.transform.rotation = Quaternion.Slerp(NPC.transform.rotation,
                            rot,
                            rotSpeed * Time.deltaTime);
        NPC.transform.Translate(0, Time.deltaTime * speed, 0 );
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}




}
