using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleEnemyState : BaseEnemyState
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateEnter(animator,stateInfo,layerIndex);
        NPC.FindNearestUnitToAttack();
	}

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {

    }

    
    	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            NPC.StopSearchingForUnits();
    }
}
