using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerUIManager : MonoBehaviour
{
    public static PlayerUIManager instance;

    [Header("Network Join")]
    [SerializeField] bool startGameAsClient;

    public PlayerUIHudManager playerUIHudManager;
    public PlayerUIPopUpManager playerUIPopUpManager;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        playerUIHudManager = GetComponentInChildren<PlayerUIHudManager>();
        playerUIPopUpManager = GetComponentInChildren<PlayerUIPopUpManager>();
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(startGameAsClient)
        {
            startGameAsClient = false;
            // FIRST SHUT DOWN, BECAUSE WE HAVE STARTED AS A HOST DURING THE TITHLE SCREEN
            NetworkManager.Singleton.Shutdown();
            // THE RESTART, AS A CLIENT
            NetworkManager.Singleton.StartClient();
        }
    }
}
