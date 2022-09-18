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
        //[SerializeField] private GameObject notPausedUI;
        [SerializeField] private GameObject pausedUI;
        [SerializeField] private GameObject storeCloseUI;
        [SerializeField] private GameObject storeOpenUI;

        public static event Action OnResume;

        public static ButtonBehaviour Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null) Instance = this;
        }

        public void ExitGame() => Application.Quit();

        public void LoadScene(string newScene)
        {
            SceneManager.LoadScene(newScene);
            //OnPaused?.Invoke(this, false);
        }

        public void PauseGame()
        {
            //notPausedUI.SetActive(false);
            pausedUI.SetActive(true);
            //Time.timeScale = 0;
            //OnPaused?.Invoke(this, true);
        }

        public void ResumeGame()
        {
            //notPausedUI.SetActive(true);
            pausedUI.SetActive(false);
            OnResume?.Invoke();
            //Time.timeScale = 1;
            //OnPaused?.Invoke(this, false);
        }

        public void OpenStore()
        {
            if (GameManager.Manager.IsPaused) return;
            storeOpenUI.SetActive(true);
            storeCloseUI.SetActive(false);
        }

        public void CloseStore()
        {
            if (GameManager.Manager.IsPaused) return;
            storeCloseUI.SetActive(true);
            storeOpenUI.SetActive(false);
        }
    }
}
