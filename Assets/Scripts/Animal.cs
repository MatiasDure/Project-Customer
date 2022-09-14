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
        [SerializeField] private Material _mat; //might need to delete this
        [SerializeField] private Vector3 _offsetStartPosition;
        [SerializeField] private Renderer _render;
        public AnimalType _type;

        public bool _selected;
        public bool Selected { get => _selected; }
        public Material Mat { get => _mat; }

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
        }

        // Start is called before the first frame update
        void Start()
        {
            _selected = false; 
        }

        public void Select()
        {
            _selected = !_selected;
        }

        public void FoundAMatch()
        {
            _selected = false;
            gameObject.SetActive(false);
        }

        public void AssignChosenValues(Animal animalToCopy)
        {
            _foodConsume = animalToCopy.FoodConsume;
            _medicConsume = animalToCopy.MedicConsume;
            _type = animalToCopy.Type;
            _mat = animalToCopy.Mat;
            _render.material = _mat;
        }
    }
}
