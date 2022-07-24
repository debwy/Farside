using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //https://www.youtube.com/watch?v=aUi9aijvpgs&t=51s

    /*
    The values defined in this constructor will be the default values the game starts
    with when there's no data to load
    */

    public Vector3 playerPosition;
    public int attackDamage;
    public int maxHealth;
    public int currentHealth;
    public int shotDmgDivide;
    public int lastSavedScene;
    public int lastScene;
    public int slimeDeaths;
    public int golemDeaths;
    public int batDeaths;
    public SerializableDictionary<string, bool> chestsOpened;
    public int chestOpenCount;
    //public string jsonDialogueSave;
    public SerializableDictionary<string, string> dialogueSave;

    public GameData()
    {
        playerPosition = new Vector3(39.82f, -2f, 0f);
        attackDamage = 10;
        maxHealth = 100;
        currentHealth = maxHealth;
        shotDmgDivide = 5;
        lastSavedScene = (int) Loader.Scenes.Map1;
        lastScene = (int) Loader.Scenes.Map1;
        slimeDeaths = 0;
        golemDeaths = 0;
        batDeaths = 0;
        chestsOpened = new SerializableDictionary<string, bool>();
        chestOpenCount = 0;
        //jsonDialogueSave = "";
        dialogueSave = new SerializableDictionary<string, string>();
    }
    
}
