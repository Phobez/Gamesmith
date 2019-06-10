using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    private int enemyHealth = 10;

    // Update is called once per frame
    private void Update()
    {
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void DeductPoints(int damageAmount)
    {
        enemyHealth -= damageAmount;
        Debug.Log(enemyHealth);
    }
}
