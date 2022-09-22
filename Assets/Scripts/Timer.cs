using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private Slider timerBar;
        [SerializeField] private Image fillArea;

        private void Awake()
        {
            if (timerBar == null) timerBar = GetComponentInChildren<Slider>();
            if (timerBar == null) Debug.LogWarning("Add fill area to the timer script!");
            SetTimerValues();
        }

        public void PassInMaxValue(int maxVal)
        {
            timerBar.maxValue = maxVal;
            timerBar.value = maxVal;
        }

        public void UpdateTimerValue(float value)
        {
            timerBar.value = value;
            ChangeColor();
        }

        private void SetTimerValues()
        {
            timerBar.minValue = 0;
        }

        private void ChangeColor()
        {
            if(timerBar.value >= timerBar.maxValue * 0.66f) fillArea.color = Color.green;
            else if (timerBar.value >= timerBar.maxValue * 0.33f) fillArea.color = Color.yellow;
            else fillArea.color = Color.red;
        }

    }
}