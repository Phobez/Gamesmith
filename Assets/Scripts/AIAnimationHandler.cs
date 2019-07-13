using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIAnimationHandler : MonoBehaviour
{
    private Animator anim;
    private NavMeshAgent navmesh;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        navmesh = GetComponentInParent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", navmesh.speed);
    }
}
