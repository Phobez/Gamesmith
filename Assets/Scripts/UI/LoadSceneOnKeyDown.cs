using UnityEngine;
using UnityEngine.SceneManagement;

// Written by:  Hans Budiman

public class LoadSceneOnKeyDown : MonoBehaviour
{

    public string targetScene;
    public KeyCode key;

    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            SceneManager.LoadScene(targetScene);
        }
    }
}
