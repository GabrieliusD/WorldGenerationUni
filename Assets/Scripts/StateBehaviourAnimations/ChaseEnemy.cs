using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemy : BaseEnemyState
{
    	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateEnter(animator,stateInfo,layerIndex);
		if(opponent != null)
			NPC.GetComponent<UnitBase>().IssuePath(opponent.transform.position);

	}

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
		if(opponent == null ) 
			animator.SetFloat("distance", Mathf.Infinity);
		if(opponent != null && NPC.body.velocity.magnitude <= 0)
		    NPC.GetComponent<UnitBase>().IssuePath(opponent.transform.position);
		

    }

    	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}
}
