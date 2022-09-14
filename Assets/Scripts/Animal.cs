using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Renderer))] 
    public class Animal : MonoBehaviour
    {
        [SerializeField] private int foodConsume;
        [SerializeField] private int medicConsume;
        [SerializeField] private AnimalType _type;
        [SerializeField] private Material _mat;
        [SerializeField] private Material[] materials;
        [SerializeField] private Renderer render;
        [SerializeField] private Vector3 _offsetStartPosition;

        private bool _selected;
        public bool Selected { get => _selected; }
        public Material Mat { get => _mat; }

        public AnimalType Type { get => _type; }

        public Vector3 OffsetStartPosition { get => _offsetStartPosition; }

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

        private void MatchWithMaterial()
        {
            switch (_type)
            {
                case AnimalType.Cat:
                    _mat = materials[0];
                    break;
                case AnimalType.Dog:
                    _mat = materials[1];
                    break;
                default:
                    _mat = materials[1];
                    break;
            }
            render.material = _mat;
        }

        public void ChooseAnimalType()
        {
            int ran = Random.Range(0, 2);
            _type = ran == 0 ? AnimalType.Cat : AnimalType.Dog;
            MatchWithMaterial();
        }
    }
}
