using UnityEngine;

// Designed by      : Abia P.H.
// Written by       : Abia P.H.
// Documented by    : Abia P.H.

// TO-DO: REPLACE WITH SCRIPTABLE OBJECT
/// <summary>
/// A class containing weapon attributes.
/// </summary>
[System.Serializable]
public class Weapon
{
    public string name = "Glock";

    public int damage = 10;
    public float range = 100.0f;

    public float fireRate = 0.0f;

    public int maxBullets = 20;
    [HideInInspector]
    public int bullets;

    public float reloadTime = 1.0f;

    public GameObject graphics;

    public Weapon()
    {
        bullets = maxBullets;
    }
}
