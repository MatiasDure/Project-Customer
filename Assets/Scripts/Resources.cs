using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts
{
    public class Resources: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI foodText;
        [SerializeField] private TextMeshProUGUI medicText;
        [SerializeField] private TextMeshProUGUI moneyText;

        private int _moneyAmount;
        private int _foodAmount;
        private int _medicAmount;

        private static Resources _resource;
        public static Resources Resource { get => _resource;  }
        public int Money { get => _moneyAmount; }
        public int Food { get => _foodAmount; }
        public int Medicine { get => _medicAmount; }

        private void Start()
        {
            _moneyAmount = 0;
            _foodAmount = 0;
            _medicAmount = 0;
        }

        private void Update() => UpdateUiText();  

        private void UpdateUiText()
        {
            foodText.SetText(""+ _foodAmount);
            medicText.SetText("" + _medicAmount);
            moneyText.SetText("" + _moneyAmount);
            Debug.Log(_foodAmount);
        }
        public void AddMoney(int amount) => _moneyAmount += amount;
        public void AddFood(int amount) => _foodAmount += amount;
        public void AddMedicine(int amount) => _medicAmount += amount;

    }
}
