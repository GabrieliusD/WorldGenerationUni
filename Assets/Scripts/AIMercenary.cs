using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMercenary : MonoBehaviour
{
    AIStateManager AIState;
    public float percMinSoldiers = 40.0f;
    public int maxSoldiers = 5;
    int stage = 0;
    public int soldierIncreaseBetweenStages;
    public int defaultSoldiers = 5;
    public int soldierIncrease = 2;
    public float timeBetweenAttacks = 10;
    List<Soldier> recruitedSoldiers = new List<Soldier>();
    List<Soldier> idleSoldiers = new List<Soldier>(); 
    GameObject playerTownHall;
    GameObject aiTownHall;
    Mercenary mercenary;
    Animator animator;
    float timeElapsed;
    float timeElapsedRecruiting;
    float recruitFrequancy = 2.0f;

    public bool allowAttack {get; private set;} = false;
    private void Start()
    {
        soldierIncrease = maxSoldiers +2;
        AIState = AIStateManager.Instance;
        playerTownHall = GameObject.Find("TownHall(Clone)");
        aiTownHall = GameObject.Find("EnemyTownHall(Clone)");
        mercenary = GetComponent<Mercenary>();
        animator = GetComponent<Animator>();
        ConfigureMercenarySettings();
    }

    public void ConfigureMercenarySettings()
    {
        WorldSettings ws = WorldSettings.Instance;
        Difficulty diff = ws.GetDifficulty();
        defaultSoldiers = diff.aIParameters.startingUnitSize;
        soldierIncrease = diff.aIParameters.extraUnitsForDefence + defaultSoldiers;
        timeBetweenAttacks = diff.aIParameters.TimeBetweenAttacks;
        soldierIncreaseBetweenStages = diff.aIParameters.unitIncreaseBetweenTime;
    }

    public void addSoldierToTheList(Soldier soldier)
    {
        recruitedSoldiers.Add(soldier);
    }

    public void recruitSoldier()
    {
        timeElapsedRecruiting += Time.deltaTime;
        if(timeElapsedRecruiting >= recruitFrequancy)
        {
            mercenary.Interaction();
            timeElapsedRecruiting = 0;
        }
    }

    public int getRecruitedSoldiersCount()
    {
        return recruitedSoldiers.Count;
    }


    private void Update()
    {

        timeElapsed += Time.deltaTime;
        if(timeElapsed >= timeBetweenAttacks)
        {
            allowAttack = true;
            if(stage != 3)
            {
                stage++;
                //defaultSoldiers+=soldierIncreaseBetweenStages;
                //soldierIncrease+=soldierIncreaseBetweenStages;
            }
            timeElapsed = 0;
        }
        recruitedSoldiers.RemoveAll(item => item == null);
        float perc = ((float)recruitedSoldiers.Count / (float)maxSoldiers)*100;
        if(perc <= percMinSoldiers)
        {
            allowAttack = false;
        } 

        animator.SetBool("underAttack", AIState.GetState() == AIStates.UnderAttack ? true : false);
        if(AIState.GetState() == AIStates.UnderAttack)
        {
            maxSoldiers = soldierIncrease;
        } else{
            maxSoldiers = defaultSoldiers;
        }
        
        
    }
    void SendUnitsTo(List<Soldier> soldiers)
    {
        foreach (var s in soldiers)
        {
            if(AIState.GetState() == AIStates.Attack)
            s.IssuePath(playerTownHall.transform.position);
            if(AIState.GetState() == AIStates.Retreat)
            s.IssuePath(aiTownHall.transform.position);
        }    
    }

    public bool checkIfRetreated()
    {
        foreach(Soldier s in recruitedSoldiers)
        {
            float distance = Vector3.Distance(s.transform.position, aiTownHall.transform.position);
            if(distance < 40)
                return true;
        }
        return false;
    }

    public void SendSoldiersToAttack()
    {
        SendUnitsTo(recruitedSoldiers);
    }

    public bool findIdleSoldiers()
    {
        foreach(var s in recruitedSoldiers)
        {
            if(s.GetSoldierState() == Soldier.SoldierState.Idle)
            {
                idleSoldiers.Add(s);
            }
        }
        if(idleSoldiers.Count > 0) return true; else return false;
    }

    public void sendIdleSoldiersToCurrentAction()
    {

        
        SendUnitsTo(idleSoldiers);

        idleSoldiers.Clear();
    }
    
}
