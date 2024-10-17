using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocomotionManager : MonoBehaviour
{
    CharacterManager character;

    //Ground check and jumping
    [SerializeField] protected float gravityForce = -5.55f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float groundCheckSphereRadius = 1;
    [SerializeField] protected Vector3 yVelocity; // the force the pulls character jump and down
    [SerializeField] protected float grounddedYVelocity = -20; //The gravity when youre grounded
    [SerializeField] protected float fallStartYVelocity = -5; // The gravity when youre falling
    protected bool fallingVelocityHasBeenSet = false;
    protected float inAirTimer = 0;

    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();
    }

    protected virtual void Update()
    {
        HandleGroundCheck();

        if (character.isGrounded)
        {

            if (yVelocity.y <0)
            {
                inAirTimer = 0;
                fallingVelocityHasBeenSet = false;
                yVelocity.y = grounddedYVelocity;
            }
        }
        else
        {
            if (!character.isJumping && !fallingVelocityHasBeenSet)
            {
                fallingVelocityHasBeenSet = true;
                yVelocity.y = fallStartYVelocity;
            }

            inAirTimer = inAirTimer + Time.deltaTime;
            character.animator.SetFloat("InAirTimer", inAirTimer);

            yVelocity.y += gravityForce * Time.deltaTime;
        }

        //gravity
        character.characterController.Move(yVelocity * Time.deltaTime);
    }

    protected void HandleGroundCheck()
    {

        character.isGrounded = Physics.CheckSphere(character.transform.position, groundCheckSphereRadius, groundLayer);


    }

    // draws shpere in scene view
    protected void OnDrawGizmosSelected()
    {
       //Gizmos.DrawSphere(character.transform.position, groundCheckSphereRadius);

        if (character)
        {
            Gizmos.DrawSphere(character.transform.position, groundCheckSphereRadius);
        }
    }

}
