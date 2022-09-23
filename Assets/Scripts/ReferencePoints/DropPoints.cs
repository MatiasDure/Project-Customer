using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

namespace Assets.Scripts
{
    public class DropPoints : MonoBehaviour
    {
        [SerializeField] DropPoint[] cagesInShelter;

        public static DropPoints Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null) Instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            if (cagesInShelter.Length == 0) Debug.LogWarning("You should add each cage into the Cages Script!");
        }

        public DropPoint FindAvailableCage()
        {
            for(int i = 0; i < cagesInShelter.Length; i++)
            {
                if (IsCageAvailable(cagesInShelter[i])) return cagesInShelter[i];
            }
            Debug.Log("All cages are occupied at the moment!");
            return null;
        }

        public int AmountAvailableCages()
        {
            int available = 0;

            for(int i = 0; i < cagesInShelter.Length; i++)
            {
                if (IsCageAvailable(cagesInShelter[i])) available++;
            }

            return available;
        }

        private bool IsCageAvailable(DropPoint cage) => !cage.Occupied;

    }
}
