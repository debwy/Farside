using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;

public class MainMenu : Menu
{
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;
    [SerializeField] private Button quitGameButton;

    private void Start() {
        if (!DataPersistenceManager.instance.HasGameData()) {
            continueGameButton.interactable = false;
            this.SetFirstSelected(newGameButton);
        }
    }

    public void NewGame() {

        DisableAllButtons();

        //Creates new game which initializes game data
        DataPersistenceManager.instance.NewGame();

        DataPersistenceManager.instance.MenuSaveGame();
        DataPersistenceManager.instance.isLoadedFromMenu = true;

        //Loads gameplay scene (saves game since main menu is unloaded)
        Loader.Load(Loader.Scene.Map1);

    }

    public void ContinueGame() {

        DisableAllButtons();

        //Saving before a scene is destroyed rather than after to prevent reference errors
        DataPersistenceManager.instance.MenuSaveGame();

        DataPersistenceManager.instance.isLoadedFromMenu = true;

        //Loads next scenne (which loads game due to OnSceneLoaded() in DataPersistenceManager)
        Loader.Load(Loader.Scene.Map1);

    }

    public void QuitGame() {

        DisableAllButtons();

        Debug.Log("Quit game");
        Application.Quit();
    }

    //To prevent user from accidentally clicking when loading
    private void DisableAllButtons() {
        newGameButton.interactable = false;
        continueGameButton.interactable = false;
        quitGameButton.interactable = false;
    }
}


//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);