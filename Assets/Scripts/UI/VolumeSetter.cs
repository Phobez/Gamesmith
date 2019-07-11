using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

// Written by:  Hans Budiman
public class VolumeSetter : MonoBehaviour
{
    public AudioMixer am;
    public Slider s;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Volume") == false)
        {
            PlayerPrefs.SetFloat("Volume", 0.0f);
        }
    }

    private void OnEnable()
    {
        s.value = Mathf.Pow(10, PlayerPrefs.GetFloat("Volume") / 20);
    }

    public void SetVolume()
    {
        float value = Mathf.Log10(s.value) * 20;
        am.SetFloat("Volume", value);
        PlayerPrefs.SetFloat("Volume", value);
    }
}
