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
        // counts priority for each command point
        for (int i = 0; i < cpInfos.Length; i++)
        {
            teamCPPoints = cpInfos[i].cpHandler.enemyPoint;
            enemyCPPoints = cpInfos[i].cpHandler.playerPoint;
            cpInfos[i].priority = Mathf.RoundToInt(maxCPPoints - teamCPPoints + enemyCPPoints);

            // assigns new targets if command point captured
            if (cpInfos[i].priority == 100)
            {
                AssignNewTarget(i);
            }
        }
    }
}
