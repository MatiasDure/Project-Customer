using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Animal : MonoBehaviour
    {
        [SerializeField] private int foodConsume;
        [SerializeField] private int medicConsume;
        [SerializeField] private AnimalType _type;
        [SerializeField] private Material _mat;

        private bool _selected;
        public bool Selected { get => _selected; }
        public Material Mat { get => _mat; }

        public AnimalType Type { get => _type; }

        public enum AnimalType
        {
            Cat,
            Dog
        }

        // Start is called before the first frame update
        void Start()
        {
            _selected = false;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Select()
        {
            _selected = !_selected;
        }

        public void FoundAMatch() => Destroy(gameObject);
    }
}
