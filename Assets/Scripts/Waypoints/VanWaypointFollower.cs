using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanWaypointFollower : WaypointsFollower
{
    private Dictionary<GameObject, AnimalPicker> chooseAnimal;
    private Dictionary<GameObject, Animal> animalStorage;

    protected override void Start()
    {
        base.Start();
        chooseAnimal = new Dictionary<GameObject, AnimalPicker>();
        animalStorage = new Dictionary<GameObject, Animal>();
    }
    protected override void Update()
    {
        base.Update();
        if(reachedTheEnd) DropAnimal();
    }

    protected override void MoveTowardsWayPoint()
    {
        target = waypoints[currentIndex];
        gameObject.transform.LookAt(target.transform);
        base.MoveTowardsWayPoint();
    }

    private void DropAnimal()
    {
        //spawn animals on the curb

        //Reset and deactivate van
        reachedTheEnd = false;
        gameObject.SetActive(false);

        //Trying to find a drop point for the animal
        Cage dropPoint = VanObjectPooling.SharedVanInstance.DropPoints.FindAvailableCage();
        if (dropPoint == null) return; 
        
        //Trying to get object from pool
        GameObject animal = AnimalObjectPooling.SharedAnimalInstance.GetPooledObject();
        if (animal == null) return;

        AnimalPicker animalPickerScript;
        Animal animalScript;

        //Checking whether we have the animal in our dictionary
        if (!chooseAnimal.ContainsKey(animal))
        {
            animalPickerScript = animal.GetComponent<AnimalPicker>();
            animalScript = animal.GetComponent<Animal>();
            chooseAnimal.Add(animal, animalPickerScript);
            animalStorage.Add(animal, animalScript);
        }
        else
        {
            animalPickerScript = chooseAnimal[animal];
            animalScript = animalStorage[animal];
        }

        //Setting the animal's type, position, and activating it
        animalPickerScript.ChooseAnimalType();
        animal.transform.position = dropPoint.transform.position;
        if (animalScript.Type == Animal.AnimalType.Cat) AudioManager.PlaySound(AudioManager.Sound.catMeow);
        else AudioManager.PlaySound(AudioManager.Sound.dogBark);
        animalScript.PlaceInCage(dropPoint);
        dropPoint.AddAnimal(animal);
        animal.SetActive(true);
    }
}
