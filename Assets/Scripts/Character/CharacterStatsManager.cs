using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class CharacterStatsManager : MonoBehaviour
{
    private CharacterManager character;

    //Stanima regen
    [SerializeField] float staminaRegenAmount = 2;
    private float staminaRegenTimer = 0;
    private float staminaTickTimer = 0;
    [SerializeField] float staminaRegenDelay = 2;

    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();
    }

    public int CalculateStaminaBasedOnLevel(int endurance)
    {
        float stamina = 0;

        // Equation to change for balencing
        stamina = endurance * 10;

        return Mathf.RoundToInt(stamina);
    }

    public virtual void RegenerateStamina()
    {
        //only owner can edit
        if (!character.IsOwner)
            return;

        if (character.characterNetworkManager.isSprinting.Value)
            return;

        if (character.isPerformingAction)
            return;

        staminaRegenTimer += Time.deltaTime;

        if (staminaRegenTimer >= staminaRegenDelay)
        {
            if (character.characterNetworkManager.currentStamina.Value < character.characterNetworkManager.maxStamina.Value)
            {
                staminaTickTimer += Time.deltaTime;

                if (staminaTickTimer >= 0.1)
                {
                    staminaTickTimer = 0;
                    character.characterNetworkManager.currentStamina.Value += staminaRegenAmount;
                }
            }
        }
    }

    public virtual void ResetStaminaRegenTimer(float previousStaminaAmount, float currentStaminaAmount)
    {
        // if we use stamina that consumes stamina we wait 2 seconds
        // if we gain stamina we dont wait
        if (currentStaminaAmount < previousStaminaAmount)
        {
            staminaRegenTimer = 0;
        }
    }
}
