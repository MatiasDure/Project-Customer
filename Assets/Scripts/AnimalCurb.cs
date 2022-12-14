using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class AnimalCurb : MonoBehaviour
    {
        [SerializeField] Animal animal;
        [SerializeField] GameObject display;
        [SerializeField] GameObject euthanizeButton;
        [SerializeField] GameObject takeInButton;
        [SerializeField] float timer;
        [SerializeField] private Sprite[] animalDecisionSprites;
        [SerializeField] private Image vetIconImage;
        [SerializeField] private GameObject vetIconDisplay;
        [SerializeField] private Animator animator;

//        private Animator anim;
        private int emptyVaccine = 0;
        public Cage bed;
        private bool foundBed, takeInPressed, euthanizedPressed;
        float startingTime;

        private void Awake()
        {
            if(animator == null) animator = GetComponent<Animator>();
        }

        private void Start()
        {
            startingTime = timer;
            vetIconDisplay.SetActive(false);
        }

        private void Update()
        {
            if (GameManager.Manager.IsPaused) return;
            if((euthanizedPressed || takeInPressed) && !foundBed) TransferToVet();
            else if(foundBed)
            {
                UpdateVetTimer();

                if(timer <= 0)
                {
                    AnimationManager.Instance.TriggerAnimation(AnimationManager.Anim.Vet,false);
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
                else if(!vetIconDisplay.activeInHierarchy)
                {
                    vetIconDisplay.SetActive(true);
                    if (euthanizedPressed) vetIconImage.sprite = animalDecisionSprites[1];
                    else vetIconImage.sprite = animalDecisionSprites[0];
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
//            anim.SetTrigger("Red Vignette");
//            anim.SetTrigger("VetEvent");
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
                if (euthanizedPressed)
                {
                    if(animal.Type == Animal.AnimalType.Cat) AudioManager.PlaySound(AudioManager.Sound.catDeath);
                    else AudioManager.PlaySound(AudioManager.Sound.dogDeath);
                    AnimationManager.Instance.TriggerAnimation(AnimationManager.Anim.Vet,true);
                }
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
            if (GameManager.Manager.IsPaused) return;
            euthanizedPressed = true;
            takeInButton.SetActive(false);
            euthanizeButton.SetActive(false);
        }

        public void TakeButton()
        {
            if (GameManager.Manager.IsPaused) return;
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
            vetIconDisplay.SetActive(false);
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