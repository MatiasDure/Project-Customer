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

        Dictionary<GameObject,TextMeshProUGUI> floatingText;

        private int _moneyAmount;
        private int _foodAmount;
        private int _medicAmount;
        private float foodTimer;
        private float startingFoodTime;
        private float medicTimer;
        private float startingMedicTime;

        private Vector3 offset;

        private static Resources _resource;
        public static Resources Resource { get => _resource;  }
        public int Money { get => _moneyAmount; }
        public int Food { get => _foodAmount; }
        public int Medicine { get => _medicAmount; }


        private void Awake()
        {
            _resource = this;
        }

        private void Start()
        {
            floatingText = new Dictionary<GameObject, TextMeshProUGUI>();
            offset = new Vector3(0, -20f, 0);
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
            DisplayFloatingTxt("+" + amount,moneyText.transform.position + offset,Color.green);
            _moneyAmount += amount;
        }

        public void AddFood(int amount)
        {
            int foodPrice = 10;
            if (!EnoughMoney(amount, foodPrice) || GameManager.Manager.IsPaused) return;
            DisplayFloatingTxt("+" + amount, foodText.transform.position + offset, Color.green);
            _foodAmount += amount;
            SpendMoney(foodPrice * amount);
        }

        public void AddMedicine(int amount)
        {
            int medicPrice = 30;
            if (!EnoughMoney(amount, medicPrice) || GameManager.Manager.IsPaused) return;
            DisplayFloatingTxt("+" + amount, medicText.transform.position + offset, Color.green);
            _medicAmount += amount;
            SpendMoney(amount * medicPrice);
        }

        private void EatFood(int amount)
        {
            DisplayFloatingTxt("" + -amount, foodText.transform.position + offset, Color.red);
            _foodAmount -= amount;
        }
        private void UseMedic(int amount)
        {
            DisplayFloatingTxt("" + -amount, medicText.transform.position + offset, Color.red);
            _medicAmount -= amount;
        }

        private bool TimeToUseResource(float timeLeft)
        {
            return timeLeft <= 0;
        }

        private float UpdateTimer(float currentTime)
        {
            return currentTime -= Time.deltaTime;
        }


        private bool EnoughMoney(int amountToBuy, int priceOfObject)
        {
            int leftOverMoney = _moneyAmount - amountToBuy * priceOfObject;
            return leftOverMoney > 0;
        }

        private void SpendMoney(int amountSpent)
        {
            DisplayFloatingTxt("" + -amountSpent,moneyText.transform.position + offset, Color.red);
            _moneyAmount -= amountSpent;
        }

        private void DisplayFloatingTxt(string txt, Vector3 pos, Color c)
        {
            //GameObject floatingTxt = ObjectPooling.SharedInstance.GetPooledObject();
            GameObject floatingTxt = FloatingTextObjectPooling.SharedFloatInstance.GetPooledObject();

            if (floatingTxt == null) return;

            //Checking whether the Floating text object was added to the dictionary. If not, add it.
            if (!floatingText.ContainsKey(floatingTxt)) floatingText.Add(floatingTxt, floatingTxt.GetComponent<TextMeshProUGUI>());

            floatingText[floatingTxt].text = txt;
            floatingText[floatingTxt].color = c;
            floatingTxt.transform.position = pos;
            floatingTxt.SetActive(true);
        }

    }
}
