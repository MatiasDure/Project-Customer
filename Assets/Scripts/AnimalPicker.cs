using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class AnimalPicker : MonoBehaviour
    {
        //[SerializeField] private Animal.AnimalType _type;
        [SerializeField] private Animal attachedAnimalScript;
        [SerializeField] private Animal[] animals; //pass in prefabs of animals
                                                   //[SerializeField] private Renderer _render;
        [SerializeField] private Vector3 _offsetStartPosition;

        //changed from serializeField
        // private int medicConsume;
        //private int foodConsume;
        //private Material _mat; //might need to delete this

        //public Material Mat { get => _mat; }

        //public Animal.AnimalType Type { get => _type; }

        public Vector3 OffsetStartPosition { get => _offsetStartPosition; }

        //public Renderer Render { get => _render; }

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
            //foodConsume = animalChosen.FoodConsume;
            //medicConsume = animalChosen.MedicConsume;
            //_mat = animalChosen.Mat;
            //_type = animalChosen.Type;
            //_render.material = _mat;
            //animalChosen._selected = false;    
        }
    }
}

