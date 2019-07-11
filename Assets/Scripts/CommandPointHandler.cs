using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandPointHandler : MonoBehaviour
{
    public enum CommandPointState
    {
        Neutral,
        PlayerOwned,
        EnemyOwned
    }

    [SerializeField]
    private GameObject commandPointSphere;
    [SerializeField]
    private Material neutralSphere, enemySphere, playerSphere;
    public string holderTag = GameController.NO_TEAM_TAG;

    private float playerPoin,enemyPoin;
    [SerializeField]
    private int maxPoint = 0;
    [SerializeField]
    private float takingSpeed = 0;
    public Transform[] waypoints;
    private List<Transform> playerNear, enemyNear;
    public CommandPointState state;
    

    // Start is called before the first frame update
    void Start()
    {
        playerNear = new List<Transform>();
        enemyNear = new List<Transform>();
        playerPoin = enemyPoin = 0;
        state = CommandPointState.Neutral;
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckOwner();
        AddPoint();
    }

    private void AddPoint()
    {
        if(playerNear.Count != 0 && playerPoin <= maxPoint && enemyPoin <= 0)
        {
            foreach(Transform player in playerNear)
            {
                playerPoin += Time.deltaTime * takingSpeed;
            }
        }
        else if (playerNear.Count != 0 && playerPoin <= maxPoint && enemyPoin > 0)
        {
            foreach (Transform player in playerNear)
            {
                enemyPoin -= Time.deltaTime * takingSpeed;
            }
        }
        if(enemyNear.Count != 0 && playerPoin >= -maxPoint && playerPoin <= 0)
        {
            foreach (Transform enemy in enemyNear)
            {
                enemyPoin += Time.deltaTime * takingSpeed;
            }
        }
        else if(enemyNear.Count != 0 && playerPoin >= -maxPoint && playerPoin <= 0)
        {
            foreach (Transform enemy in playerNear)
            {
                playerPoin -= Time.deltaTime * takingSpeed;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerNear.Add(other.transform);
        }
        else if(other.CompareTag("EnemyTeam"))
        {
            enemyNear.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear.Remove(other.transform);
        }
        else if (other.CompareTag("EnemyTeam"))
        {
            enemyNear.Remove(other.transform);
        }
    }

    private void CheckOwner()
    {
        if(playerPoin >= maxPoint)
        {
            state = CommandPointState.PlayerOwned;
            commandPointSphere.GetComponent<Renderer>().material = playerSphere;
        }
        if(enemyPoin >= maxPoint)
        {
            state = CommandPointState.EnemyOwned;
            commandPointSphere.GetComponent<Renderer>().material = enemySphere;
        }
        if(playerPoin < 1 && enemyPoin < 1)
        {
            state = CommandPointState.Neutral;
            commandPointSphere.GetComponent<Renderer>().material = neutralSphere;
        }
    }

}
