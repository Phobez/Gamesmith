using UnityEngine;
using UnityEngine.AI;

// Designed by      : Abia P.H.
// Written by       : Abia P.H.
// Documented by    : Abia P.H.

// TEMPORARY SCRIPT
// REMOVE LATER
public class MoveTo : MonoBehaviour
{
    public Transform goal;

    // Start is called before the first frame update
    void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
