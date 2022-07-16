using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public static class Loader
{

    public enum Scenes {
        MainMenu,
        Map1,
        Map1a
    }

    public static void LoadScene(Scenes scene) {
        //GameObject.Find("SceneTransition").GetComponent<SceneTransition>().ExitSceneTransition();
        DataPersistenceManager.instance.SaveGame();
        int temp = (int) scene;
        SceneManager.LoadSceneAsync(temp);
    }

    public static void LoadScene(int scene) {
        SceneManager.LoadSceneAsync(scene);
    }

    public static int CurrentSceneIndex() {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public static string CurrentSceneName() {
        return SceneManager.GetActiveScene().name;
    }

    public static void PositionPlayer(int previousScene) {
        int currentScene = CurrentSceneIndex();
        Debug.Log(previousScene);
        if (previousScene == (int) Scenes.Map1 && currentScene == (int) Scenes.Map1a) {
            Debug.Log("Positioning correctly in map1a");
            GameObject.Find("Player").transform.position = new Vector3(-8.29f, 1.911422f, 0f);
        } else if (previousScene == (int) Scenes.Map1a && currentScene == (int) Scenes.Map1) {
            Debug.Log("Positioning correctly in map1");
            GameObject.Find("Player").transform.position = new Vector3(6.156194f, -18.09135f, 0f);
        }
    }

}


/*
--- not in use ---

//SceneManager.LoadScene(Scene.Loading.ToString());

public static LevelManager instance;

    [SerializeField]
    private GameObject loaderCanvas;
    [SerializeField]
    private Image progressBar;

    void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public async void LoadScene(string sceneName) {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        loaderCanvas.SetActive(true);

        do {
            progressBar.fillAmount = scene.progress;
        } while (scene.progress < 0.9f);

        scene.allowSceneActivation = true;
        loader.setActive(false);
    }
*/
