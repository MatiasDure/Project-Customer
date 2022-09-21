using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Npc))]
    public class NpcWaypointFollower : WaypointsFollower
    {
        public Npc npc;
        public Cages waypointArray;
        public Cage currentCheckPoint;

        protected override void Update()
        {
            base.Update();
            if (reachedTheEnd) LeaveShelter();
        }
        protected override void MoveTowardsWayPoint()
        {
            if (npc.HandedPet || npc.ImOut) LeaveBuilding();
            //base.MoveTowardsWayPoint();

            //We move towards the new checkpoint if it is not occupied or if we are leaving the building
            if(waypointArray.IsCageAvailable(currentIndex) || currentCheckPoint == waypointArray.CagesInShelter[currentIndex] || leaveBuilding)
            {
                //We ignore occupying the checkpoints if we are leaving the building
                if(!leaveBuilding)
                {
                    currentCheckPoint = waypointArray.CagesInShelter[currentIndex];
                    currentCheckPoint.AddAnimal(gameObject);
                }

                target = waypointArray.CagesInShelter[currentIndex].gameObject;

                //calculating direction towards target
                Vector3 distance = target.transform.position - gameObject.transform.position;
                
                //looking at target
                gameObject.transform.LookAt(target.transform);

                //Check whether we reached the checkpoint
                if (ReachedWaypoint(distance.magnitude))
                {
                    //Check if this is the last checkpoint
                    if (currentIndex + 1 == waypointArray.CagesInShelter.Length)
                    {
                        ResetWaypoint();
                        return;
                    }

                    //if we are not leaving the building
                    //and no animal has been handed to the npc
                    //and they are in front of the desk
                    //or if the cage in front is occupied we return
                    if (!leaveBuilding && 
                        (!npc.HandedPet && currentIndex == restAtWaypoint || 
                        !waypointArray.IsCageAvailable(currentIndex + 1))) return;

                    //checking whether the cage is not null to avoid null error
                    if(currentCheckPoint != null)
                    {
                        currentCheckPoint.RemoveAnimal();
                        currentCheckPoint = null;
                    }

                    //The next checkpoint becomes the new target
                    UpdateWaypoint();
                }
                else
                {
                    Vector3 velocity = distance.normalized * speed * Time.deltaTime;
                    gameObject.transform.position += velocity;
                }
            }
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
