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
        private float foodTimer;
        private float startingFoodTime;
        private float medicTimer;
        private float startingMedicTime;

        private static Resources _resource;
        public static Resources Resource { get => _resource;  }
        public int Money { get => _moneyAmount; }
        public int Food { get => _foodAmount; }
        public int Medicine { get => _medicAmount; }

        private void Start()
        {
            _moneyAmount = 500;
            _foodAmount = 0;
            _medicAmount = 0;
            foodTimer = startingFoodTime = 10;
            medicTimer = startingMedicTime = 90;
        }

        private void Update()
        {
            if (GameManager.Manager.IsPaused) return;
            UpdateUiText();

            //updating timers
            foodTimer = UpdateTimer(foodTimer);
            medicTimer = UpdateTimer(medicTimer);

            if(TimeToUseResource(foodTimer))
            {
                EatFood(2);
                foodTimer = startingFoodTime;
            }
            if (TimeToUseResource(medicTimer))
            {
                UseMedic(1);
                medicTimer = startingMedicTime;
            }
        }

        private void UpdateUiText()
        {
            foodText.SetText("" + _foodAmount);
            medicText.SetText("" + _medicAmount);
            moneyText.SetText("" + _moneyAmount);
        }

        public void AddMoney(int amount)
        {
            if (GameManager.Manager.IsPaused) return;
            _moneyAmount += amount;
        }

        public void AddFood(int amount)
        {
            int foodPrice = 10;
            if (!EnoughMoney(amount, foodPrice) || GameManager.Manager.IsPaused) return;
            _foodAmount += amount;
            SpendMoney(foodPrice * amount);
        }

        private void EatFood(int amount)
        {
            _foodAmount -= amount;
        }

        private bool TimeToUseResource(float timeLeft)
        {
            return timeLeft <= 0;
        }

        private float UpdateTimer(float currentTime)
        {
            return currentTime -= Time.deltaTime;
        }


        public void AddMedicine(int amount)
        {
            int medicPrice = 30;
            if (!EnoughMoney(amount, medicPrice) || GameManager.Manager.IsPaused) return;
            _medicAmount += amount;
            SpendMoney(amount * medicPrice);
        }

        private void UseMedic(int amount)
        {
            _medicAmount -= amount;
        }

        private bool EnoughMoney(int amountToBuy, int priceOfObject)
        {
            int leftOverMoney = _moneyAmount - amountToBuy * priceOfObject;
            return leftOverMoney > 0;
            //_moneyAmount = leftOverMoney;
        }

        private void SpendMoney(int amountSpent)
        {
            _moneyAmount -= amountSpent;
        }

    }
}
