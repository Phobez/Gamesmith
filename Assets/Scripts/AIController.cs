using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// A component controlling AI movement, shooting, and logic.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(WeaponManager))]
public class AIController : MonoBehaviour
{
    //private enum EnemyState
    //{
    //    GoToCommandPoint,
    //    Attacking,
    //    DefendCommandPoint
    //}



    // TO-DO: DETECT ONLY ENEMY TEAM ENTITIES
    public LayerMask layerMask;             // target layer mask

    public float detectionRange = 10.0f;
    public float fieldOfViewAngle = 85.0f;
    public string aiTeamTag = GameController.NO_TEAM_TAG;
    public string targetTag = GameController.NO_TEAM_TAG;

    public Transform target;               // NavMesh target
    private NavMeshAgent agent;             // NavMeshAgent component reference
    private Animator animator;
    private WeaponManager weaponManager;    // WeaponManager component reference
    private Weapon currentWeapon;           // currently equipped weapon

    // cached variables
    private Quaternion lookRotation;        // rotation to look at target
    private Vector3 offset;                 // vector distance between this and target
    private Vector3 direction;              // vector direction to target
    private RaycastHit hit;
    private float sqrDetectionRange;        // sqr of detection range
    private float sqrDistance;              // sqr of distance

    private StrategicEnemyHandler strategicEnemy;
    public GameObject PlayerNear;
    private float stateCheckCD; //cooldown for state checking 
    //private EnemyState state;

    // Start is called before the first frame update
    private void Start()
    {
        strategicEnemy = FindObjectOfType<StrategicEnemyHandler>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        weaponManager = GetComponent<WeaponManager>();

        sqrDetectionRange = detectionRange * detectionRange;
    }

    // Update is called once per frame
    private void Update()
    {
        //offset = target.position - transform.position;
        //sqrDistance = offset.sqrMagnitude;

        //if (sqrDistance < sqrDetectionRange)
        //{
        //    agent.SetDestination(target.position);
        //}

        //stateCheckCD -= Time.deltaTime;
        //if(stateCheckCD <= 0)
        //{
        //    CheckState();
        //    stateCheckCD = Random.Range(0.2f,0.7f);
        //}

        //FaceTarget();
        //currentWeapon = weaponManager.GetCurrentWeapon();
        //// stop moving when in range
        //if (target.CompareTag("Player"))
        //    agent.stoppingDistance = currentWeapon.range;
        //else
        //    agent.stoppingDistance = 2;


    }

    /// <summary>
    /// A method to rotate the AI towards target over time.
    /// </summary>
    public void FaceTarget()
    {
        direction = (target.position - transform.position).normalized;
        direction.y = 0.0f;
        lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5.0f);
    }

    public void FaceTarget(Transform _target)
    {
        direction = (_target.position - transform.position).normalized;
        direction.y = 0.0f;
        lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5.0f);
    }

    public void Shoot()
    {
        if (weaponManager.isReloading)
        {
            return;
        }

        if (currentWeapon.bullets <= 0)
        {
            weaponManager.Reload();
            return;
        }

        currentWeapon.bullets--;
        GetComponent<AudioSource>().Play();
        if (Physics.Raycast(transform.position, transform.forward, out hit, currentWeapon.range, layerMask))
        {
            // TO-DO: CHECK IF ENEMY TEAM, NOT SPECIFICALLY PLAYER
            if (hit.transform.gameObject.CompareTag("Player"))
            {
                hit.transform.GetComponent<Entity>().TakeDamage(currentWeapon.damage);
            }
        }

        if (currentWeapon.bullets <= 0)
        {
            weaponManager.Reload();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    //private void CheckState()
    //{

    //    switch(state)
    //    {
    //        case EnemyState.GoToCommandPoint:
    //            if (PlayerNear != null)
    //            {
    //                target = PlayerNear.transform;
    //                agent.SetDestination(PlayerNear.transform.position);
    //                state = EnemyState.Attacking;
    //            }
    //            if (target == null)
    //            {
    //                target = strategicEnemy.AssignTarget().transform;
    //                agent.SetDestination(target.position);
    //            }
    //            break;
    //        case EnemyState.DefendCommandPoint:

    //            //defend

    //            break;
    //        case EnemyState.Attacking:
    //            if(PlayerNear != null && target.CompareTag("Player"))
    //            {
    //                Shoot();
    //            }
    //            else
    //            {
    //                target = strategicEnemy.AssignTarget().transform;
    //                agent.SetDestination(target.position);
    //                state = EnemyState.GoToCommandPoint;
    //            }

    //            break;
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        // transition to FIGHT state
        if (other.gameObject.CompareTag(targetTag))
        {
            animator.GetBehaviour<MoveBehaviour>().CheckFieldOfView(other);
        }
    }
}
