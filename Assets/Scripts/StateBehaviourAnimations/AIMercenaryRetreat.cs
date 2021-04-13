using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMercenaryRetreat : AIMercenaryBase
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateEnter(animator,stateInfo,layerIndex);
        aiState.SetState(AIStates.Retreat);
        ai.SendSoldiersToAttack();
	}

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
            if(ai.findIdleSoldiers())
                ai.sendIdleSoldiersToCurrentAction();
            animator.SetBool("Retreated", ai.checkIfRetreated());
            
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        aiState.SetState(AIStates.Idle);
	}
}
