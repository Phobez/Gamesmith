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
    }

    private void OnDisable()
    {
        Destroy(playerUIInstance);
    }
}
