using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class AnimalCurb : MonoBehaviour
    {
        [SerializeField] Animal animal;
        [SerializeField] GameObject display;
        private int emptyVaccine = 0;   

        public void EuthanizeButton()
        {
            animal.gameObject.SetActive(false);
            animal.ResetValues();
        }

        public void TakeButton()
        {
            bool checkMedic = Resources.Resource.Medicine > emptyVaccine;
            //Change magic number 40 to price of vaccine!!
            bool checkMoney = Resources.Resource.Money >= Resources.Resource.VaccineCost;

            //Checks whether there are enough resources to accept a new animal, or else gets euthanize
            if (!(checkMedic || checkMoney))
            {
                Debug.Log("There is no money to take in the pet!");
                return;
            }
            //Check if there is empty cage to put the animal inside
            Cage availableCage = Cages.Instance.FindAvailableCage();
            if(availableCage == null)
            {
                Debug.Log("no cage avaialable to take in the pet!");
                return;
            }

            //Taking in the pet
            display.SetActive(false);
            PlacePetInCage(availableCage);
            animal.TakeIn(availableCage);
            animal.AnimalInf.ResetInfoText();


            if (checkMedic) Resources.Resource.UseMedic(1);
            else Resources.Resource.SpendMoney(35);
        }
       
        private void PlacePetInCage(Cage cage)
        {
            animal.transform.position = cage.transform.position;
            cage.AddAnimal(animal);
        }
    }
}