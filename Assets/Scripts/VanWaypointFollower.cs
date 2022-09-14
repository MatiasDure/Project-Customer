using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanWaypointFollower : WaypointsFollower
{

    protected override void Start()
    {
        base.Start();
        speed = UnityEngine.Random.Range(0.05f, 0.1f);
    }

    protected override void Update()
    {
        base.Update();
        if(reachedTheEnd) DropAnimal();
    }

    private void DropAnimal()
    {
        //spawn animals on the curb

        //Reset and deactivate van waypoint  
        reachedTheEnd = false;
        gameObject.SetActive(false);

        //Trying to get object from pool
        GameObject animal = AnimalObjectPooling.SharedAnimalInstance.GetPooledObject();
        if (animal == null) return;

        //Trying to get the animal component from the animal prefab
        AnimalPicker animalScript = animal.GetComponent<AnimalPicker>();

        //Setting the animal's type, position, and activating it
        animalScript.ChooseAnimalType();
        animal.transform.position = gameObject.transform.position + animalScript.OffsetStartPosition + new Vector3(0,0,UnityEngine.Random.Range(-3,4)); 
        animal.SetActive(true);
    }
}
