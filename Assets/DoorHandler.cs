using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    private Animator anim;
    [SerializeField]
    private GameObject owner;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(owner.tag))
        {
            anim.SetTrigger("BeingOpen");
        }
    }
}
