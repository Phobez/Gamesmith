using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Designed by      : Abia P.H., Yosua M.
// Written by       : Abia P.H.
// Documented by    : Abia P.H.

public class GuardBehaviour : BaseStateMachineBehaviour
{
    public CommandPointHandler cpHandler = null;

    public float guardStoppingDistance = 0.0f;
    public float guardDelayTime = 5.0f;

    private Transform commandPoint = null;

    private bool guardingWaypoint = false;
    private bool atWaypoint = false;
    private int currentWaypointIndex = -1;

    // cached variables and references
    private Transform targetWaypoint = null;
    private WaitForSeconds guardDelay;
    private int waypointsLength = 0;
    private int randomWaypointIndex = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        navMeshAgent.stoppingDistance = 0.0f;
        commandPoint = aiController.target;
        cpHandler = aiController.target.gameObject.GetComponent<CommandPointHandler>();
        waypointsLength = cpHandler.waypoints.Length;

        guardDelay = new WaitForSeconds(guardDelayTime);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // TRANSITION TO MOVE STATE
        if (aiController.target != commandPoint)
        {
            animator.SetTrigger(aiStateParameters[AIState.MOVE]);
            return;
        }

        if (cpHandler.holderTag != aiController.aiTeamTag)
        {
            if (!guardingWaypoint)
            {
                if (currentWaypointIndex < 0)
                {
                    currentWaypointIndex = Random.Range(0, waypointsLength - 1);
                }
                else
                {
                    do
                    {
                        randomWaypointIndex = Random.Range(0, waypointsLength - 1);
                    } while (randomWaypointIndex == currentWaypointIndex);

                    currentWaypointIndex = randomWaypointIndex;
                }

                guardingWaypoint = true;

                targetWaypoint = cpHandler.waypoints[currentWaypointIndex];
                navMeshAgent.SetDestination(targetWaypoint.position);
            }
            else
            {
                if (!atWaypoint)
                {
                    if (navMeshAgent.remainingDistance <= guardStoppingDistance)
                    {
                        if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0.0f)
                        {
                            atWaypoint = true;
                            GuardWaypoint();
                        }
                    }
                }
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

    private IEnumerator GuardWaypoint()
    {
        yield return guardDelay;
        guardingWaypoint = false;
        atWaypoint = false;
    }
}
