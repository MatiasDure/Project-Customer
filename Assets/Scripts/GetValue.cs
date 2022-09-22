using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Assets.Scripts
{
    public class GetValue : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI textHolder;

        public static GetValue Instance { get; private set; }
        private void Awake()
        {
            if (Instance == null) Instance = this;
        }

        // Update is called once per frame
        void Update()
        {
            textHolder.text = string.Format("Animals Saved: {0}\n\nHigh Score: {1}",GameManager.Manager.AnimalSaved, GameManager.Manager.HighScore);
        }

    }
}
