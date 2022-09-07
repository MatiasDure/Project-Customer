using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Spawner : MonoBehaviour
    {
        [SerializeField] protected GameObject[] spawnObject;
        [SerializeField] protected Vector3Int spawnPosition;

        protected void Spawn(GameObject objectToSpawn)
        {
            if(spawnObject.Length == 0)
            {
                Debug.Log("Error: No spawn object added to the list!");
                return;
            }
            objectToSpawn.transform.position = spawnPosition;
            Instantiate(objectToSpawn);
        }
    }
}