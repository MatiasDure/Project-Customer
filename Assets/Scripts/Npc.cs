using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Npc : MonoBehaviour
    {
        [SerializeField] private Material _mat;
        private Animal.AnimalType _preference;
        private float waitingTime;

        public Animal.AnimalType Preference { get => _preference; }
        public Material Mat { get => _mat; }
        public bool HandedPet { get => _handedPet; }
        public bool ImOut { get => _imOut; }

        bool _imOut;
        bool _handedPet;
        bool selected;

        // Start is called before the first frame update
        void Start()
        {
            _imOut = false;
            selected = false;
            waitingTime = UnityEngine.Random.Range(20.0f, 90.0f);
            int randomNum = UnityEngine.Random.Range(0, 2);
            switch(randomNum)
            {
                case 0:
                    _preference = Animal.AnimalType.Cat;
                    break;
                case 1:
                    _preference = Animal.AnimalType.Dog;
                    break;
                default:
                    _preference = Animal.AnimalType.Dog;
                    break;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!_imOut && !TimeLeft() && !_handedPet) _imOut = true;

            else UpdateTimer();
        }

        private bool TimeLeft() => waitingTime > 0;

        private void UpdateTimer() => waitingTime -= Time.deltaTime;

        public void HandPet() => _handedPet = true;

        public void Select() => selected = !selected;

    }
}
