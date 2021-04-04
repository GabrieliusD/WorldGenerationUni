using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyState : StateMachineBehaviour
{
    public Soldier NPC;
	public Interactable opponent;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		NPC = animator.gameObject.GetComponent<Soldier>();
        opponent = NPC.GetInteractable();
	}

}

