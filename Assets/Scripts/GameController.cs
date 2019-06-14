using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A component to control game logic.
/// </summary>
public class GameController : MonoBehaviour
{
    #region Singleton
    public static GameController instance = null;   // static GameController instance

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("More than one GameController in scene.");
        }
    }
    #endregion

    public static string PLAYER_TEAM_TAG = "PlayerTeam";
    public static string ENEMY_TEAM_TAG = "EnemyTeam";
    public static string NO_TEAM_TAG = "NoTeam";

    public GameObject player;

    public MatchSettings matchSettings;

    // TO-DO: CREATE MULTIPLE SPAWN POINTS FOR EACH TEAM
    //        OR CREATE ONE SPAWN POINT FOR EACH TEAM
    public Transform spawnPoint;

    [SerializeField]
    private List<CommandPointHandler> commandPoints;

    [SerializeField]
    private float maxPoin, scoringSpeed;
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

    /// <summary>
    /// A method to return the appropriate spawn point.
    /// </summary>
    /// <returns>Transform of the spawn point.</returns>
    public Transform GetSpawnPoint()
    {
        // TO-DO: DYNAMICALLY RETURN APPROPRIATE SPAWN POINT DEPENDING ON TEAM
        return spawnPoint;
    }

    private void CheckPoin()
    {
        foreach (CommandPointHandler cp in commandPoints)
        {
            if (cp.state == CommandPointHandler.CommandPointState.PlayerOwned)
            {
                playerPoin += Time.deltaTime * scoringSpeed;
            }
            if (cp.state == CommandPointHandler.CommandPointState.EnemyOwned)
            {
                enemyPoin += Time.deltaTime * scoringSpeed;
            }
        }
    }

    private void CheckWinner()
    {
        if(playerPoin >= maxPoin)
        {
            //player win
        }
        if(enemyPoin >= maxPoin)
        {
            //enemy win
        }
    }
}
