using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal;
using Unity.PlasticSCM.Editor.WebApi;

namespace Assets.Scripts
{
    public class Store : MonoBehaviour
    {
        [SerializeField] private TMP_InputField foodInputField;
        [SerializeField] private TMP_InputField medicInputField;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private int foodPrice;
        [SerializeField] private int medicPrice;

        private int foodAmount, medicAmount, currentPrice;

        private void Awake()
        {
            if (foodInputField == null ||
                medicInputField == null) Debug.LogWarning("Add the input field into the store script!");
            if (priceText == null) Debug.LogWarning("Add the price textmeshpro into the store script!");
        }

        // Start is called before the first frame update
        void Start()
        {
            foodInputField.text = medicInputField.text = "0";
            ButtonBehaviour.OnOpenStore += ResetValues;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                GetInput();
            }
            currentPrice = CalculateCost();
            UpdatePrice(currentPrice);
        }

        private void GetInput()
        {
            int tempFoodAmount; //= int.Parse(foodInputField.text);
            int tempMedicAmount; //= int.Parse(medicInputField.text);

            if (int.TryParse(foodInputField.text, out tempFoodAmount) &&
                int.TryParse(medicInputField.text, out tempMedicAmount))
            {
                //Checking that the input is not less than 0
                foodAmount = tempFoodAmount > 0 ? tempFoodAmount : 0;
                medicAmount = tempMedicAmount > 0 ? tempMedicAmount : 0;
                UpdateInputFieldText();
            }
        }

        private int CalculateCost()
        {
            int costOfFood = foodAmount * foodPrice;
            int costOfMedic = medicAmount * medicPrice;
            return costOfFood + costOfMedic;
        }

        private void UpdatePrice(int price)
        {
            priceText.text = "" + price;
            if (price > Resources.Resource.Money) UpdatePriceColor(Color.red);
            else UpdatePriceColor(Color.green);
        }

        private void UpdatePriceColor(Color c) => priceText.color = c;

        private void UpdateInputFieldText()
        {
            foodInputField.text = ""+foodAmount;
            medicInputField.text = ""+medicAmount;
        }

        public void AddFood()
        {
            foodAmount++;
            UpdateInputFieldText();
        }
        public void SubtractFood()
        {
            if (!PositiveNumber(foodAmount)) return;
            foodAmount--;
            UpdateInputFieldText();
        }

        public void AddMedic()
        {
            medicAmount++;
            UpdateInputFieldText();
        }

        public void SubtractMedic()
        {
            if (!PositiveNumber(medicAmount)) return;
            medicAmount--;
            UpdateInputFieldText();
        }

        private void ResetValues()
        {
            foodAmount = medicAmount = 0;
            foodInputField.text = medicInputField.text = "" + 0;
        }

        private bool PositiveNumber(int number) => number > 0;

        private bool AbleToBuy() => currentPrice <= Resources.Resource.Money && currentPrice != 0;

        public void BuyProducts()
        {
            if (!AbleToBuy()) return;
            Resources.Resource.AddFood(foodAmount,foodPrice);
            Resources.Resource.AddMedicine(medicAmount, medicPrice);
            Resources.Resource.SpendMoney(currentPrice);
            
        }

        private void OnDestroy()
        {
            ButtonBehaviour.OnOpenStore -= ResetValues;
        }
    }
}
