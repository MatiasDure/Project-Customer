using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcWaypointFollower : WaypointsFollower
{
    public Npc npc;

    protected override void MoveTowardsWayPoint()
    {
        if (npc.HandedPet || npc.ImOut) LeaveBuilding();
        base.MoveTowardsWayPoint();
    }
}
