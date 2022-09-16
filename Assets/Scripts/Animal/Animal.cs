using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Renderer))] 
    public class Animal : MonoBehaviour
    {
        [SerializeField] private int _foodConsume;
        [SerializeField] private int _medicConsume;
        [SerializeField] private GameObject curbDisplay;
        [SerializeField] private AnimalInfo animalInfo;
        [SerializeField] private Material _mat; //might need to delete this
        [SerializeField] private Vector3 _offsetStartPosition;
        [SerializeField] private Renderer _render;
        [SerializeField] private int[] rangeToEat;
        public AnimalType _type;

        public bool _selected;
        public bool Selected { get => _selected; }

        //made public for now
        public bool _taken;
        public bool _vaccinated;

        private float timer;
        private float originalTimer;

        public AnimalInfo AnimalInf { get => animalInfo; }
        public bool Vaccinated { get => _vaccinated; }
        public Material Mat { get => _mat; }

        public bool Taken { get => _taken; }
        public Renderer Render { get => _render; }
        public AnimalType Type { get => _type; }
        public int FoodConsume { get => _foodConsume; }
        public int MedicConsume { get => _medicConsume; }

        public Vector3 OffsetStartPosition { get => _offsetStartPosition; }

        public enum AnimalType
        {
            Cat,
            Dog
        }

        private void Awake()
        {
            if (_render == null) _render = gameObject.GetComponent<Renderer>();
            if (animalInfo == null) animalInfo = gameObject.GetComponentInChildren<AnimalInfo>();
            if (curbDisplay == null) UnityEngine.Debug.LogWarning("You need to add the curbDisplay to the Animal Script!");
        }


        // Start is called before the first frame update
        void Start()
        {
            ResetValues();
        }

        private void Update()
        {
            if(GameManager.Manager.IsPaused) return;
            if (TimeToConsume())
            {
                Consume();
                ResetTimer();
            }
            else UpdateTimer();
        }

        public void Select()
        {
            _selected = !_selected;
        }

        public void FoundAMatch()
        {
            gameObject.SetActive(false);
        }

        public void AssignChosenValues(Animal animalToCopy)
        {
            _foodConsume = animalToCopy.FoodConsume;
            _medicConsume = animalToCopy.MedicConsume;
            _type = animalToCopy.Type;
            _mat = animalToCopy.Mat;
            _render.material = _mat;
            ResetValues();
        }

        public void TakeIn()
        {
            _vaccinated = true;
            _taken = true;
        }

        private void Consume() => Resources.Resource.EatFood(_foodConsume);

        public void ResetValues()
        {
            _selected = false;
            _taken = false;
            _vaccinated = false;
            if (curbDisplay != null) curbDisplay.SetActive(true);
            //timer to consume food
            if (rangeToEat.Length > 1) timer = Random.Range(rangeToEat[0], rangeToEat[1] + 1);
            else timer = 20;
            originalTimer = timer;
            if (animalInfo != null)
            {
                animalInfo.InfoDisplay.SetActive(false);
                animalInfo.ResetInfoText();
            }
        }

        private bool TimeToConsume() => timer <= 0;

        private void UpdateTimer() => timer -= Time.deltaTime;

        private void ResetTimer() => timer = originalTimer;
    }
}
