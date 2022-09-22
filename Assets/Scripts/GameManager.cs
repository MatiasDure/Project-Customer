using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private int amountAttempts;
        public static GameManager Manager { get; private set; }

        private bool _isPaused;
        private int _animalSaved;
        private int _wrongAttempts;

        public int WrongAttempts { get => _wrongAttempts; }
        public bool IsPaused { get => _isPaused; }
        public int AnimalSaved { get => _animalSaved; }

        void Awake()
        {
            if (Manager == null)
            {
                Manager = this;
                DontDestroyOnLoad(gameObject);
                SubscribeToEvent();
            }
            else Destroy(gameObject);

        }

        private void Start()
        {
            _isPaused = false;
            amountAttempts = amountAttempts == 0 ? 5 : amountAttempts;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log(_animalSaved);
                _isPaused = !_isPaused;
                if (_isPaused) ButtonBehaviour.Instance.PauseGame();
                else ButtonBehaviour.Instance.ResumeGame();
            }
            if (_wrongAttempts >= amountAttempts) ButtonBehaviour.Instance.LoadScene("LosingScene");
        }

        void PauseGame() => _isPaused = false;

        void SubscribeToEvent()
        {
            ButtonBehaviour.OnResume += PauseGame;
        }

        void UnsubscribeFromEvent()
        {
            ButtonBehaviour.OnResume -= PauseGame;
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvent();
        }

        public void ResetAnimalSavedValue()
        {
            _animalSaved = 0;
        }

        public void ResetGameValues()
        {
            _isPaused = false;
            amountAttempts = amountAttempts == 0 ? 5 : amountAttempts;
            _wrongAttempts = 0;
        }

        public void WrongChoice() => _wrongAttempts++;

        public void SavedAnAnimal() => _animalSaved++;
    }
}
