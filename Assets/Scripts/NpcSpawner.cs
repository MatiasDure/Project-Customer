using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts
{
    public class NpcSpawner : Spawner
    {
        [SerializeField] private float cooldownSpawnTime; //shared with animal van spawner
        [SerializeField] private GameObject[] waypoints; //shared with animal van spawner
        [SerializeField] private int amountToSpawn;

        int startingCooldownTime; //shared with animal van spawner
        int amountSpawned;
        // Start is called before the first frame update
        void Start()
        {
            startingCooldownTime = (int)cooldownSpawnTime;
            amountSpawned = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if(!canSpawn) canSpawn = IsSpawnable();
            if (amountToSpawn == amountSpawned || GameManager.Manager.IsPaused) return;
            DecreaseTimer();
            if(CheckCoolDown())
            {
                ResetTimer();
                SpawnRandomNpc();
            }
        }

        //shared with van spawner
        void DecreaseTimer() => cooldownSpawnTime -= Time.deltaTime;

        //shared with van spawner
        void ResetTimer() => cooldownSpawnTime = startingCooldownTime;

        //shared with van spawner
        bool CheckCoolDown() => cooldownSpawnTime <= 0;
        void SpawnRandomNpc()
        {
            if (!canSpawn) return;
            int randomIndex = Random.Range(0, spawnObject.Length);

            //We are getting the WaypointsFollower script from the npc we want to spawn 
            WaypointsFollower npcWaypointScript = spawnObject[randomIndex].GetComponent<WaypointsFollower>();
            //npcWaypointScript.npc = spawnObject[randomIndex].GetComponent<Npc>();

            //Passing the waypoints to npc we want to spawn
            npcWaypointScript.waypoints = this.waypoints;

            //spawning npc
            Spawn(spawnObject[randomIndex]);
            amountSpawned++;
        }
    }
}
