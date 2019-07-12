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
        // counts priority for each command point
        for (int i = 0; i < cpInfos.Length; i++)
        {
            teamCPPoints = cpInfos[i].cpHandler.playerPoint;
            enemyCPPoints = cpInfos[i].cpHandler.enemyPoint;
            cpInfos[i].priority = Mathf.RoundToInt(maxCPPoints - enemyCPPoints + teamCPPoints);

            // assigns new targets if command point captured
            if (cpInfos[i].priority == 100)
            {
                AssignNewTarget(i);
            }
        }
    }
}
