using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITownHallMarket : AITownHallBaseState
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateEnter(animator,animatorStateInfo, layerIndex);
        ai.build("market", animator);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, animatorStateInfo, layerIndex);
    }
}
