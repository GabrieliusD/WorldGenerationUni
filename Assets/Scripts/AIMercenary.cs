using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIStates{Idle, Attack, Retreat, None};
public class AIMercenary : MonoBehaviour
{
    AIStates aiStates = AIStates.Idle;
    public float percMinSoldiers = 40.0f;
    public int maxSoldiers = 5;
    public float timeBetweenAttacks = 10;
    List<Soldier> recruitedSoldiers = new List<Soldier>();
    List<Soldier> idleSoldiers = new List<Soldier>(); 
    GameObject playerTownHall;
    GameObject aiTownHall;
    Mercenary mercenary;
    float timeElapsed;
    float timeElapsedRecruiting;
    float recruitFrequancy = 2.0f;

    public bool allowAttack {get; private set;} = false;
    private void Start()
    {
        playerTownHall = GameObject.Find("TownHall(Clone)");
        aiTownHall = GameObject.Find("EnemyTownHall(Clone)");
        mercenary = GetComponent<Mercenary>();
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
            //timeElapsed = 0;
        }
        recruitedSoldiers.RemoveAll(item => item == null);
        float perc = ((float)recruitedSoldiers.Count / (float)maxSoldiers)*100;
        if(perc <= percMinSoldiers)
        {
            allowAttack = false;
        } 
        
        
    }
    void SendUnitsTo()
    {
        foreach (var s in recruitedSoldiers)
        {
            if(aiStates == AIStates.Attack)
            s.IssuePath(playerTownHall.transform.position);
            if(aiStates == AIStates.Retreat)
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
        SendUnitsTo();
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
        foreach (var s in idleSoldiers)
        {
            SendUnitsTo();
        }
        idleSoldiers.Clear();
    }

    public void SetAIState(AIStates state)
    {
        aiStates = state;
    }
    

}
