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
        private int amountMedic;
        private bool vaccinated;

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
            textField.text = string.Format("Animal: {0}\nFood: {1}\nMed: {2}\nVaccinated: {3}",animalType,amountFood,amountMedic,vaccinated);
        }

        public void ResetInfoText()
        {
            amountFood = animal.FoodConsume;
            amountMedic = animal.MedicConsume;
            animalType = animal.Type;
            vaccinated = animal.Vaccinated;
        }

    }
}