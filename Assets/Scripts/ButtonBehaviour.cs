using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

namespace Assets.Scripts
{
    public class ButtonBehaviour : MonoBehaviour
    {
        //Only necessary while playing game
        [SerializeField] private GameObject notPausedUI;
        [SerializeField] private GameObject pausedUI;
        [SerializeField] private GameObject storeCloseUI;
        [SerializeField] private GameObject storeOpenUI;

        public static event EventHandler<bool> OnPaused;

        public void ExitGame() => Application.Quit();

        public void LoadScene(string newScene)
        {
            SceneManager.LoadScene(newScene);
            OnPaused?.Invoke(this, false);
        }

        public void PauseGame()
        {
            notPausedUI.SetActive(false);
            pausedUI.SetActive(true);
            OnPaused?.Invoke(this, true);
        }

        public void ResumeGame()
        {
            notPausedUI.SetActive(true);
            pausedUI.SetActive(false);
            OnPaused?.Invoke(this, false);
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
