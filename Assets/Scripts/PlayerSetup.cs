using UnityEngine;

/// <summary>
/// A component to handle player setup.
/// </summary>
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
        if (playerUIInstance == null)
        {
            playerUIInstance = Instantiate(playerUIPrefab);
            playerUIInstance.name = playerUIPrefab.name;
        }
        // enable PlayerUI
        else
        {
            playerUIInstance.SetActive(true);
        }

        // configure PlayerUI
        PlayerUI playerUI = playerUIInstance.GetComponent<PlayerUI>();
        if (playerUI == null)
        {
            Debug.LogError("No PlayerUI component on PlayerUI prefab.");
        }
        playerUI.SetPlayer(GetComponent<Entity>());

        // hide and lock cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void OnDisable()
    {
        // disable PlayerUI
        playerUIInstance.SetActive(false);

        // unhide and unlock cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
