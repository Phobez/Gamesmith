using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategicEnemyHandler : MonoBehaviour
{
    #region Singleton
    public static StrategicEnemyHandler instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField]
    private GameObject[] commandPoints,Players;

    private GameObject priority;

    private void Update()
    {
        
    }

    private void CheckPriority()
    {

    }

    public GameObject AssignTarget()
    {

        return commandPoints[0];
    }
    
}
