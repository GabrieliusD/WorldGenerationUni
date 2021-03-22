using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : NPCBaseFSM {

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		base.OnStateEnter(animator,stateInfo,layerIndex);
		NPC.GetComponent<TankAI>().StartFiring();
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
        Quaternion rot =
        FaceObject(new Vector2(opponent.transform.position.x, opponent.transform.position.y), new Vector2(NPC.transform.position.x, NPC.transform.position.y), FacingDirection.UP);

        NPC.transform.rotation = Quaternion.Slerp(NPC.transform.rotation,
                            rot,
                            rotSpeed * Time.deltaTime);
    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		NPC.GetComponent<TankAI>().StopFiring();
	}
}
