using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts
{
    public abstract class ObjectPooling : MonoBehaviour
    {
        [SerializeField] protected GameObject objectToPool;
        [SerializeField] protected int amountToPool;
        [SerializeField] protected Vector3 startingPosition;

        protected List<GameObject> pooledObjects;

        // Start is called before the first frame update
        protected void Start()
        {
            pooledObjects = new List<GameObject>();

            for (int i = 0; i < amountToPool; i++)
            {
                InitializeObjects();
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

        protected virtual GameObject InitializeObjects()
        {
            GameObject temp = Instantiate(objectToPool);
            temp.transform.position = startingPosition;
            temp.SetActive(false);
            pooledObjects.Add(temp);
            return temp;
        }
    }
}