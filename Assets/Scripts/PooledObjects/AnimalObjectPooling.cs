using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class AnimalObjectPooling : ObjectPooling
    {
        [SerializeField] private Cages _cageSystem;

        public Cages CageSystem { get => _cageSystem; }
        public static AnimalObjectPooling SharedAnimalInstance { get; private set;}
        
        void Awake()
        {
            SharedAnimalInstance = this;
        }

        protected override void Start()
        {
            base.Start();
            if (_cageSystem == null) Debug.LogWarning("You need to add the cageSystem in the animalPooling!");
        }

    }
}
