using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Npc : MonoBehaviour
    {
        [SerializeField] private Material _mat;
        [SerializeField] private Animal.AnimalType _preference;
        [SerializeField] private Image animalPreferenceDisplay;
        [SerializeField] private Sprite[] animalPhotos;
        [SerializeField] private Renderer _render;
        [SerializeField] private GameObject display;
        [SerializeField] private NpcInfo npcInfo;
        [SerializeField] private Timer timeBar; 
        
        private float _waitingTime;
        private Dictionary<Animal.AnimalType, Sprite> animalPicture;

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
            if (npcInfo == null) npcInfo = GetComponentInChildren<NpcInfo>();
            if (timeBar == null) timeBar = GetComponentInChildren<Timer>();
            if (display == null) Debug.LogWarning("You need to add the display to the NPC script!");
            if(animalPreferenceDisplay == null) animalPreferenceDisplay = GetComponentInChildren<Image>();
            if (animalPhotos.Length <= 1) Debug.LogWarning("Include the animal sprites in the NPC script");
            else
            {
                animalPicture = new Dictionary<Animal.AnimalType, Sprite>();
                animalPicture[Animal.AnimalType.Cat] = animalPhotos[0];
                animalPicture[Animal.AnimalType.Dog] = animalPhotos[1];
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            ResetNpc();            
        }

        // Update is called once per frame
        void Update()
        {
            if (!_imOut && !TimeLeft())
            {
                _imOut = true;
                GameManager.Manager.WrongChoice();
                //add angry sound effect
                AudioManager.PlaySound(AudioManager.Sound.AngryNpc);
            }

            else if(!HandedPet) UpdateTimer();
        }

        private bool TimeLeft() => _waitingTime > 0;

        private void UpdateTimer()
        {
            if(_waitingTime <= 0)
            {
                _waitingTime = 0;
                return;
            }
            _waitingTime -= Time.deltaTime;
            timeBar.UpdateTimerValue(_waitingTime);
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
            //if(display != null) display.SetActive(false);
            ResetWaitingTime();
            timeBar.PassInMaxValue((int)_waitingTime);
            PickingRandomPreference();
            animalPreferenceDisplay.sprite = animalPicture[_preference];
            //npcInfo.ResetInfoText();
        }

    }
}
