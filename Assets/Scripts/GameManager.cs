using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Manager { get; private set; }

        private bool _isPaused;
        public bool IsPaused { get => _isPaused; }


        void Awake()
        {
            if (Manager == null)
            {
                Manager = this;
                SubscribeToEvent();
            }

        }

        private void Start()
        {
            _isPaused = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _isPaused = !_isPaused;
                if (_isPaused) ButtonBehaviour.Instance.PauseGame();
                else ButtonBehaviour.Instance.ResumeGame();
            }
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
    }
}
