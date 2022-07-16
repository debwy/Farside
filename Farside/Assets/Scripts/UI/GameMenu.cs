using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : Menu
{
    [SerializeField] GameObject saveButton;

    public void SaveGame() {

        //Creates new game which initializes game data
        DataPersistenceManager.instance.SaveGame();
        Debug.Log("Saved game");

    }

    void Update() {
        if (DialogueManager.GetInstance().dialogueIsPlaying) {
            saveButton.SetActive(false);
        } else {
            saveButton.SetActive(true);
        }
    }

}
