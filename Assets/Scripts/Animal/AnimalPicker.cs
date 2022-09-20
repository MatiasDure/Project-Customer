using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class AnimalPicker : MonoBehaviour
    {
        [SerializeField] private Animal attachedAnimalScript;
        [SerializeField] private Animal[] animals; 
        [SerializeField] private Vector3 _offsetStartPosition;

        public Vector3 OffsetStartPosition { get => _offsetStartPosition; }

        private void Awake()
        {
            if(attachedAnimalScript == null) attachedAnimalScript = GetComponent<Animal>();
        }

        public void ChooseAnimalType()
        {
            if (animals.Length == 0) return;
            int randIndex = Random.Range(0, animals.Length);
            DeclareNewAnimal(randIndex);
        }

        private void DeclareNewAnimal(int index)
        {
            Animal animalChosen = animals[index];
            attachedAnimalScript.AssignChosenValues(animalChosen);
        }
    }
}

