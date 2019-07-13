using UnityEngine;
using UnityEngine.SceneManagement;

// Written by: Hans Budiman
public class BackgroundMusicPlayer : MonoBehaviour
{

    private AudioSource audi;
    private static BackgroundMusicPlayer instance = null;
    private bool isPaused;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        audi = GetComponent<AudioSource>();
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2 && isPaused == false)
        {
            audi.Pause();
            isPaused = true;
        }
        else if (SceneManager.GetActiveScene().buildIndex != 2 && isPaused == true)
        {
            audi.Play();
            isPaused = false;
        }
    }
}
