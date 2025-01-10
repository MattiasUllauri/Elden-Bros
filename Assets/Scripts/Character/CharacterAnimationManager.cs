using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

public class CharacterAnimationManager : MonoBehaviour
{
    CharacterManager character;

    float horizontal;
    float vertical;

    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();

        vertical = Animator.StringToHash("Vertical");
        horizontal = Animator.StringToHash("Horizontal");
    }
    public void UpdateAnimatorMovementParamerters(float horizontalMovement, float verticalMovement, bool isSprinting)
    {
        float horizontalAmount = horizontalMovement;
        float verticalAmount = verticalMovement;

        if (isSprinting)
        {
            verticalAmount = 2;
        }

        character.animator.SetFloat("Horizontal", horizontalAmount, 0.1f, Time.deltaTime);
        character.animator.SetFloat("Vertical", verticalAmount, 0.1f, Time.deltaTime);
    }

    public virtual void PlayTargetActionAnimation(string targetAnimation, bool isPerformingAction, bool applyRootMotion = true, bool canRotate = false, bool canMove = false)
    {
        character.applyRootMotion = applyRootMotion;
        character.animator.CrossFade(targetAnimation, 0.2f);

        character.isPerformingAction = isPerformingAction;
        character.canRotate = canRotate;
        character.canMove = canMove;

        // TELL THE SERVER/HOST WE PLAYED AN ANIMATION, AND TO PLAY THAT ANI FOR EVERYBODY ELSE PRESENT
        character.characterNetworkManager.NotifytheServerOfActionAnimationServerRpc(NetworkManager.Singleton.LocalClientId, targetAnimation, applyRootMotion);
    }

    public virtual void PlayTargetAttackActionAnimation(
        string targetAnimation,
        bool isPerformingAction,
        bool applyRootMotion = true,
        bool canRotate = false,
        bool canMove = false)
    {

        // keep track of last attack performed for combos
        //keep track of current attack type (light heavey)
        //update acimation
        //decide if our attack can be parried
        //tell the network we are in an "attack" flag is active (for counter damage)
        character.applyRootMotion = applyRootMotion;
        character.animator.CrossFade(targetAnimation, 0.2f);
        character.isPerformingAction = isPerformingAction;
        character.canRotate = canRotate;
        character.canMove = canMove;

        // TELL THE SERVER/HOST WE PLAYED AN ANIMATION, AND TO PLAY THAT ANI FOR EVERYBODY ELSE PRESENT
        character.characterNetworkManager.NotifytheServerOfAttackActionAnimationServerRpc(NetworkManager.Singleton.LocalClientId, targetAnimation, applyRootMotion);

    }
}
