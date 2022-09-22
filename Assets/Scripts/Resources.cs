using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using TMPro;
//using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts
{
    public class Resources: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI foodText;
        [SerializeField] private TextMeshProUGUI medicText;
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private int _vaccineCost;
        [SerializeField] private int _foodCost;

        Dictionary<GameObject,TextMeshProUGUI> floatingText;

        private int _moneyAmount;
        private int _foodAmount;
        private int _medicAmount;

        private Vector3 offset;

        private static Resources _resource;
        public static Resources Resource { get => _resource;  }
        public int Money { get => _moneyAmount; }
        public int Food { get => _foodAmount; }
        public int Medicine { get => _medicAmount; }
        public int VaccineCost { get => _vaccineCost; }
        public int FoodCost { get => _foodCost; }


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
        }

        private void Update()
        {
            if (GameManager.Manager.IsPaused) return;
            UpdateUiText();
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

        public void AddFood(int amount, int foodPrice)
        {
            if (GameManager.Manager.IsPaused) return;
            DisplayFloatingTxt("+" + amount, foodText.transform.position + offset, Color.green);
            _foodAmount += amount;
        }

        public void AddMedicine(int amount, int medicPrice)
        {
            if (GameManager.Manager.IsPaused) return;
            DisplayFloatingTxt("+" + amount, medicText.transform.position + offset, Color.green);
            _medicAmount += amount;
        }

        public void EatFood(int amount)
        {
            if (!(_foodAmount >= amount)) return;
            DisplayFloatingTxt("-" + amount, foodText.transform.position + offset, Color.red);
            _foodAmount -= amount;
        }
        public void UseMedic(int amount)
        {
            if (!(_medicAmount >= amount)) return;
            DisplayFloatingTxt("-" + amount, medicText.transform.position + offset, Color.red);
            _medicAmount -= amount;
        }

        private bool EnoughMoney(int amountToBuy, int priceOfObject)
        {
            int leftOverMoney = _moneyAmount - amountToBuy * priceOfObject;
            return leftOverMoney >= 0;
        }

        public void SpendMoney(int amountSpent)
        {
            AudioManager.PlaySound(AudioManager.Sound.SpendMoney);
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
