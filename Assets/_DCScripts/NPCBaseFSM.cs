using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBaseFSM : StateMachineBehaviour {

	public GameObject NPC;
	public GameObject opponent;
	public float speed = 2.0f;
	public float rotSpeed = 0.1f;
	public float accuracy = 3.0f;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		NPC = animator.gameObject;
		opponent = NPC.GetComponent<TankAI>().GetPlayer();
	}

    public enum FacingDirection
    {
        UP = 270,
        DOWN = 90,
        LEFT = 180,
        RIGHT = 0
    }

    public static Quaternion FaceObject(Vector2 startingPosition, Vector2 targetPosition, FacingDirection facing)
    {
        Vector2 direction = targetPosition - startingPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle -= (float)facing;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
