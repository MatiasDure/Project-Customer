using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class NpcWaypointFollower : WaypointsFollower
    {
        public Npc npc;

        protected override void Update()
        {
            base.Update();
            if (reachedTheEnd) LeaveShelter();
        }
        protected override void MoveTowardsWayPoint()
        {
            if (npc.HandedPet || npc.ImOut) LeaveBuilding();
            base.MoveTowardsWayPoint();
        }

        private void LeaveShelter()
        {
            //Reset and deactivate npc waypoint  
            if (npc.HandedPet)
            {
                int randomDonation = UnityEngine.Random.Range(30,61);
                Resources.Resource.AddMoney(randomDonation);
            }
            reachedTheEnd = false;
            leaveBuilding = false;
            
            //Picking a new Preference pet
            npc.ResetNpc();
            
            //Deactivating npc pooled
            gameObject.SetActive(false);


        }
    }
}
