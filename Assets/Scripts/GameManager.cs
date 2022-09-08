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
                DontDestroyOnLoad(gameObject);
                SubscribeToEvent();
            }
            else Destroy(gameObject);

        }

        private void Start()
        {
            _isPaused = false;
        }

        // Update is called once per frame
        void Update()
        {
        }

        void PauseGame(System.Object sender,bool pause) => _isPaused = pause;

        void SubscribeToEvent()
        {
            ButtonBehaviour.OnPaused += PauseGame;
        }

        void UnsubscribeFromEvent()
        {
            ButtonBehaviour.OnPaused -= PauseGame;
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvent();
        }
    }
}
