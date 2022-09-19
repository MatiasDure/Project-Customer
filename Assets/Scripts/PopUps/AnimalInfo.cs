using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts
{
    public class AnimalInfo : Info
    {
        [SerializeField] private Animal animal;

        private int amountFood;
        private int timeToFeed;
        private int health;

        public GameObject InfoDisplay { get => display; }

        protected override void Awake()
        {
            if(animal == null) animal = GetComponentInParent<Animal>();
            base.Awake();
        }
        // Start is called before the first frame update
        void Start()
        {
            ResetInfoText(); 
        }

        protected override void UpdateText()
        {
            UpdateValues();
            textField.text = string.Format("Animal: {0}\nFood: {1}\nNext meal in: {2}\nHealth: {3}",animalType,amountFood,timeToFeed,health);
        }

        private void UpdateValues()
        {
            timeToFeed = (int)animal.Timer;
            health = animal.Hp;
        }

        public void ResetInfoText()
        {
            amountFood = animal.FoodConsume;
            animalType = animal.Type;
            health = animal.Hp;
        }

    }
}