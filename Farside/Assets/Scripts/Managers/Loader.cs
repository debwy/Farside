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
        Map1
    }

    public static void Load(Scene scene) {
        //SceneManager.LoadScene(Scene.Loading.ToString());
        SceneManager.LoadScene(scene.ToString());
    }
}


/*
--- not in use ---
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
