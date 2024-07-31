using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantCharacterEffect : ScriptableObject
{
    //Base class for all instant effects
    [Header("Effect Id")]
    public int instantEffectID;

    public virtual void ProcessEffect(CharacterManager character)
    {

    }
}
