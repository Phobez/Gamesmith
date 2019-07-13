using UnityEngine;
using UnityEngine.SceneManagement;


// Written by:  Hans Budiman
public class LoadSceneOnClick : MonoBehaviour
{
    public void LoadScene(string targetScene)
    {
        SceneManager.LoadScene(targetScene);
    }
}
