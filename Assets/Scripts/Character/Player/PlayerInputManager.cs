using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager instance;

    public PlayerManager player;

    PlayerControlls playerControls;

    [Header("PLAYER MOVEMENT INPUT")]
    [SerializeField] Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;
    public float moveAmount;

    [Header("CAMERA MOVEMENT INPUT")]
    [SerializeField] Vector2 cameraInput;
    public float cameraVerticalInput;
    public float cameraHorizontalInput;

    [Header("Player Action Input")]
    [SerializeField] bool dodgeInput = false;
    [SerializeField] bool sprintInput = false;
    [SerializeField] bool jumpInput = false;

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

    }

    private void Start()
    { 
        DontDestroyOnLoad(gameObject);

        //WHEN THE SCENE CHANGES, RUN THIS LOGIC
        SceneManager.activeSceneChanged += OnScreenChange;

        instance.enabled = false;  

        if (playerControls != null)
        {
            playerControls.Disable();                                      
        }
    }

    private void Update()
    {
        HandleAllInputs();
    }

    private void OnScreenChange(Scene oldScene, Scene newScene)
    {
        //IF WE ARE LOADING INTO OR WORLD SCENE, ENABLE OUR PLAYERS CONTROLS
        if(newScene.buildIndex == WorldSaveGameManager.instance.GetWorldSceneIndex())
        {
            instance.enabled = true;

            if (playerControls != null)
            {
                playerControls.Enable();
            }
        }
        //OTHERWISE WE MUST BE AT THE MAIN MENU, DISABLE OUOR PLAYER CONTROLLS
        else
        {
            instance.enabled = false;

            if (playerControls != null)
            {
                playerControls.Disable();
            }
        }
    }

    private void OnEnable()
    {
       if (playerControls == null )
       {
            playerControls = new PlayerControlls();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerCamera.Movement.performed += i => cameraInput = i.ReadValue<Vector2>();
            playerControls.PlayerActions.Dodge.performed += i => dodgeInput = true;
            playerControls.PlayerActions.Jump.performed += i => jumpInput = true;

            playerControls.PlayerActions.Sprint.performed += i => sprintInput = true;    //Sprint
            playerControls.PlayerActions.Sprint.canceled += i => sprintInput = false;     //Released
        }

        playerControls.Enable();
    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= OnScreenChange;
    }

    // IF WE MINIMIZE OR LOWER THE WINDOW, STOP ADJUSTING MOVEMENT (TESTING)
    private void OnApplicationFocus(bool focus)
    {
        if(enabled)
        {
            if (focus)
            {
                playerControls.Enable();
            }
            else
            {
                playerControls.Disable();
            }
        }
    }

    private void HandleAllInputs()
    {
        HandlePlayerMovementInput();
        HandleCameraMovementInput();
        HandleDodgeInput();
        HandleSprintInput();
        HandleJumpInput();
    }

    //MOVEMENT

    private void HandlePlayerMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        //abs always positive cause movement based off camera
        moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));

        //we clamp the values so they are 0, 0.5 or 1 (VERY OPTIONAL) can remove later cause zelda dont do it
        if(moveAmount <= 0.5 &&  moveAmount > 0)
        {
            moveAmount = 0.5f;
        }
        else if(moveAmount > 0.5 && moveAmount <= 1)
        {
            moveAmount = 1;
        }

        //WHY DO WE PASS 0 ON THE HORIZONTAL? BECAUSE WE ONLY  WANT NON-STRAFING MOVEMENT
        //WE USE YTHE HORIZONTAL WHEN WE ARE STRAFING OR LOCKING ON

        if (player == null)
            return;

        //if we are not loced on, only use the move amount
        player.playerAnimatorManager.UpdateAnimatorMovementParamerters(0, moveAmount, player.playerNetworkManager.isSprinting.Value);

        // IF WE ARE LOKED ON PLASS THE HORIZONTAL MOVEMENT AS WELL
    }

    private void HandleCameraMovementInput()
    {
        cameraVerticalInput = cameraInput.y;
        cameraHorizontalInput = cameraInput.x;
    }    

    //ACTIONS

    private void HandleDodgeInput()
    {
        if (dodgeInput)
        {
            dodgeInput = false;

            //FN: RETURN IF MENU OPEN NO DODGE

            player.playerLocomotionManager.AttemptToPerformDodge();
        }
    }

    private void HandleSprintInput()
    {
        if (sprintInput)
        {
            player.playerLocomotionManager.HandleSprinting();
        }
        else
        {
            player.playerNetworkManager.isSprinting.Value = false;
        }
    }

    private void HandleJumpInput()
    {
        if (jumpInput)
        {
            jumpInput = false;

            //if we have a ui window open then disable

            //attemp to jup
            player.playerLocomotionManager.AttemptToPerformJump();
        }
    }
}
