using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Npc : MonoBehaviour
    {
        [SerializeField] private Material _mat;
        [SerializeField] private Animal.AnimalType _preference;
        [SerializeField] private Renderer _render;
        [SerializeField] private GameObject display;
        [SerializeField] private NpcInfo npcInfo;
        private float _waitingTime;

        public float WaitingTime { get => _waitingTime; }
        public Animal.AnimalType Preference { get => _preference; }
        public Material Mat { get => _mat; }
        public Renderer Render { get => _render; }
        public bool HandedPet { get => _handedPet; }
        public bool ImOut { get => _imOut; }

        bool _imOut;
        bool _handedPet;
        bool selected;

        private void Awake()
        {
            if (_mat == null) _mat = GetComponent<Material>();
            if (_render == null) _render = GetComponent<Renderer>();
            if (npcInfo == null) npcInfo = GetComponent<NpcInfo>();
            if (display == null) Debug.LogWarning("You need to add the display to the NPC script!");
        }
        // Start is called before the first frame update
        void Start()
        {
            ResetNpc();            
        }

        // Update is called once per frame
        void Update()
        {
            if (!_imOut && !TimeLeft()) _imOut = true;

            else UpdateTimer();
        }

        private bool TimeLeft() => _waitingTime > 0;

        private void UpdateTimer()
        {
            if(_waitingTime <= 0 || _handedPet)
            {
                _waitingTime = 0;
                return;
            }
            _waitingTime -= Time.deltaTime;
        }

        public void HandPet() => _handedPet = true;

        public void Select() => selected = !selected;

        private void PickingRandomPreference()
        {
            int randomNum = UnityEngine.Random.Range(0, 2);
            switch (randomNum)
            {
                case 0:
                    _preference = Animal.AnimalType.Cat;
                    break;
                case 1:
                    _preference = Animal.AnimalType.Dog;
                    break;
                default:
                    _preference = Animal.AnimalType.Dog;
                    break;
            }
        }

        private void ResetWaitingTime() => _waitingTime = UnityEngine.Random.Range(20.0f, 90.0f);

        public void ResetNpc()
        {
            selected = false;
            _handedPet = false;
            _imOut = false;
            _render.material = _mat;
            if(display != null) display.SetActive(false);
            ResetWaitingTime();
            PickingRandomPreference();
            npcInfo.ResetInfoText();
        }

    }
}
