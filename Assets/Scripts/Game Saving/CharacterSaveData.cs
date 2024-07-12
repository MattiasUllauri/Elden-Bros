using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
// Since we want to reference this data for every save file, this script is not a monobehaviour and is instead serializable
public class CharacterSaveData
{
    //Character name
    public string characterName = "Character";

    //Time played
    public float secondsPlayed;

    //World cords (Can only save basic variable types) JSON
    public float xPosition;
    public float yPosition;
    public float zPosition;

}
