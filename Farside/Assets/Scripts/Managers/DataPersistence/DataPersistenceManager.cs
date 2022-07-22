using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

//Keeps track of game data
public class DataPersistenceManager : MonoBehaviour
{
    //Singleton
    //Able to get instance publicly, but modifying it is private within the class
    public static DataPersistenceManager instance {get; private set;}

    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private FileDataHandler dataHandler;

    private GameData gameData;

    private List<IDataPersistence> dataPersistenceObjects;

    private string mainProfileId = "main";
    private string tempProfileId = "temp";
    public bool isLoadedFromMenu = false;
    public bool doNotSave = false;
    public bool isSavedFromCheckpoint = false;

    private void Awake()
    {
        if (instance != null) {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        //Application.persistentDataPath gives OS standard directory for persisting data in a unity project
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
    }

    public void NewGame() {
        this.gameData = new GameData();
    }

    public void LoadGame() {

        //Loads any saved data from a file (using the data handler)
        //Result would be null if data doesn't exist
        if (isLoadedFromMenu) {
            this.gameData = dataHandler.Load(mainProfileId);
        } else {
            this.gameData = dataHandler.Load(tempProfileId);
        }

        //If no data to load, don't continue
        if (this.gameData == null) {
            Debug.Log("No data was found");
            return;
        }

        //Push the loaded data to all other scripts that need it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects) {
            dataPersistenceObj.LoadData(gameData);
        }

        isLoadedFromMenu = false;
    }

    public void MenuSaveGame() {

        isSavedFromCheckpoint = true;

        //If there's no data to save, log a warning
        if (this.gameData == null) {
            Debug.LogWarning("No data was found. A new game needs to be started before data can be saved");
            return;
        }

        //Pass the data to other scripts so that they can update it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects) {
            dataPersistenceObj.SaveData(gameData);
        }

        //Save that data to a file (using the data handler)
        dataHandler.Save(gameData, mainProfileId);
        dataHandler.Save(gameData, tempProfileId);
        
        isSavedFromCheckpoint = false;

        Debug.Log("Saved game");
    }

    public void SaveGame() {
        //do not save in instances like character death when changing scene
        if (!doNotSave) {

            //If there's no data to save, log a warning
            if (this.gameData == null) {
                Debug.LogWarning("No data was found. A new game needs to be started before data can be saved");
                return;
            }

            //Pass the data to other scripts so that they can update it
            foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects) {
                dataPersistenceObj.SaveData(gameData);
            }

            //Save that data to a file (using the data handler)
            dataHandler.Save(gameData, tempProfileId);

        } else {
            doNotSave = false;
        }
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects() {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public bool HasGameData() {
        return gameData != null;
    }

    private void OnEnable() {
        //Subscribing to scene events
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable() {
        //Unsubscribing from scene events
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //OnSceneLoaded is called after OnEnable() but before Start() (& OnDisable())
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        //Everytime a scene is loaded re-initializes list of data persistence objects
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }
    
    public bool IsLoadedFromMenu() {
        return isLoadedFromMenu;
    }

    public bool IsSavedFromCheckpoint() {
        return isSavedFromCheckpoint;
    }

    public void SetLoadedFromMenu(bool input) {
        isLoadedFromMenu = input;
    }

    /*
    public void OnSceneUnloaded(Scene scene) {
        //Data is saved everytime a scene is unloaded
        SaveGame();
    }
    */

}
