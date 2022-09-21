using System;
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
        [SerializeField] float timer;

        private int emptyVaccine = 0;
        public Cage bed;
        private bool foundBed, takeInPressed, euthanizedPressed;
        float startingTime;

        private void Start()
        {
            startingTime = timer;
        }

        private void Update()
        {
            if((euthanizedPressed || takeInPressed) && !foundBed) TransferToVet();
            else if(foundBed)
            {
                UpdateVetTimer();
                if(timer <= 0)
                {
                    if (euthanizedPressed)
                    {
                        Euthanize();
                        ResetValues();
                    }
                    else
                    {
                        PseudoResetValues();
                        TakeIn();
                    }
                }
            }
        }

        private void TakeIn()
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
            Cage availableCage = AnimalObjectPooling.SharedAnimalInstance.CageSystem.FindAvailableCage();
            if (availableCage == null)
            {
                Debug.Log("no cage avaialable to take in the pet!");
                return;
            }

            //Switching Pets cage
            PlacePetInCage(availableCage);
            animal.TakeIn(availableCage);
            animal.AnimalInf.ResetInfoText();

            if (checkMedic) Resources.Resource.UseMedic(1);
            else Resources.Resource.SpendMoney(35);
        }

        private void Euthanize()
        {
            //has to be the animal because it is the component of the main parent
            animal.gameObject.SetActive(false);
        }

        private void UpdateVetTimer()
        {
            timer -= Time.deltaTime;
        }

        public void TransferToVet()
        {
            //looking for bed in vet
            bed = AnimalObjectPooling.SharedAnimalInstance.VetBedSystem.FindAvailableCage();
            if (bed != null)
            {

                //repositioning animal
                animal.transform.position = bed.transform.position;

                //switching cages
                if (animal.CurrentCage != null)
                {
                    animal.CurrentCage.RemoveAnimal();
                    animal.RemoveFromDropPoint();
                }
                bed.AddAnimal(gameObject);
                animal.PlaceInCage(bed);
                foundBed = true;
            }
        }

        public void EuthanizeButton()
        {
            euthanizedPressed = true;
            takeInButton.SetActive(false);
            euthanizeButton.SetActive(false);
        }

        public void TakeButton()
        {
            takeInPressed = true;
            //Removing take in button
            takeInButton.SetActive(false);
            //centering euthanize button
            euthanizeButton.transform.localPosition = Vector3.zero;
        }
       
        private void PlacePetInCage(Cage cage)
        {
            animal.transform.position = cage.transform.position;
            cage.AddAnimal(animal.gameObject);
        }

        private void RemoveFromDropPoint()
        {
            if (animal.CurrentCage == null) return;
            animal.CurrentCage.RemoveAnimal();
            animal.RemoveFromDropPoint();
        }

        public void ResetButtons()
        {
            takeInButton.SetActive(true);
            euthanizeButton.SetActive(true);
            euthanizeButton.transform.localPosition = new Vector3(.7f, 0, 0);
        }

        private void PseudoResetValues()
        {
            euthanizedPressed = false;
            takeInPressed = false;
            timer = startingTime;
            RemoveFromDropPoint();
            foundBed = false;
            if (bed != null)
            {
                bed.RemoveAnimal();
                bed = null;
            }
        }
        private void ResetValues()
        {
            ResetButtons();
            PseudoResetValues();
        }
    }
}