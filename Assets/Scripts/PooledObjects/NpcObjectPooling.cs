using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcObjectPooling : ObjectPooling
{
    //[SerializeField] private GameObject[] waypoints;
    [SerializeField] private Cages waypointSystem;
    public static NpcObjectPooling SharedNpcInstance { get; private set; }

    private void Awake()
    {
        SharedNpcInstance = this;
    }

    protected override void Start()
    {
        base.Start();
        if (waypointSystem == null) Debug.LogWarning("Pass in the waypointSystem to the npcObjectPooling script!");
    }

    protected override GameObject InitializeObjects()
    {
        GameObject temp = base.InitializeObjects();
        //temp.GetComponent<NpcWaypointFollower>().waypoints = waypoints;
        temp.GetComponent<NpcWaypointFollower>().waypointArray = waypointSystem;
        return null;
    }
}
