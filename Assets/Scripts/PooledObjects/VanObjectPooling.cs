using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{

    public class VanObjectPooling : ObjectPooling
    {
        [SerializeField] private GameObject[] waypoints;
        public static VanObjectPooling SharedVanInstance { get; private set; }

        private void Awake()
        {
            SharedVanInstance = this;
        }

        protected override GameObject InitializeObjects()
        {
            GameObject temp = base.InitializeObjects();
            temp.GetComponent<VanWaypointFollower>().waypoints = waypoints;
            return null;
        }
    }
}
