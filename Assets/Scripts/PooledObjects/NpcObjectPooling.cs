using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcObjectPooling : ObjectPooling
{
    [SerializeField] private GameObject[] waypoints;
    public static NpcObjectPooling SharedNpcInstance { get; private set; }

    private void Awake()
    {
        SharedNpcInstance = this;
    }

    protected override GameObject InitializeObjects()
    {
        GameObject temp = base.InitializeObjects();
        temp.GetComponent<NpcWaypointFollower>().waypoints = waypoints;
        return null;
    }
}
