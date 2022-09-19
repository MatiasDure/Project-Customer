using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Cage : MonoBehaviour
    {
        public bool Occupied { get; private set; }
        public GameObject AnimalInside { get; private set; }

        private void Awake()
        {
            Occupied = false;
        }

        public void AddAnimal(GameObject animal)
        {
            AnimalInside = animal;
            Occupied = true;
        }

        public void RemoveAnimal()
        {
            AnimalInside = null;
            Occupied = false;
        }

    }
}
