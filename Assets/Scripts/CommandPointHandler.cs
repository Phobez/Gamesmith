using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandPointHandler : MonoBehaviour
{
    private int points;
    [SerializeField]
    private int maxPoint;
    [SerializeField]
    private Transform[] waypoints;
    private List<Transform> PlayerNear, EnemyNear;

    // Start is called before the first frame update
    void Start()
    {
        points = 0;        
    }

    // Update is called once per frame
    void Update()
    {
        AddPoint();
    }

    private void AddPoint()
    {
        if(PlayerNear.Count != 0 && points <= maxPoint)
        {
            foreach(Transform player in PlayerNear)
            {
                points += 1;
            }
        }
        if(EnemyNear.Count != 0 && points >= -maxPoint)
        {
            foreach (Transform player in EnemyNear)
            {
                points -= 1;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerNear.Add(other.transform);
        }
        else if(other.CompareTag("Enemy"))
        {
            EnemyNear.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerNear.Remove(other.transform);
        }
        else if (other.CompareTag("Enemy"))
        {
            EnemyNear.Remove(other.transform);
        }
    }

}
