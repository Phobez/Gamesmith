using UnityEngine;

// Designed by      : Abia P.H., Yosua M.
// Written by       : Abia P.H.
// Documented by    : Abia P.H.

/// <summary>
/// Allied AI commander behaviour.
/// </summary>
public class AllyAICommander : AICommander
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
            cpInfos[i].priority = Mathf.RoundToInt(maxCPPoints - enemyCPPoints + teamCPPoints);

            Debug.Log(cpInfos[i].cpHandler.gameObject.name + ": " + cpInfos[i].priority);

            if (cpInfos[i].priority < 100)
            {
                if (cpInfos[i].priority > cpInfos[tempHighestPriorityIndex].priority)
                {
                    tempHighestPriorityIndex = i;
                }
            }

            // assigns new targets if command point captured
            if (cpInfos[i].priority >= 100)
            {
                AssignNewTarget(i);
            }
        }

        highestPriorityIndex = tempHighestPriorityIndex;
    }
}
