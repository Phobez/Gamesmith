using UnityEngine;

public class GameController : MonoBehaviour
{
    public MatchSettings matchSettings;

    public Transform spawnPoint;

    public static GameController instance = null;

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

    public Transform GetSpawnPoint()
    {
        return spawnPoint;
    }
}
