using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMercenaryRecruit : AIMercenaryBase
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateEnter(animator,stateInfo,layerIndex);

	}

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if(ai.getRecruitedSoldiersCount() >= ai.maxSoldiers)
        {
            animator.SetBool("recruit",  false);
        } else {
            ai.recruitSoldier();
        }

    }
    override public void OnStateMove(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {

    }

    	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}
}
