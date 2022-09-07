using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts
{
    public class NpcSpawner : Spawner
    {
        [SerializeField] private float cooldownSpawnTime;

        int startingCooldownTime;
        // Start is called before the first frame update
        void Start()
        {
            startingCooldownTime = (int)cooldownSpawnTime;
        }

        // Update is called once per frame
        void Update()
        {
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
            int randomIndex = Random.Range(0, spawnObject.Length);
            Spawn(spawnObject[randomIndex]);
        }
    }
}
