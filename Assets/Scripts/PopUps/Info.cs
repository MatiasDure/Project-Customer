using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Info : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI textField;
        [SerializeField] protected GameObject display;

        protected Animal.AnimalType animalType;

        protected virtual void Awake()
        {
            if (textField == null) textField = gameObject.transform.parent.GetComponentInChildren<TextMeshProUGUI>();
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            if (GameManager.Manager.IsPaused) return;
            UpdateText();
        }

        private void OnMouseOver()
        {
            //toggle by clicking
            //if (Input.GetMouseButtonDown(0)) display.SetActive(!display.activeInHierarchy);
        }

        protected virtual void UpdateText() { }
    }
}