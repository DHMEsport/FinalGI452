using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Initializer : MonoBehaviour
{
    private const string LOADING_SCREEN_NAME = "GamePlayManager";

    private void Awake()
    {
        Scene loadingScreenScene = SceneManager.GetSceneByName(LOADING_SCREEN_NAME);

        if (loadingScreenScene == null || !loadingScreenScene.isLoaded)
        {
            SceneManager.LoadScene(LOADING_SCREEN_NAME, LoadSceneMode.Additive);
            Debug.Log("Screen is loading");
        }
    }
}

