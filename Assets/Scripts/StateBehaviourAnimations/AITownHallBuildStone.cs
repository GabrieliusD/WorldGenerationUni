using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITownHallBuildStone : AITownHallBaseState
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        ai.build("StoneHut", animator);
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, animatorStateInfo, layerIndex);
    }
}
