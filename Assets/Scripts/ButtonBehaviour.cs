using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class ButtonBehaviour : MonoBehaviour
    {
        //Only necessary while playing game
        [SerializeField] private GameObject notPausedUI;
        [SerializeField] private GameObject pausedUI;
        [SerializeField] private GameObject storeCloseUI;
        [SerializeField] private GameObject storeOpenUI;

        public void ExitGame() => Application.Quit();

        public void LoadScene(string newScene) => SceneManager.LoadScene(newScene);

        public void PauseGame()
        {
            notPausedUI.SetActive(false);
            pausedUI.SetActive(true);
        }

        public void ResumeGame()
        {
            notPausedUI.SetActive(true);
            pausedUI.SetActive(false);
        }

        public void OpenStore()
        {
            storeOpenUI.SetActive(true);
            storeCloseUI.SetActive(false);
        }

        public void CloseStore()
        {
            storeCloseUI.SetActive(true);
            storeOpenUI.SetActive(false);
        }
    }
}
