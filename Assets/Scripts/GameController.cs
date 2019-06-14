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

    /// <summary>
    /// A method to return the appropriate spawn point.
    /// </summary>
    /// <returns>Transform of the spawn point.</returns>
    public Transform GetSpawnPoint()
    {
        // TO-DO: DYNAMICALLY RETURN APPROPRIATE SPAWN POINT DEPENDING ON TEAM
        return spawnPoint;
    }
}
