using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    private GameObject animalSelected;
    private GameObject npcSelected;

    Animal.AnimalType npcAnimalPreference;
    Animal.AnimalType animalTypeSelected;

    private Dictionary<GameObject, Animal> animals;
    private Dictionary<GameObject, Npc> npcs;

    private void Start()
    {
        animalSelected = null;
        npcSelected = null;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray,out hit,80.0f))
            {
                if(hit.transform != null)
                {
                    string gameObjectTag = hit.transform.tag;
                    if (gameObjectTag == "Animal") SelectAnimal(hit.transform.gameObject);
                    else if (gameObjectTag == "Npc") SelectNpc(hit.transform.gameObject);
                }
            }
        }
        CheckMatch();
    }

    //Select the animal by clicking
    private void SelectAnimal(GameObject selected)
    {
        //checking if the user pressed on a different animal
        if(animalSelected != selected)
        {
            Animal selectedAnimal = RetrieveAnimalScript(selected);
            if (!selectedAnimal.Taken) return; 
            if(animalSelected != null)
            {
                //Returning the previously selected animal to its original color
                Animal previousSelectedAnimal = RetrieveAnimalScript(animalSelected);
                Renderer previousSelectedRenderer = previousSelectedAnimal.Render;
                previousSelectedRenderer.material.color = previousSelectedAnimal.Mat.color;
                previousSelectedAnimal.Select();
            }

            //Assigning new selected animal 
            animalSelected = selected;

            //Retrieving scripts from new animal selected

            //Avoid Selecting an animal in the curb

            Renderer renderer = selectedAnimal.Render;

            //Retrieving the animal type
            animalTypeSelected = selectedAnimal.Type; 

            //marking new animal selected as selected
            selectedAnimal.Select();

            //changing the selected animal's color
            renderer.material.color = Color.blue;
        }

    }

    private void SelectNpc(GameObject selected)
    {
        //checking if the user pressed on a different npc
        if (npcSelected != selected)
        {
            if (npcSelected != null)
            {
                //Returning the previously selected npc to its original color
                Npc previousSelectedNpc = RetrieveNpcScript(npcSelected);
                Renderer previousSelectedRenderer = previousSelectedNpc.Render;
                previousSelectedRenderer.material.color = previousSelectedNpc.Mat.color;
                previousSelectedNpc.Select();
            }

            //Assigning new selected npc 
            npcSelected = selected;

            //Retrieving scripts from new animal selected
            Npc selectedNpc = RetrieveNpcScript(npcSelected);
            Renderer renderer = selectedNpc.Render; 

            //Retrieving the animal preference of the npc
            npcAnimalPreference = selectedNpc.Preference;

            //marking new animal selected as selected
            selectedNpc.Select();

            //changing the selected animal's color
            renderer.material.color = Color.blue;
        }
    }

    private void CheckMatch()
    {
        if(npcSelected != null && animalSelected != null)
        {
            if (npcAnimalPreference == animalTypeSelected)
            {
                Npc npcScript = RetrieveNpcScript(npcSelected);

                //Checking whether the npc is already leaving the animal shelter
                if (npcScript.HandedPet || npcScript.ImOut)
                {
                    Debug.Log("npc is leaving the store");
                    npcSelected = null;
                    return;
                }
                npcScript.HandPet();
                RetrieveAnimalScript(animalSelected).RemoveAnimal();
                ResetAfterMatch();
            }
        }
    }

    private void ResetAfterMatch()
    {
        npcSelected = null;
        animalSelected = null;
    }
    private Animal RetrieveAnimalScript(GameObject animal) => animal.GetComponent<Animal>();
    private Npc RetrieveNpcScript(GameObject npc) => npc.GetComponent<Npc>();

}
