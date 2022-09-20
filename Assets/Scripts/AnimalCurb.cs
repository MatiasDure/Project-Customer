using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class AnimalCurb : MonoBehaviour
    {
        [SerializeField] Animal animal;
        [SerializeField] GameObject display;
        [SerializeField] GameObject euthanizeButton;
        [SerializeField] GameObject takeInButton;

        private int emptyVaccine = 0;   

        public void EuthanizeButton()
        {
            animal.gameObject.SetActive(false);
            //if animal has been taken, remove from the cage after euthanize
            if (animal.CurrentCage != null) animal.CurrentCage.RemoveAnimal();

            //Reset the animal values
            animal.RemoveFromDropPoint();
            animal.RemoveAnimal();
            //animal.ResetValues();
            //ResetButtons();
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
            Cage availableCage = AnimalObjectPooling.SharedAnimalInstance.CageSystem.FindAvailableCage();//Cages.Instance.FindAvailableCage();
            if(availableCage == null)
            {
                Debug.Log("no cage avaialable to take in the pet!");
                return;
            }

            //Taking in the pet
            //display.SetActive(false);

            //Removing take in button
            takeInButton.SetActive(false);
            //centering euthanize button
            euthanizeButton.transform.localPosition = Vector3.zero;
            RemoveFromDropPoint();
            PlacePetInCage(availableCage);
            animal.TakeIn(availableCage);
            animal.AnimalInf.ResetInfoText();


            if (checkMedic) Resources.Resource.UseMedic(1);
            else Resources.Resource.SpendMoney(35);
        }
       
        private void PlacePetInCage(Cage cage)
        {
            animal.transform.position = cage.transform.position;
            cage.AddAnimal(animal.gameObject);
        }

        private void RemoveFromDropPoint()
        {
            animal.CurrentCage.RemoveAnimal();
            animal.RemoveFromDropPoint();
        }

        public void ResetButtons()
        {
            takeInButton.SetActive(true);
            euthanizeButton.transform.localPosition = new Vector3(.7f, 0, 0);
        }
    }
}