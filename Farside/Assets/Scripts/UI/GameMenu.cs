using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : Menu
{

    public void SaveGame() {

        //Creates new game which initializes game data
        DataPersistenceManager.instance.SaveGame();

        Debug.Log("Saved game");

    }

}
