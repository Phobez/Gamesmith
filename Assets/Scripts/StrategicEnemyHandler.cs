using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Designed by      : Abia P.H., Yosua M.
// Written by       : Yosua M.
// Documented by    : -

public class StrategicEnemyHandler : MonoBehaviour
{
    #region Singleton
    public static StrategicEnemyHandler instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject[] commandPoints,Players;
    public AIController[] enemies;

    private GameObject priority;

    //private void Awake()
    //{
    //    enemies[0].target = commandPoints[0].transform;
    //    enemies[1].target = commandPoints[0].transform;
    //    enemies[2].target = commandPoints[1].transform;
    //    enemies[3].target = commandPoints[1].transform;
    //    enemies[4].target = commandPoints[2].transform;
    //}

    private void CheckPriority()
    {

    }

    public GameObject AssignTarget()
    {

        return commandPoints[0];
    }
    
}
