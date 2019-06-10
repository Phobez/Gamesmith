using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected bool isDead = false;
    public bool IsDead
    {
        get { return isDead; }
        protected set { isDead = value; }
    }

    [SerializeField]
    protected int maxHealth = 100;
    protected int currentHealth;

    [SerializeField]
    protected Behaviour[] componentsToDisableOnDeath;
    protected Collider col;
    protected WaitForSeconds respawnDelay;

    private void Awake()
    {
        SetUp();
    }

    protected void SetUp()
    {
        col = GetComponent<Collider>();
        respawnDelay = new WaitForSeconds(GameController.instance.matchSettings.respawnDelay);
        SetDefaults();
    }

    public void SetDefaults()
    {
        isDead = false;

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

    public void TakeDamage(int _amount)
    {
        if (isDead)
        {
            return;
        }

        currentHealth -= _amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected void Die()
    {
        isDead = true;

        for (int i = 0; i < componentsToDisableOnDeath.Length; i++)
        {
            componentsToDisableOnDeath[i].enabled = false;
        }

        if (col != null)
        {
            col.enabled = false;
        }

        Debug.Log(transform.name + " is DEAD!");

        StartCoroutine(Respawn());
    }

    protected IEnumerator Respawn()
    {
        yield return respawnDelay;

        SetDefaults();
        Transform _spawnPoint = GameController.instance.GetSpawnPoint();
        transform.position = _spawnPoint.position;
        transform.rotation = _spawnPoint.rotation;

        Debug.Log(transform.name + " respawned.");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(1000);
        }
    }
}
