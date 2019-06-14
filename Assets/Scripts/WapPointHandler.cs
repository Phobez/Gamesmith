using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WapPointHandler : MonoBehaviour
{
    [SerializeField]
    private Transform[] aiRotation;

    public Transform GiveRotation()
    {
        int x = Random.Range(1, 4);
        switch (x)
        {
            case 1:
                return aiRotation[0];
                break;
            case 2:
                return aiRotation[1];
                break;
            case 3:
                return aiRotation[2];
                break;
        }
        return aiRotation[0];
    }
}
