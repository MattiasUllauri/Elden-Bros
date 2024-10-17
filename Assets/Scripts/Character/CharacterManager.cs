using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CharacterManager : NetworkBehaviour
{

    [Header("Status")]
    public NetworkVariable<bool> isDead = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    [HideInInspector] public CharacterController characterController;
    [HideInInspector] public Animator animator;

    [HideInInspector] public CharacterNetworkManager characterNetworkManager;
    [HideInInspector] public CharacterEffectsManager characterEffectsManager;
    [HideInInspector] public CharacterAnimationManager characterAnimationManager;

    //Flags
    public bool isPerformingAction = false;
    public bool isJumping = false;
    public bool isGrounded = false;
    public bool applyRootMotion = false;
    public bool canRotate = true;
    public bool canMove = true;

    protected virtual void Awake()
    {
        DontDestroyOnLoad(this);   

        characterEffectsManager = GetComponent<CharacterEffectsManager>();

        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        characterNetworkManager = GetComponent<CharacterNetworkManager>();  
        characterAnimationManager = GetComponent<CharacterAnimationManager>();
    }

    protected virtual void Update()
    {
        animator.SetBool("isGrounded", isGrounded);

        // IF THIS CHARACTER IS BEING CONTROLLED FROM OUR SIDE, THEN ASSIGN ITS NETWORK POSITION TO THE POSITION OF OUR TRANSFORM
        if (IsOwner)
        {
            characterNetworkManager.networkPosition.Value = transform.position;
            characterNetworkManager.networkRotation.Value = transform.rotation;
        }
        // IF THIS CHARACTER IS BEING CONTROLLED FROM ELSE WHERE, THEN ASSIGN ITS POSITION HERE LOCALLY BY THE POSITION OF ITS NETWORK TRANSFORM
        else
        {
            //POSITION
            transform.position = Vector3.SmoothDamp
                (transform.position, characterNetworkManager.networkPosition.Value,
                ref characterNetworkManager.networkPositionVelocity,
                characterNetworkManager.networkPositionSmoothTime);
            //ROTATION
            transform.rotation = Quaternion.Slerp
                (transform.rotation, 
                characterNetworkManager.networkRotation.Value, 
                characterNetworkManager.networkRotationSmoothTime);
        }
    }

    protected virtual void LateUpdate()
    {
        //david deleted this one
    }

    public virtual IEnumerator ProcessDeathEvent(bool manuallySelectDeathAnimation = false)
    {
        if(IsOwner)
        {
            characterNetworkManager.currentHealth.Value = 0;
            isDead.Value = true;

            if (!manuallySelectDeathAnimation)
            {
                characterAnimationManager.PlayTargetActionAnimation("Death_01", true);
            }
        }

        // Play death sfx

        yield return new WaitForSeconds(5);

        // Disable Character
    }

    public virtual void ReviveCharacter()
    {

    }
}
