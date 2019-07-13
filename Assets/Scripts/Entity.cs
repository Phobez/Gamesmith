using System.Collections;
using UnityEngine;

/// <summary>
/// A component defining Player and AI attributes and behaviour.
/// </summary>
public class Entity : MonoBehaviour
{
    protected bool isDead = false;
    public bool IsDead                                  // getter setter
    {
        get { return isDead; }
        protected set { isDead = value; }
    }
    protected bool isCrouching = false;
    public bool IsCrouching                                  // getter setter
    {
        get { return isCrouching; }
        protected set { isCrouching = value; }
    }

    public float crouchDeltaHeight = 1.0f;

    [SerializeField]
    protected int maxHealth = 100;
    protected int currentHealth;

    [SerializeField]
    protected Behaviour[] componentsToDisableOnDeath;
    protected Collider col;                             // to be disabled on death
    protected CapsuleCollider capsuleCol;
    protected WaitForSeconds respawnDelay;

    [SerializeField]
    protected AudioClip dieSound;
    // cached variables
    protected Vector3 capsuleColCenterCrouchDelta;

    /// <summary>
    /// A method to initially set up Entity.
    /// </summary>
    public void SetUp()
    {
        col = GetComponent<Collider>();
        capsuleCol = GetComponent<CapsuleCollider>();
        capsuleColCenterCrouchDelta = new Vector3(0.0f, crouchDeltaHeight / 2, 0.0f);
        respawnDelay = new WaitForSeconds(GameController.instance.matchSettings.respawnDelay);
        SetDefaults();
    }

    /// <summary>
    /// A method to set values to default.
    /// </summary>
    public void SetDefaults()
    {
        IsDead = false;

        currentHealth = maxHealth;

        for (int i = 0; i < componentsToDisableOnDeath.Length; i++)
        {
            componentsToDisableOnDeath[i].enabled = true;
        }

        if (col != null)
        {
            col.enabled = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(1000);
        }
    }

    /// <summary>
    /// A method to take damage and check health.
    /// </summary>
    /// <param name="_amount">Amount of damage received.</param>
    public void TakeDamage(int _amount)
    {
        if (IsDead)
        {
            return;
        }

        currentHealth -= _amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Crouch()
    {
        isCrouching = true;
        capsuleCol.height -= crouchDeltaHeight;
        capsuleCol.center -= capsuleColCenterCrouchDelta;
    }

    public void StandUp()
    {
        isCrouching = false;
        capsuleCol.height += crouchDeltaHeight;
        capsuleCol.center += capsuleColCenterCrouchDelta;
    }

    /// <summary>
    /// A method to kill the Entity and initiate respawn.
    /// </summary>
    protected void Die()
    {
        IsDead = true;

        for (int i = 0; i < componentsToDisableOnDeath.Length; i++)
        {
            componentsToDisableOnDeath[i].enabled = false;
        }

        if (col != null)
        {
            col.enabled = false;
        }

        if(dieSound != null)
        AudioSource.PlayClipAtPoint(dieSound,transform.position);
        StartCoroutine(Respawn());
    }

    /// <summary>
    /// A coroutine to handle respawn.
    /// </summary>
    /// <returns></returns>
    protected IEnumerator Respawn()
    {
        yield return respawnDelay;

        SetDefaults();
        Transform _spawnPoint = GameController.instance.GetSpawnPoint(gameObject.tag);
        transform.position = _spawnPoint.position;
        transform.rotation = _spawnPoint.rotation;
    }

    /// <summary>
    /// A method to return Entity health as a percentage between 0 and 1.
    /// </summary>
    /// <returns>Health in percentage between 0 and 1.</returns>
    public float GetHealthPercentage()
    {
        return (float)currentHealth / maxHealth;
    }
}
