using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class AnimalObjectPooling : ObjectPooling
    {
        public static AnimalObjectPooling SharedAnimalInstance { get; private set;}
        
        void Awake()
        {
            SharedAnimalInstance = this;
        }
        
    }
}
