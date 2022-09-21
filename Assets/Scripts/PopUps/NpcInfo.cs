using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class NpcInfo : Info
    {
        [SerializeField] private Npc npc;

        private int waitingTime;
        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            display.SetActive(true);
            ResetInfoText();
        }

        protected override void UpdateText()
        {
            UpdateNpcWaitingTime();
            //textField.text = string.Format("Wants: {0}\nWaiting: {1}",animalType,waitingTime);
        }

        private void UpdateNpcWaitingTime() => waitingTime = (int)npc.WaitingTime;

        public void ResetInfoText() => animalType = npc.Preference;

    }
}
