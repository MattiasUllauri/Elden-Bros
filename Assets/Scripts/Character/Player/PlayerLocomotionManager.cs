using UnityEngine;

public class PlayerLocomotionManager : CharacterLocomotionManager
{
    PlayerManager player;

    public float vericalMovement;
    public float horizontalMovement;
    public float moveAmount;

    //Movement Settings
    private Vector3 moveDirection;
    private Vector3 targetRotationDirection;
    [SerializeField] float walkingSpeed = 2;
    [SerializeField] float runningSpeed = 5;
    [SerializeField] float sprintingSpeed = 6.5f;
    [SerializeField] float rotationSpeed = 15;
    [SerializeField] int sprintingStaminaCost = 2;

    //Dodge
    private Vector3 rollDirection;
    [SerializeField] float dodgeStaminaCost = 25;
    [SerializeField] float jumpStaminaCost = 25;

    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerManager>();
    }

    protected override void Update()
    {
        base.Update();

        if (player.IsOwner)
        {
            player.characterNetworkManager.verticalMovement.Value = vericalMovement;
            player.characterNetworkManager.horizontalMovement.Value = horizontalMovement;
            player.characterNetworkManager.moveAmount.Value = moveAmount;
        }
        else
        {
            vericalMovement = player.characterNetworkManager.verticalMovement.Value;
            horizontalMovement = player.characterNetworkManager.horizontalMovement.Value;
            moveAmount = player.characterNetworkManager.moveAmount.Value;

            //IF NOT LOCKED ON, MOASS MOVE AMOUNT
            player.playerAnimatorManager.UpdateAnimatorMovementParamerters(0, moveAmount, player.playerNetworkManager.isSprinting.Value);

            //IF LOCKED ON, PASS VER AND HORZ VALUES
        }    
    }

    public void HandleALLMovement()
    {
        HandleGroundedMovement();
        HandleRotation();
        //HandleSprinting();
        //AERIAL MOVEMENT
    }

    private void GetMovementVales()
    {
        vericalMovement = PlayerInputManager.instance.verticalInput;
        horizontalMovement = PlayerInputManager.instance.horizontalInput;
        moveAmount = PlayerInputManager.instance.moveAmount;

        //CLAMP THE MOVEMENTS
    }

    private void HandleGroundedMovement()
    {
        GetMovementVales();

        if (!player.canMove)
            return;

        // OUR MOVE DIRECTION IS BASED ON OUR CAMERAS FACING PERSPECTIVE & OUR MOVEMENT INPUTS
        moveDirection = PlayerCamera.instance.transform.forward * vericalMovement;
        moveDirection = moveDirection + PlayerCamera.instance.transform.right * horizontalMovement;
        moveDirection.Normalize();
        moveDirection.y = 0;

        
        if(player.playerNetworkManager.isSprinting.Value)   //Sprinting
        {
            player.characterController.Move(moveDirection * sprintingSpeed * Time.deltaTime);
        }
        else    //Walking & Running
        {
            if(PlayerInputManager.instance.moveAmount > 0.5f)
            {
                player.characterController.Move(moveDirection * runningSpeed * Time.deltaTime);
            }
            else if (PlayerInputManager.instance.moveAmount <= 0.5f)
            {
                player.characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);
            }
        }

    }

    private void HandleRotation()
    {
        if (!player.canRotate)
            return;

        targetRotationDirection = Vector3.zero;
        targetRotationDirection = PlayerCamera.instance.cameraObject.transform.forward * vericalMovement;
        targetRotationDirection = targetRotationDirection + PlayerCamera.instance.cameraObject.transform.right * horizontalMovement;
        targetRotationDirection.Normalize();
        targetRotationDirection.y = 0;

        if (targetRotationDirection == Vector3.zero)
        {
            targetRotationDirection = transform.forward;
        }

        Quaternion newRotation = Quaternion.LookRotation(targetRotationDirection);
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed  * Time.deltaTime);
        transform.rotation = newRotation;
    }

    public void HandleSprinting()
    {
        if (player.isPerformingAction)
        {
            player.playerNetworkManager.isSprinting.Value = false;
        }

        // we out of stamina, set Sprinting to False
        if (player.playerNetworkManager.currentStamina.Value <= 0 )
        {
            player.playerNetworkManager.isSprinting.Value = false;
            return;
        }

        if (moveAmount >= 0.5)  //  If we are moving, sprinting is true
        {
            player.playerNetworkManager.isSprinting.Value = true;
        }
        else    // if we are stationary/moving slowly sprinting is false
        {
            player.playerNetworkManager.isSprinting.Value = false;
        }

        if (player.playerNetworkManager.isSprinting.Value)
        {
            player.playerNetworkManager.currentStamina.Value -= sprintingStaminaCost * Time.deltaTime;
        }
    }

    public void AttemptToPerformDodge()
    {
        if (player.isPerformingAction)
            return;

        if (player.playerNetworkManager.currentStamina.Value <= 0)
            return;

        //MOVING THEN ROLL
        if (moveAmount > 0)
        {
            rollDirection = PlayerCamera.instance.cameraObject.transform.forward * PlayerInputManager.instance.verticalInput;
            rollDirection += PlayerCamera.instance.cameraObject.transform.right * PlayerInputManager.instance.horizontalInput;
            rollDirection.y = 0;
            rollDirection.Normalize();

            Quaternion playerRotation = Quaternion.LookRotation(rollDirection);
            player.transform.rotation = playerRotation;

            player.playerAnimatorManager.PlayTargetActionAnimation("Roll_Forward_01", true, true);
        }
        else
        {
            player.playerAnimatorManager.PlayTargetActionAnimation("Back_Step_01", true, true);
        }
        player.playerNetworkManager.currentStamina.Value -= dodgeStaminaCost;

    }

    public void AttemptToPerformJump()
    {
       //Will change when in combat

       //no jumping mid action
        if (player.isPerformingAction)
            return;

        // if no stamtina then no jump
        if (player.playerNetworkManager.currentStamina.Value <= 0)
            return;

        // no double jumps
        if (player.isJumping)
            return;

        // no falling jump
        if (player.isGrounded)
            return;

        // if we are two handing then play two handed jump animation (To do)
        player.playerAnimatorManager.PlayTargetActionAnimation("Main_Jump01", false);

        player.isJumping = true;

        player.playerNetworkManager.currentStamina.Value -= jumpStaminaCost;
    }

    public void ApplyJumpoingVelocity()
    {
        // Apply an Upward velocity
    }
}
