using System.Collections.Generic;
using UnityEngine;

// Designed by      : Abia P.H., Yosua M.
// Written by       : Abia P.H.
// Documented by    : Abia P.H.

/// <summary>
/// Base AI Commander component.
/// </summary>
public class AICommander : MonoBehaviour
{
    public int maxCPPoints = 50;                                            // max CP points possible for a team
    public CommandPointHandler[] cpHandlers = new CommandPointHandler[3];   // array of CP
    public List<AIController> soldiers = new List<AIController>();          // array of team soldiers under AI command

    //protected Dictionary<AIController, int> soldierAssignments = new Dictionary<AIController, int>();
    protected List<SoldierAssignment> soldierAssignments;
    protected CommandPointInfo[] cpInfos;   // array of info for each CP
    protected int highestPriorityIndex = 0; // index of CP with highest priority

    // cached variables
    protected float teamCPPoints = 0;     // CP points for team at a CP
    protected float enemyCPPoints = 0;    // CP points for enemy team at a CP
    protected int tempPriority = 0;     // temporary priority value
    protected int tempHighestPriorityIndex = 0;

    // Start is called before the first frame update
    protected void Start()
    {
        cpInfos = new CommandPointInfo[3];

        // set up command point info
        for (int i = 0; i < cpInfos.Length; i++)
        {
            cpInfos[i].cpHandler = cpHandlers[i];
            cpInfos[i].priority = 0;
            cpInfos[i].assignedSoldiers = new List<AIController>();
            //cpInfos[i].assignedSoldiers = new List<AIController>();
        }

        int tempIndex = 0;
        SoldierAssignment tempSoldierAssignment;

        soldierAssignments = new List<SoldierAssignment>();

        // assign soldiers semi-random initial target
        for (int i = 0; i < soldiers.Count; i++)
        {
            soldiers[i].commander = this;

            do
            {
                tempIndex = Random.Range(0, 3);
            } while (cpInfos[tempIndex].assignedSoldiers.Count >= 2);

            //cpInfos[tempIndex].assignedSoldiers.Add(soldiers[i]);
            //soldierAssignments.Add(soldiers[i], tempIndex);
            tempSoldierAssignment = new SoldierAssignment();
            tempSoldierAssignment.soldier = soldiers[i];
            tempSoldierAssignment.targetIndex = tempIndex;
            soldierAssignments.Add(tempSoldierAssignment);
            soldiers[i].target = cpInfos[tempIndex].cpHandler.transform;
            soldiers[i].agent.SetDestination(soldiers[i].target.position);
        }
    }

    /// <summary>
    /// Assign new target to soldiers with target index.
    /// </summary>
    /// <param name="_index">Current target index.</param>
    public void AssignNewTarget(int _index)
    {
        // iterate through dictionary
        // reassign _index-target soldiers to new target
        //foreach (KeyValuePair<AIController, int> soldierAssignment in soldierAssignments)
        //{
        //    if (soldierAssignment.Value == _index)
        //    {
        //        soldierAssignments[soldierAssignment.Key] = _index;
        //        soldierAssignment.Key.target = cpInfos[highestPriorityIndex].cpHandler.transform;
        //    }
        //}

        SoldierAssignment tempSoldierAssignment;

        for (int i = 0; i < soldierAssignments.Count; i++)
        {
            if (soldierAssignments[i].targetIndex == _index)
            {
                soldierAssignments[i].soldier.SetTarget(cpInfos[highestPriorityIndex].cpHandler.transform);
                tempSoldierAssignment = soldierAssignments[i];
                tempSoldierAssignment.targetIndex = highestPriorityIndex;
                soldierAssignments[i] = tempSoldierAssignment;
            }
        }
    }

    /// <summary>
    /// Gets new target on soldier death.
    /// </summary>
    /// <param name="_soldier">Soldier requesting new target.</param>
    public void GetNewTarget(AIController _soldier)
    {
        SoldierAssignment tempSoldierAssignment;

        //soldierAssignments[_soldier] = highestPriorityIndex;
        //_soldier.target = cpInfos[highestPriorityIndex].cpHandler.transform;
        for (int i = 0; i < soldierAssignments.Count; i++)
        {
            if (soldierAssignments[i].soldier == _soldier)
            {
                soldierAssignments[i].soldier.SetTarget(cpInfos[highestPriorityIndex].cpHandler.transform);
                tempSoldierAssignment = soldierAssignments[i];
                tempSoldierAssignment.targetIndex = highestPriorityIndex;
                soldierAssignments[i] = tempSoldierAssignment;
            }
        }
    }
}

/// <summary>
/// Contains Command Point information for AI Commanders.
/// </summary>
public struct CommandPointInfo
{
    public CommandPointHandler cpHandler;
    public int priority;
    public List<AIController> assignedSoldiers;
}

public struct SoldierAssignment
{
    public AIController soldier;
    public int targetIndex;
}
