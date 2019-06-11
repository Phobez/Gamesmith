using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
