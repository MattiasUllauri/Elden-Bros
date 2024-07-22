using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICharacterSaveSlot : MonoBehaviour
{
    SaveFileDataWriter saveFileWriter;

    //Game slot
    public CharacterSlot characterSlot;

    //Chracter info
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI timePlayed;

    private void OnEnable()
    {
        LoadSaveSlots();
    }

    private CharacterSlot GetCharacterSlot()
    {
        return characterSlot;
    }

    private void LoadSaveSlots()
    {
        saveFileWriter = new SaveFileDataWriter();
        saveFileWriter.saveDataDirectoryPath = Application.persistentDataPath;

        //Save slot 01
        switch (characterSlot)
        {
            case CharacterSlot.CharacterSlot_01:
                saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                // If the file exsist, get info from it
                if (saveFileWriter.CheckToSeeIfFlieExsist())
                { 
                    characterName.text = WorldSaveGameManager.instance.characterSlot01.characterName;
                }
                // if it dont, disabel this gameobject
                else
                {
                    gameObject.SetActive(false);
                }
                break;

            case CharacterSlot.CharacterSlot_02:
                saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                // If the file exsist, get info from it
                if (saveFileWriter.CheckToSeeIfFlieExsist())
                {
                    characterName.text = WorldSaveGameManager.instance.characterSlot02.characterName;
                }
                // if it dont, disabel this gameobject
                else
                {
                    gameObject.SetActive(false);
                }
                break;

            case CharacterSlot.CharacterSlot_03:
                saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                // If the file exsist, get info from it
                if (saveFileWriter.CheckToSeeIfFlieExsist())
                {
                    characterName.text = WorldSaveGameManager.instance.characterSlot03.characterName;
                }
                // if it dont, disabel this gameobject
                else
                {
                    gameObject.SetActive(false);
                }
                break;

            case CharacterSlot.CharacterSlot_04:
                saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                // If the file exsist, get info from it
                if (saveFileWriter.CheckToSeeIfFlieExsist())
                {
                    characterName.text = WorldSaveGameManager.instance.characterSlot04.characterName;
                }
                // if it dont, disabel this gameobject
                else
                {
                    gameObject.SetActive(false);
                }
                break;

            case CharacterSlot.CharacterSlot_05:
                saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                // If the file exsist, get info from it
                if (saveFileWriter.CheckToSeeIfFlieExsist())
                {
                    characterName.text = WorldSaveGameManager.instance.characterSlot05.characterName;
                }
                // if it dont, disabel this gameobject
                else
                {
                    gameObject.SetActive(false);
                }
                break;

            case CharacterSlot.CharacterSlot_06:
                saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                // If the file exsist, get info from it
                if (saveFileWriter.CheckToSeeIfFlieExsist())
                {
                    characterName.text = WorldSaveGameManager.instance.characterSlot06.characterName;
                }
                // if it dont, disabel this gameobject
                else
                {
                    gameObject.SetActive(false);
                }
                break;

            case CharacterSlot.CharacterSlot_07:
                saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                // If the file exsist, get info from it
                if (saveFileWriter.CheckToSeeIfFlieExsist())
                {
                    characterName.text = WorldSaveGameManager.instance.characterSlot07.characterName;
                }
                // if it dont, disabel this gameobject
                else
                {
                    gameObject.SetActive(false);
                }
                break;

            case CharacterSlot.CharacterSlot_08:
                saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                // If the file exsist, get info from it
                if (saveFileWriter.CheckToSeeIfFlieExsist())
                {
                    characterName.text = WorldSaveGameManager.instance.characterSlot08.characterName;
                }
                // if it dont, disabel this gameobject
                else
                {
                    gameObject.SetActive(false);
                }
                break;

            case CharacterSlot.CharacterSlot_09:
                saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                // If the file exsist, get info from it
                if (saveFileWriter.CheckToSeeIfFlieExsist())
                {
                    characterName.text = WorldSaveGameManager.instance.characterSlot09.characterName;
                }
                // if it dont, disabel this gameobject
                else
                {
                    gameObject.SetActive(false);
                }
                break;

            case CharacterSlot.CharacterSlot_10:
                saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                // If the file exsist, get info from it
                if (saveFileWriter.CheckToSeeIfFlieExsist())
                {
                    characterName.text = WorldSaveGameManager.instance.characterSlot10.characterName;
                }
                // if it dont, disabel this gameobject
                else
                {
                    gameObject.SetActive(false);
                }
                break;

            default:
                Debug.Log("error");
                break;
        }

        
    }

    public void LoadGameFromCharacterSlot()
    {
        WorldSaveGameManager.instance.currentCharacterSlotBeingUsed = characterSlot;
        WorldSaveGameManager.instance.LoadGame();
    }

    public void SelectedCurrentSlot()
    {
        TitleScreenManager.Instance.SelectCharacterSlot(characterSlot);
    }
}
