using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITownHallBaseState : StateMachineBehaviour
{
    protected AIBuilding ai;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        ai = animator.GetComponent<AIBuilding>();
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.SetInteger("wood", ResourceManager.Instance.GetWood(PlayerTypes.AIPlayer));
        animator.SetInteger("metal", ResourceManager.Instance.GetMetal(PlayerTypes.AIPlayer));
        animator.SetInteger("stone", ResourceManager.Instance.GetStone(PlayerTypes.AIPlayer));

    }
}
