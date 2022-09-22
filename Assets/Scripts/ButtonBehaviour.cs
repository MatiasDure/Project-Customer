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
        
        [SerializeField] private GameObject pausedUI;
        [SerializeField] private GameObject storeCloseUI;
        [SerializeField] private GameObject storeOpenUI;

        public static event Action OnResume;
        public static event Action OnOpenStore;

        public static ButtonBehaviour Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null) Instance = this;
        }

        public void ExitGame() => Application.Quit();

        public void LoadScene(string newScene)
        {
            if(newScene != "LosingScene") GameManager.Manager.ResetAnimalSavedValue();
            GameManager.Manager.ResetGameValues();
            SceneManager.LoadScene(newScene);
        }

        public void PauseGame()
        {
            CloseStore();
            Time.timeScale = 0f;
            pausedUI.SetActive(true);
        }

        public void ResumeGame()
        {
            pausedUI.SetActive(false);
            Time.timeScale = 1f;
            OnResume?.Invoke();
        }

        public void OpenStore()
        {
            if (GameManager.Manager.IsPaused) return;
            OnOpenStore?.Invoke();
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
