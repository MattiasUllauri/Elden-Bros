using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenLoadMenuManager : MonoBehaviour
{
    //conntrol spelled incrorectlly
    PlayerControlls playerControls;

    [Header("Title Screen Input")]
    [SerializeField] bool deleteCharacterSlot = false;

    private void Update()
    {
        if (deleteCharacterSlot)
        {
            deleteCharacterSlot = false;
            TitleScreenManager.Instance.AtteptToDeleteCharacterSlot();
        }
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControlls();
            playerControls.UI.X_Button.performed += i => deleteCharacterSlot = true;
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
}
