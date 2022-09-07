using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

namespace Assets.Scripts
{
    public class Resources: MonoBehaviour
    {
        private int _money;
        private int _food;
        private int _medicine;

        private static Resources _resource;
        public static Resources Resource { get => _resource;  }
        public int Money { get => _money; }
        public int Food { get => _food; }
        public int Medicine { get => _medicine; }  
        
        public void AddMoney(int amount) => _money += amount;
        public void AddFood(int amount) => _food += amount;
        public void AddMedicine(int amount) => _medicine += amount;
        
    }
}
