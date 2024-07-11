using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.UI;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{

    public static TitleScreenManager Instance;

    [SerializeField] GameObject titleScreenMainMenu;
    [SerializeField] GameObject titleScreenLoadMenu;

    [Header ("Buttons")]
    [SerializeField] Button loadMenuReturnButton;
    [SerializeField] Button mainMenuLoadGameButton;
    [SerializeField] Button mainMenuNewGameButton;

    [Header ("Pop ups")]
    [SerializeField] GameObject noCharacterSlotsPopUp;
    [SerializeField] Button noCharacterSlotOkayButton;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartNetworkAsHost()
    {
        NetworkManager.Singleton.StartHost(); 
    }

    public void StartNewGame()
    {
        WorldSaveGameManager.instance.AttemptToCreateNewGame();
    }

    public void OpenLoadGameMenu()
    {
        // Close main menu
        titleScreenMainMenu.SetActive(false);

        // open load menu
        titleScreenLoadMenu.SetActive(true);

        // find and auto select latest load slot
        loadMenuReturnButton.Select();
    }

    public void CloseLoadGameMenu()
    {
        // open load menu
        titleScreenLoadMenu.SetActive(false);

        // Close main menu
        titleScreenMainMenu.SetActive(true);


        // find and auto select latest load slot
        mainMenuLoadGameButton.Select();
    }

    public void DisplayNoFreeCharacterSlotsPopUp()
    {
        noCharacterSlotsPopUp.SetActive(true);
        noCharacterSlotOkayButton.Select();
    }

    public void CloseNoFreeCharacterSlotsPopUp()
    {
        noCharacterSlotsPopUp.SetActive(false);
        mainMenuNewGameButton.Select();
    }
}
