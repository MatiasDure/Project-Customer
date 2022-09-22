using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class AnimalObjectPooling : ObjectPooling
    {
        [SerializeField] private Cages _cageSystem;
        [SerializeField] private Cages _vetBedSystem;

        public Cages CageSystem { get => _cageSystem; }
        public Cages VetBedSystem { get => _vetBedSystem; }
        public static AnimalObjectPooling SharedAnimalInstance { get; private set;}
        
        void Awake()
        {
            SharedAnimalInstance = this;
            
        }

        protected override void Start()
        {
            base.Start();
            if (_cageSystem == null) Debug.LogWarning("You need to add the cageSystem in the animalPooling!");
            if (_vetBedSystem == null) Debug.LogWarning("You need to add the vetBedSystem in the animalPooling!");
        }

    }
}
