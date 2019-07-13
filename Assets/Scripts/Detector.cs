using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Designed by      : Yosua M.
// Written by       : Yosua M.
// Documented by    : -

public class Detector : MonoBehaviour
{
    public GameObject player;
    private AIController enemy;

    private void Start()
    {
        enemy = GetComponentInParent<AIController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && player == null)
        {
            enemy.PlayerNear = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.PlayerNear = null;
        }
    }
}
