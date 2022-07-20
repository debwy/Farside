using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneNextScene : MonoBehaviour
{
    void OnEnable() {
        Loader.LoadScene(Loader.Scenes.Map1);
    }
}
