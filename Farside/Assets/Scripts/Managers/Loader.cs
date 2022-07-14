using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene {
        MainMenu,
        Loading,
        Map1,
        Map1a
    }

    public static void Load(Scene scene) {
        SceneManager.LoadSceneAsync(scene.ToString());
    }

    /*
    public static string CurrentScene() {
        return Scene.name;
    }
    */
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
