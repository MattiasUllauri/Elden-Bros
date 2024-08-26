using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Effects/Instant Effect/Take Damage")]
public class TakeDamage : InstantCharacterEffect
{

    [Header("Character Causing Damage")]
    public CharacterManager characterCausingDamage;

    [Header("Enemys Damage")]
    public float physicalDamage = 0; // Standard/Strike/Slash/Pierce
    public float magicDamage = 0;
    public float fireDamage = 0;
    public float holyDamage = 0;
    public float lightningDamage = 0;

    [Header("Final Damage")]
    private int finalDamageDelt = 0;

    [Header("Poise")]
    public float poiseDamage = 0;
    public bool poiseIsBroken = false; //Stance break

    //Build Ups
    //Build up effect amounts

    [Header("Animation")]
    public bool playDamageAnimation = true;
    public bool manuallySelectDamageAnimation = false;
    public string damageAnimation;

    [Header("Sound FX")]
    public bool willPlayDamageSFX = true;
    public AudioClip elementalDamagaeSoundFX; // Magic/Fire/Lightning/Holy

    [Header("Directional Damage")]
    public float angleHitFrom;      //Directional damage animation
    public Vector3 contanctPoint;   //Blood spater direction

    public override void ProcessEffect(CharacterManager character)
    {
        base.ProcessEffect(character);

        if (character.isDead.Value)
            return;

        //Check vulnerablility

        CalculateDamagae(character);
        //Check where damage came from
        //play damage animation
        //check for build ups
        //play damage sound fx
        //blood
    }

    private void CalculateDamagae(CharacterManager character)
    {
        if (!character.IsOwner) 
            return;

        if (characterCausingDamage != null)
        {
            // Check for damage modifires
        }
        
        //armor modifires, etc
        
        finalDamageDelt = Mathf.RoundToInt(physicalDamage + magicDamage + fireDamage + lightningDamage + holyDamage);

        if(finalDamageDelt <= 0)
        {
            finalDamageDelt = 1;
        }

        character.characterNetworkManager.currentHealth.Value -= finalDamageDelt;

        // calculate poise damage to play animation
    }
}
