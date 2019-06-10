using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGunDamage : MonoBehaviour
{
    [SerializeField]
    private int damageAmount = 5;
    [SerializeField]
    private float allowedRange = 15;

    private float targetDistance;

    private RaycastHit shot;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out shot))
            {
                targetDistance = shot.distance;
                Debug.Log(targetDistance);

                if (targetDistance < allowedRange)
                {
                    shot.transform.gameObject.GetComponent<EnemyScript>().DeductPoints(damageAmount);
                }
            }
        }
    }
}
