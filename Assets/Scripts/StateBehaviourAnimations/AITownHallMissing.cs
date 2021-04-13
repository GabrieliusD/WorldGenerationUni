using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITownHallMissing : AITownHallBaseState
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("missing", false);
    }
}
