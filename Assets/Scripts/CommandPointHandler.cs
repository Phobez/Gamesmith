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

    public string holderTag = GameController.NO_TEAM_TAG;

    private float playerPoin,enemyPoin;
    [SerializeField]
    private int maxPoint;
    [SerializeField]
    private float takingSpeed;
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
        else if(other.CompareTag("Enemy"))
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
        else if (other.CompareTag("Enemy"))
        {
            enemyNear.Remove(other.transform);
        }
    }

    private void CheckOwner()
    {
        if(playerPoin >= maxPoint)
        {
            state = CommandPointState.PlayerOwned;
        }
        if(enemyPoin >= maxPoint)
        {
            state = CommandPointState.EnemyOwned;
        }
        if(playerPoin < 1 && enemyPoin < 1)
        {
            state = CommandPointState.Neutral;
        }
    }

}
