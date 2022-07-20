using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    void OnEnable()
    {
        //Only specifying the sceneName or sceneBuildIndex will load the scene with the Single mode
        SceneManager.LoadScene("Map1", LoadSceneMode.Single);
    }
}
