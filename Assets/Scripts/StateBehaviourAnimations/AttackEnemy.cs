using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy : BaseEnemyState
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateEnter(animator,stateInfo,layerIndex);
	}

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if(opponent == null)
            animator.SetFloat("distance", Mathf.Infinity);
    }

    	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	}
}
