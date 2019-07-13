using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Designed by      : Abia P.H., Yosua M.
// Written by       : Abia P.H.
// Documented by    : Abia P.H.

public class BaseStateMachineBehaviour : StateMachineBehaviour
{
    public enum AIState { MOVE, GUARD, FIGHT };

    protected GameObject entityAI = null;
    protected GameObject targetEntity = null;
    protected Transform transform = null;
    protected NavMeshAgent navMeshAgent = null;
    protected Animator animator = null;
    protected AIController aiController = null;

    public static Dictionary<AIState, string> aiStateParameters = new Dictionary<AIState, string>()
    {
        [AIState.MOVE] = "Move",
        [AIState.GUARD] = "Guard",
        [AIState.FIGHT] = "Fight"
    };

    protected bool enemyInSight = false;
    protected float detectionRange = 0.0f;
    protected float fieldOfViewAngle = 0.0f;
    protected string targetTag = "";

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        entityAI = animator.gameObject;
        transform = entityAI.GetComponent<Transform>();
        navMeshAgent = entityAI.GetComponent<NavMeshAgent>();
        this.animator = animator;
        aiController = entityAI.GetComponent<AIController>();

        detectionRange = aiController.detectionRange;
        fieldOfViewAngle = aiController.fieldOfViewAngle;
        targetTag = aiController.targetTag;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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

    /// <summary>
    /// Checks whether an enemy is in the field of view and line of sight.
    /// </summary>
    /// <param name="_collider">Collider of the detected entity.</param>
    //public bool CheckFieldOfView(Collider _collider)
    //{
    //    enemyInSight = false;

    //    Vector3 direction = _collider.transform.position - transform.position;
    //    float angle = Vector3.Angle(direction, transform.forward);

    //    if (angle < fieldOfViewAngle * 0.5f)
    //    {
    //        RaycastHit hit;

    //        if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, detectionRange))
    //        {
    //            if (hit.collider.gameObject.CompareTag(targetTag))
    //            {
    //                enemyInSight = true;
    //                if (targetEntity == null)
    //                {
    //                    targetEntity = hit.collider.gameObject;
    //                }
    //                animator.SetTrigger(aiStateParameters[AIState.FIGHT]);
    //                return true;
    //            }
    //        }
    //    }

    //    return false;
    //}
}
