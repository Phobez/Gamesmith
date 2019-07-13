using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Designed by      : Abia P.H., Yosua M.
// Written by       : Yosua M.
// Documented by    : -

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

    public float playerPoint,enemyPoint;
    [SerializeField]
    private int maxPoint = 0;
    [SerializeField]
    private float takingSpeed = 0;
    public Transform[] waypoints;
    private List<Transform> playerNear, enemyNear;
    public CommandPointState state;

    private SphereCollider col;
    

    // Start is called before the first frame update
    void Start()
    {
        playerNear = new List<Transform>();
        enemyNear = new List<Transform>();
        col = GetComponent<SphereCollider>();
        playerPoint = enemyPoint = 0;
        state = CommandPointState.Neutral;
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckEntities();
        CheckOwner();
        AddPoint();
    }

    private void AddPoint()
    {
        if (playerNear.Count != 0 && playerPoint <= maxPoint && enemyPoint <= 0)
        {
            foreach (Transform player in playerNear)
            {
                playerPoint += Time.deltaTime * takingSpeed;
            }
        }
        else if (playerNear.Count != 0 && playerPoint <= maxPoint && enemyPoint > 0)
        {
            foreach (Transform player in playerNear)
            {
                enemyPoint -= Time.deltaTime * takingSpeed;
            }
        }
        if (enemyNear.Count != 0 && enemyPoint <= maxPoint && playerPoint <= 0)
        {
            foreach (Transform enemy in enemyNear)
            {
                enemyPoint += Time.deltaTime * takingSpeed;
            }
        }
        else if (enemyNear.Count != 0 && enemyPoint <= maxPoint && playerPoint > 0)
        {
            foreach (Transform enemy in playerNear)
            {
                playerPoint -= Time.deltaTime * takingSpeed;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerTeam"))
        {
            playerNear.Add(other.transform);
        }
        else if(other.CompareTag("EnemyTeam"))
        {
            enemyNear.Add(other.transform);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerTeam"))
        {
            if (!playerNear.Contains(other.transform))
            {
                playerNear.Add(other.transform);
            }
        }
        else if (other.CompareTag("EnemyTeam"))
        {
            if (!enemyNear.Contains(other.transform))
            {
                enemyNear.Add(other.transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerTeam"))
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
        if(playerPoint >= maxPoint)
        {
            state = CommandPointState.PlayerOwned;
            commandPointSphere.GetComponent<Renderer>().material = playerSphere;
            holderTag = GameController.PLAYER_TEAM_TAG;
        }
        else if(enemyPoint >= maxPoint)
        {
            state = CommandPointState.EnemyOwned;
            commandPointSphere.GetComponent<Renderer>().material = enemySphere;
            holderTag = GameController.NO_TEAM_TAG;
        }
        else //(playerPoint < 50 && enemyPoint < 50)
        {
            state = CommandPointState.Neutral;
            commandPointSphere.GetComponent<Renderer>().material = neutralSphere;
            holderTag = GameController.ENEMY_TEAM_TAG;
        }
    }

    private Vector3 offset;
    private float sqrDistance;

    private void CheckEntities()
    {
        if (playerNear.Count > 0)
        {
            for (int i = 0; i < playerNear.Count; i++)
            {
                offset = playerNear[i].transform.position - transform.position;
                sqrDistance = offset.sqrMagnitude;

                if (sqrDistance > col.radius * col.radius)
                {
                    playerNear.Remove(playerNear[i]);
                }
            }
        }

        if (enemyNear.Count > 0)
        {
            for (int i = 0; i < enemyNear.Count; i++)
            {
                offset = enemyNear[i].transform.position - transform.position;
                sqrDistance = offset.sqrMagnitude;

                if (sqrDistance > col.radius * col.radius)
                {
                    enemyNear.Remove(enemyNear[i]);
                }
            }
        }
    }
}
