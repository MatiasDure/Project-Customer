using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts
{
    public class ObjectPooling : MonoBehaviour
    {
        [SerializeField] private GameObject objectToPool;
        [SerializeField] private int amountToPool;
        [SerializeField] private Canvas canvas;


        private List<GameObject> pooledObjects;
        public static ObjectPooling SharedInstance { get; private set; }

        private void Awake()
        {
            SharedInstance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            pooledObjects = new List<GameObject>();
            GameObject temp;

            for (int i = 0; i < amountToPool; i++)
            {
                temp = Instantiate(objectToPool);
                temp.transform.SetParent(canvas.transform,false);
                temp.SetActive(false);
                pooledObjects.Add(temp);
            }
        }

        public GameObject GetPooledObject()
        {
            for (int i = 0; i < amountToPool; i++)
            {
                if (!pooledObjects[i].activeInHierarchy) return pooledObjects[i];
            }
            return null;
        }
    }
}