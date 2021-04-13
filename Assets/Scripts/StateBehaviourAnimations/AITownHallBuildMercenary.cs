﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITownHallBuildMercenary : AITownHallBaseState
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateEnter(animator,animatorStateInfo, layerIndex);
        ai.build("mercenary", animator);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, animatorStateInfo, layerIndex);
    }
}
