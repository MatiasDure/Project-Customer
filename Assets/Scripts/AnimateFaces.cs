using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class AnimateFaces : MonoBehaviour
    {
        [SerializeField] private Image[] faces;

        private void Awake()
        {
            if (faces.Length != 5) Debug.LogWarning("There needs to be 5 images in the AnimateFaces script!");
        }

        private void Start()
        {
            ResetValues();
        }

        private void Update()
        {
            ChangeFaces();
        }

        private void ChangeFaces()
        {
            if (GameManager.Manager.WrongAttempts <= 0) return;

            int indexToDisable = -1;
            switch(GameManager.Manager.WrongAttempts)
            {
                case 1:
                    indexToDisable = 0;
                    break;
                case 2:
                    indexToDisable = 1;
                    break;
                case 3:
                    indexToDisable = 2;
                    break;
                case 4:
                    indexToDisable = 3;
                    break;
                case 5:
                    indexToDisable = 4;
                    break;
                default:
                    indexToDisable = -1;
                    break;
            }
    
            if(indexToDisable >= 0 && faces[indexToDisable].gameObject.activeInHierarchy) faces[indexToDisable].gameObject.SetActive(false);
        }

        void ResetValues()
        {
            for(int i = 0; i < faces.Length; i++)
            {
                faces[i].gameObject.SetActive(true);
            }
        }
    }
}
