using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : Item
{
    //Animation Controller

    [Header("Weapon Model")]
    public GameObject weaponModel;

    [Header("Weapon Requirments")]
    public int strengthREQ = 0;
    public int dexREQ = 0;
    public int intREQ = 0;
    public int faithREQ = 0;

    [Header("Weapon Base Damage")]
    public int physicalDamage;
    public int magicDamage = 0;
    public int fireDamage = 0;
    public int holyDamage = 0;
    public int lightningDamage = 0;

    [Header("Weapon Poise")]
    public float poiseDamage = 10;
    //Poise Bonus

    [Header("Stamina Cost")]
    public int baseSatimaCost = 5;
    //light and heavy attacks and other attacks stamina cost

    //Item Base actions
    [Header("Actions")]
    public WeaponItemAction oh_RB_Action; //One Hand Right Button Action

    //Ash of War

    //Sounds
}
