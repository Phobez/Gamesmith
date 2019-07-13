using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Designed by      : Abia P.H., Yosua M.
// Written by       : Abia P.H., Yosua M.
// Documented by    : Abia P.H.

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
    public Transform[] PlayerSpawnPoint, EnemySpawnPoint;

    public List<CommandPointHandler> commandPoints;

    [SerializeField]
    private float maxScore = 0, scoringSpeed = 0;
    private float playerScore, enemyScore;

    [SerializeField]
    private GameObject winText, loseText;

    [SerializeField]
    private Text scoreText;

    // Start is called before the first frame update
    private void Start()
    {
        Time.timeScale = 1f;
        playerScore = enemyScore = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        CheckPoints();
        scoreText.text = "Score : " + ((int)playerScore).ToString();
        CheckWinner();
    }

    /// <summary>
    /// A method to return the appropriate spawn point.
    /// </summary>
    /// <returns>Transform of the spawn point.</returns>
    public Transform GetSpawnPoint(string tag)
    {
        // TO-DO: DYNAMICALLY RETURN APPROPRIATE SPAWN POINT DEPENDING ON TEAM
        //return spawnPoint;
        if (tag.Equals("PlayerTeam"))
            return PlayerSpawnPoint[Random.Range(0, PlayerSpawnPoint.Length - 1)];
        else if (tag.Equals("EnemyTeam"))
            return EnemySpawnPoint[Random.Range(0, EnemySpawnPoint.Length - 1)];
        return PlayerSpawnPoint[Random.Range(0, PlayerSpawnPoint.Length - 1)];
    }

    private void CheckPoints()
    {
        foreach (CommandPointHandler cp in commandPoints)
        {
            if (cp.state == CommandPointHandler.CommandPointState.PlayerOwned)
            {
                playerScore += Time.deltaTime * scoringSpeed;
            }
            if (cp.state == CommandPointHandler.CommandPointState.EnemyOwned)
            {
                enemyScore += Time.deltaTime * scoringSpeed;
            }
        }
    }

    private void CheckWinner()
    {
        
        //if(playerScore >= maxScore)
        //{
        //    AllyWinning();
        //    Time.timeScale = 0f;
        //}
        //if(enemyScore >= maxScore)
        //{
        //    EnemyWinning();
        //    Time.timeScale = 0f;
        //}
    }

    private void AllyWinning()
    {
        winText.SetActive(true);
    }

    private void EnemyWinning()
    {
        loseText.SetActive(true);
    }
}
