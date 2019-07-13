using UnityEngine;

// Designed by      : Abia P.H.
// Written by       : Abia P.H.
// Documented by    : -

// TEMPORARY SCRIPT
// REMOVE LATER
public class GunFire : MonoBehaviour
{
    private AudioSource audioSource;
    private Animation animationComponent;
    public ParticleSystem muzzleFlash;

    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animationComponent = GetComponent<Animation>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            muzzleFlash.Play();
            audioSource.Play();
            animationComponent.Play("Gunshot");
        }
    }
}
