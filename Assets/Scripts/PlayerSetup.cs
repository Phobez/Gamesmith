using UnityEngine;

[RequireComponent(typeof(Entity))]
public class PlayerSetup : MonoBehaviour
{
    public GameObject playerUIPrefab;
    private GameObject playerUIInstance;

    // Start is called before the first frame update
    private void Start()
    {
        GetComponent<Entity>().SetUp();
    }

    private void OnEnable()
    {
        // create PlayerUI
        playerUIInstance = Instantiate(playerUIPrefab);
        playerUIInstance.name = playerUIPrefab.name;

        // configure PlayerUI
        PlayerUI playerUI = playerUIInstance.GetComponent<PlayerUI>();
        if (playerUI == null)
        {
            Debug.LogError("No PlayerUI component on PlayerUI prefab.");
        }
        playerUI.SetPlayer(GetComponent<Entity>());
    }

    private void OnDisable()
    {
        Destroy(playerUIInstance);
    }
}
