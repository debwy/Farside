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

    public GameData()
    {
        playerPosition = new Vector3(39.82f, -2f, 0f);
        attackDamage = 10;
        maxHealth = 100;
        currentHealth = maxHealth;
        shotDmgDivide = 5;
        lastSavedScene = (int) Loader.Scenes.Map1;
    }
    
}
