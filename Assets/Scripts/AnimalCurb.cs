using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class AnimalCurb : MonoBehaviour
    {
        [SerializeField] Animal animal;
        [SerializeField] GameObject display;

        public void EuthanizeButton()
        {
            animal.gameObject.SetActive(false);
            animal.ResetValues();
        }

        public void TakeButton()
        {
            bool checkMedic = Resources.Resource.Medicine > 0;
            //Change magic number 40 to price of vaccine!!
            bool checkMoney = Resources.Resource.Money >= 40;

            //Checks whether there are enough resources to accept a new animal, or else gets euthanize
            if (!(checkMedic || checkMoney)) return;
            display.SetActive(false);
            //pay for vaccination and take in animal
            animal.TakeIn();
            animal.AnimalInf.ResetInfoText();

            if (checkMedic) Resources.Resource.UseMedic(1);
            else Resources.Resource.SpendMoney(35);
        }
    }
}