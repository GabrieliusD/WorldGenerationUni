using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIStates{Idle, Attack, Retreat, UnderAttack, None};

public class AIStateManager
{
    static AIStates aiCurrentState = AIStates.Idle;
    public AIStateManager()
    {
        if(Instance == null)
        Instance = this;
    }
    public static AIStateManager Instance{get; private set;}


    public AIStates GetState()
    {
        return aiCurrentState;
    }
    public void SetState(AIStates state)
    {
        aiCurrentState = state;
    }
}
