using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform boundaryToSpawn;
    [SerializeField] private GameObject[] spawnObject;

    private void Awake()
    {
        if (boundaryToSpawn == null) boundaryToSpawn = GameObject.Find("Floor").transform;
        if (spawnObject.Length == 0) spawnObject = new GameObject[] { GameObject.CreatePrimitive(PrimitiveType.Cube) };
    }

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Spawn()
    {
        int randomIndex = Random.Range(0, spawnObject.Length);
        spawnObject[randomIndex].transform.position = new Vector3(Random.Range(-boundaryToSpawn.transform.localScale.x / 2, boundaryToSpawn.transform.localScale.x / 2),
                                               1,
                                               Random.Range(-boundaryToSpawn.transform.localScale.z / 2, boundaryToSpawn.transform.localScale.z / 2));
    }
}
