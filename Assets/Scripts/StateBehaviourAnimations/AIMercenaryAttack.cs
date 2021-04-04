using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMercenaryAttack : AIMercenaryBase
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateEnter(animator,stateInfo,layerIndex);
        ai.SetAIState(AIStates.Attack);
        ai.SendSoldiersToAttack();
	}

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
            animator.SetBool("attackPlayer", ai.allowAttack);
            if(ai.findIdleSoldiers())
                ai.sendIdleSoldiersToCurrentAction();
    }

     override public void OnStateMove(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}
}
