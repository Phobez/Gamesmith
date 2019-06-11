using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// A component controlling AI movement, shooting, and logic.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(WeaponManager))]
public class AIController : MonoBehaviour
{
    // TO-DO: DETECT ONLY ENEMY TEAM ENTITIES
    public LayerMask layerMask;             // target layer mask

    public float detectionRange = 10.0f;

    private Transform target;               // NavMesh target
    private NavMeshAgent agent;             // NavMeshAgent component reference
    private WeaponManager weaponManager;    // WeaponManager component reference
    private Weapon currentWeapon;           // currently equipped weapon

    // cached variables
    private Quaternion lookRotation;        // rotation to look at target
    private Vector3 offset;                 // vector distance between this and target
    private Vector3 direction;              // vector direction to target
    private RaycastHit hit;
    private float sqrDetectionRange;        // sqr of detection range
    private float sqrDistance;              // sqr of distance

    // Start is called before the first frame update
    private void Start()
    {
        target = GameController.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();

        weaponManager = GetComponent<WeaponManager>();

        sqrDetectionRange = detectionRange * detectionRange;
    }

    // Update is called once per frame
    private void Update()
    {
        offset = target.position - transform.position;
        sqrDistance = offset.sqrMagnitude;

        if (sqrDistance < sqrDetectionRange)
        {
            agent.SetDestination(target.position);
        }

        currentWeapon = weaponManager.GetCurrentWeapon();
        // stop moving when in range
        agent.stoppingDistance = currentWeapon.range;
    }

    /// <summary>
    /// A method to rotate the AI towards target over time.
    /// </summary>
    private void FaceTarget()
    {
        direction = (target.position - transform.position).normalized;
        direction.y = 0.0f;
        lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5.0f);
    }

    private void Shoot()
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
}
