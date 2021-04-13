using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMercenaryBase : StateMachineBehaviour
{
    protected AIMercenary ai;
    protected AIStateManager aiState;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        ai = animator.GetComponent<AIMercenary>();
        aiState = AIStateManager.Instance;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.SetInteger("gold", ResourceManager.Instance.GetGold(PlayerTypes.AIPlayer));
        if(ai.getRecruitedSoldiersCount() < ai.maxSoldiers)
        {
            animator.SetBool("recruit", true);
        }
        animator.SetBool("attackPlayer", ai.allowAttack);

    }

}
