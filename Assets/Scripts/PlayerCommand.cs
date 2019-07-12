using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCommand : MonoBehaviour
{
    private const string ALLY_TAG = "PlayerTeam";

    public Camera cam;

    public KeyCode selectAllyKey = KeyCode.E;

    public float selectRange = 5.0f;

    private AIController selectedAlly = null;

    private KeyCode moveKey = KeyCode.F1;
    private KeyCode followKey = KeyCode.F2;
    private KeyCode stayKey = KeyCode.F3;

    public LayerMask layerMask;

    // cached variables
    private RaycastHit hit;

    private void Awake()
    {
        //layerMask = LayerMask.NameToLayer("PlayerTeam");
    }

    // Start is called before the first frame update
    //private void Start()
    //{

    //}

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(selectAllyKey))
        {
            Debug.Log("Select ally key pressed.");
            //Debug.DrawRay(cam.transform.position, cam.transform.forward, Color.red, selectRange);
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, selectRange, layerMask))
            {
                if (hit.transform.gameObject.CompareTag(ALLY_TAG))
                {
                    selectedAlly = hit.transform.GetComponent<AIController>();
                    Debug.Log("Ally selected: " + selectedAlly);
                }
            }
        }

        if (Input.GetKeyDown(followKey))
        {
            if (selectedAlly)
            {
                selectedAlly.Follow(transform);
                Debug.Log("Follow called");
            }
        }
    }
}
