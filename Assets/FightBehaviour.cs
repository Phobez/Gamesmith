using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightBehaviour : BaseStateMachineBehaviour
{
    public float shootTimerMin = 0.2f;
    public float shootTimerMax = 0.7f;

    private Transform targetCover = null;

    private LayerMask coverPointsLayer = LayerMask.NameToLayer("CoverPoint");
    private Vector3 position = Vector3.zero;

    private bool hasFoundCover = false;
    private bool isAtCover = false;
    private float shootTimer = 0.0f;

    // cached variables
    private Vector3 offset = Vector3.zero;
    private float sqrDistance = 0.0f;
    private float sqrDetectionRange = 0.0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        position = transform.position;
        navMeshAgent.stoppingDistance = 0.0f;
        sqrDetectionRange = aiController.detectionRange * aiController.detectionRange;

        targetCover = GoToCover();

        if (targetCover != null)
        {
            hasFoundCover = true;
            navMeshAgent.SetDestination(targetCover.position);
        }
        else
        {
            hasFoundCover = false;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        offset = targetEntity.transform.position - transform.position;
        sqrDistance = offset.sqrMagnitude;

        if (sqrDistance > sqrDetectionRange)
        {
            Collider[] _hitEnemies = Physics.OverlapSphere(Vector3.zero, aiController.detectionRange, aiController.layerMask);
            if (_hitEnemies.Length > 0)
            {
                targetEntity = _hitEnemies[0].gameObject;
            }
            else
            {
                animator.SetTrigger(aiStateParameters[AIState.MOVE]);
            }
        }

        aiController.FaceTarget(targetEntity.transform);

        if (hasFoundCover && !isAtCover)
        {
            if (navMeshAgent.remainingDistance <= 0.0f)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0.0f)
                {
                    isAtCover = true;
                }
            }
        }
        else
        {
            if (shootTimer <= 0)
            {
                aiController.Shoot();
                shootTimer = Random.Range(shootTimerMin, shootTimerMax);
            }
            else
            {
                shootTimer -= Time.deltaTime;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}

    private Transform GoToCover()
    {
        Collider[] _hitCovers = Physics.OverlapSphere(Vector3.zero, 10.0f, coverPointsLayer, QueryTriggerInteraction.Collide);

        if (_hitCovers.Length > 0)
        {
            Transform _closestCover = null;
            float _closestSqrDistance = 0.0f;

            Transform _currentCover = null;
            float _currentSqrDistance = 0.0f;
            Vector3 _offset = Vector3.zero;

            foreach (Collider _cover in _hitCovers)
            {
                _currentCover = _cover.GetComponent<Transform>();

                _offset = _currentCover.position - position;
                _currentSqrDistance = _offset.sqrMagnitude;

                if (_currentSqrDistance > _closestSqrDistance)
                {
                    _closestSqrDistance = _currentSqrDistance;
                    _closestCover = _currentCover;
                }
            }

            return _closestCover;
        }
        else
        {
            return null;
        }
    }
}
