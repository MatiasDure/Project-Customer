using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] private string sceneName;
    // Update is called once per frame
    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (Input.GetKeyDown(KeyCode.Escape) && currentScene.name != "MainMenuScene") ButtonBehaviour.Instance.LoadScene("MainMenuScene");
        else if (currentScene.name == "MainMenuScene" && Input.GetMouseButtonDown(0)) return;
        else if (Input.anyKeyDown) ButtonBehaviour.Instance.LoadScene(sceneName);   
    }
}
