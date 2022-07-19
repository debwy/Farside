using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : Menu
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private GameObject pauseMenu;

    private bool isPaused = false;

    public void Start() {
        pauseMenu.SetActive(false);
    }

    public void Update() {
        if (DialogueManager.GetInstance().dialogueIsPlaying) {
            isPaused = false;
        } else {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                if (isPaused) {
                    Resume();
                } else {
                    Pause();
                }
            }
        }
    }

    public void Pause() {
        EnableAllButtons();
        GameObject.Find("Player").GetComponent<PlayerMain>().DisablePlayerActions();
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume() {
        DisableAllButtons();
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        isPaused = false;
        GameObject.Find("Player").GetComponent<PlayerMain>().EnablePlayerActions();
    }

    public void MainMenu() {
        DisableAllButtons();

        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        isPaused = false;
        GameObject.Find("Player").GetComponent<PlayerMain>().EnablePlayerActions();

        DataPersistenceManager.instance.doNotSave = true;
        Loader.LoadScene(Loader.Scenes.MainMenu);
    }

    private void DisableAllButtons() {
        resumeButton.interactable = false;
        mainMenuButton.interactable = false;
    }

    private void EnableAllButtons() {
        resumeButton.interactable = true;
        mainMenuButton.interactable = true;
    }

}
