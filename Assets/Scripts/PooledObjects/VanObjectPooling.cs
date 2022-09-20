using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{

    public class VanObjectPooling : ObjectPooling
    {
        [SerializeField] private GameObject[] waypoints;
        [SerializeField] private Cages _dropPoints;

        public Cages DropPoints { get => _dropPoints; }
        public static VanObjectPooling SharedVanInstance { get; private set; }

        private void Awake()
        {
            SharedVanInstance = this;
        }

        protected override void Start()
        {
            base.Start();
            if (_dropPoints == null) Debug.LogWarning("You need to add the dropPoints into the vanObjectPooling!");
        }

        protected override GameObject InitializeObjects()
        {
            GameObject temp = base.InitializeObjects();
            temp.GetComponent<VanWaypointFollower>().waypoints = waypoints;
            return null;
        }
    }
}
