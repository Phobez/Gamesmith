using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<CommandPointHandler> commandPoints;

    [SerializeField]
    private float maxPoin, scoringSpeed;
    [SerializeField]
    private float playerPoin, enemyPoin;

    // Start is called before the first frame update
    void Start()
    {
        playerPoin = enemyPoin = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CheckPoin();
    }

    private void CheckPoin()
    {
        foreach(CommandPointHandler cp in commandPoints)
        {
            if(cp.state == CommandPointHandler.CommandPointState.PlayerOwned)
            {
                playerPoin += Time.deltaTime * scoringSpeed;
            }
            if (cp.state == CommandPointHandler.CommandPointState.EnemyOwned)
            {
                enemyPoin += Time.deltaTime * scoringSpeed;
            }
        }
    }
}
