using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts
{
    public class NpcSpawner : Spawner
    {
        [SerializeField] private float cooldownSpawnTime;
        [SerializeField] private GameObject[] waypoints;
        [SerializeField] private int amountToSpawn;

        int startingCooldownTime;
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

        void DecreaseTimer() => cooldownSpawnTime -= Time.deltaTime;
        void ResetTimer() => cooldownSpawnTime = startingCooldownTime;
        bool CheckCoolDown() => cooldownSpawnTime <= 0;
        void SpawnRandomNpc()
        {
            if (!canSpawn) return;
            int randomIndex = Random.Range(0, spawnObject.Length);

            //We are getting the WaypointsFollower script from the npc we want to spawn 
            WaypointsFollower npcWaypointScript = spawnObject[randomIndex].GetComponent<WaypointsFollower>();
            npcWaypointScript.npc = spawnObject[randomIndex].GetComponent<Npc>();

            //Passing the waypoints to npc we want to spawn
            npcWaypointScript.waypoints = this.waypoints;

            //spawning npc
            Spawn(spawnObject[randomIndex]);
            amountSpawned++;
        }
    }
}
