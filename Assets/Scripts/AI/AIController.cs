using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Designed by      : Abia P.H., Yosua M.
// Written by       : Abia P.H.
// Documented by    : Abia P.H.

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
    public NavMeshAgent agent;             // NavMeshAgent component reference
    public AICommander commander;
    private Animator animator;
    private WeaponManager weaponManager;    // WeaponManager component reference
    private Entity entity;                  // Entity component reference
    public Weapon currentWeapon;           // currently equipped weapon

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
    public ParticleSystem muzzleFlash;
    //private EnemyState state;
    private bool hasEnteredFSM = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        strategicEnemy = FindObjectOfType<StrategicEnemyHandler>();
        
        animator = GetComponent<Animator>();

        weaponManager = GetComponent<WeaponManager>();
        entity = GetComponent<Entity>();
        entity.SetUp();

        sqrDetectionRange = detectionRange * detectionRange;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!hasEnteredFSM)
        {
            if (target != null)
            {
                animator.SetTrigger("Enter");
                hasEnteredFSM = true;
            }
        }

        currentWeapon = weaponManager.GetCurrentWeapon();

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

    private void OnEnable()
    {
        if (commander != null)
        {
            commander.GetNewTarget(this);
        }
        if (animator != null)
        {
            animator.SetTrigger(BaseStateMachineBehaviour.aiStateParameters[BaseStateMachineBehaviour.AIState.MOVE]);
        }
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
            Debug.Log("Bullets: " + currentWeapon.bullets);
            return;
        }

        muzzleFlash.Play();
        currentWeapon.bullets--;
        GetComponent<AudioSource>().Play();
        if (Physics.Raycast(transform.position, transform.forward, out hit, currentWeapon.range, layerMask))
        {
            if (hit.transform.gameObject.CompareTag(targetTag))
            {
                hit.transform.GetComponent<Entity>().TakeDamage(currentWeapon.damage);
            }
        }

        if (currentWeapon.bullets <= 0)
        {
            weaponManager.Reload();
        }
    }

    public void Crouch()
    {
        entity.Crouch();
    }

    public void StandUp()
    {
        entity.StandUp();
    }

    // PLAYER COMMAND METHODS
    public void Follow(Transform _target)
    {
        animator.SetTrigger(BaseStateMachineBehaviour.aiStateParameters[BaseStateMachineBehaviour.AIState.MOVE]);
        target = _target;
        agent.SetDestination(target.position);
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
        agent.SetDestination(_target.position);
        animator.SetTrigger(BaseStateMachineBehaviour.aiStateParameters[BaseStateMachineBehaviour.AIState.MOVE]);
    }

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, detectionRange);
    //}

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
            //animator.GetBehaviour<FightBehaviour>().CheckFieldOfView(other);
            CheckFieldOfView(other);
        }
    }

    public bool CheckFieldOfView(Collider _collider)
    {
        //enemyInSight = false;
        GameObject targetEntity = null;

        Vector3 direction = _collider.transform.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);

        if (angle < fieldOfViewAngle * 0.5f)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, detectionRange, layerMask))
            {
                if (hit.collider.gameObject.CompareTag(targetTag))
                {
                    //enemyInSight = true;
                    if (targetEntity == null)
                    {
                        targetEntity = hit.collider.gameObject;
                    }
                    animator.GetBehaviour<FightBehaviour>().SetTargetEntity(targetEntity);
                    animator.SetTrigger(BaseStateMachineBehaviour.aiStateParameters[BaseStateMachineBehaviour.AIState.FIGHT]);
                    
                    return true;
                }
            }
        }

        return false;
    }
}
