using UnityEngine;

// Designed by      : Abia P.H., Yosua M.
// Written by       : Abia P.H.
// Documented by    : Abia P.H.

/// <summary>
/// Allied Enemy AI commander behaviour.
/// </summary>
public class EnemyAICommander : AICommander
{
    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    private void Update()
    {
        int tempPriority = 0;

        // counts priority for each command point
        for (int i = 0; i < cpInfos.Length; i++)
        {
            tempPriority = cpInfos[i].priority;

            teamCPPoints = cpInfos[i].cpHandler.enemyPoint;
            enemyCPPoints = cpInfos[i].cpHandler.playerPoint;

            tempPriority = Mathf.RoundToInt(maxCPPoints - enemyCPPoints + teamCPPoints);

            if (tempPriority < 100)
            {
                cpInfos[i].priority = tempPriority;
            }
            else
            {
                cpInfos[i].priority = -1;
            }

            Debug.Log(cpInfos[i].cpHandler.gameObject.name + ": " + cpInfos[i].priority);

            if (cpInfos[i].priority > cpInfos[tempHighestPriorityIndex].priority)
            {
                tempHighestPriorityIndex = i;
            }
        }

        highestPriorityIndex = tempHighestPriorityIndex;

        for (int i = 0; i < cpInfos.Length; i++)
        {
            if (cpInfos[i].priority == -1)
            {
                AssignNewTarget(i);
            }
        }
    }
}
