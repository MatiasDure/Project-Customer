using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class TimeBasedSpawner : MonoBehaviour
    {
        [SerializeField] private float[] rangeOfSpawnTime;
        [SerializeField] private NpcType typeToActivate;
        [SerializeField] private Vector3 startingPosition;

        enum NpcType
        {
            human, 
            van
        };

        private float cooldownSpawnTime; 

        float startingCooldownTime; 

        void Start()
        {
            //we set a random starting cooldown time at the start given two input number to declare the range
            startingCooldownTime = cooldownSpawnTime = Random.Range(rangeOfSpawnTime[0], rangeOfSpawnTime[1] + 1);
        }

        // Update is called once per frame
        void Update()
        {
            //if (!canSpawn) canSpawn = IsSpawnable();
            if (GameManager.Manager.IsPaused) return;
            DecreaseTimer();
            if (CheckCoolDown())
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
            GameObject obj;
            switch(typeToActivate)
            {
                case NpcType.van:
                    obj = VanObjectPooling.SharedVanInstance.GetPooledObject();
                    break;
                case NpcType.human:
                    obj = VanObjectPooling.SharedVanInstance.GetPooledObject();
                    break;
                default:
                    obj = VanObjectPooling.SharedVanInstance.GetPooledObject();
                    break;
            }

            if (obj == null) return;
            obj.transform.position = startingPosition;
            obj.SetActive(true);
        }
    }
}

