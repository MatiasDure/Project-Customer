using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

[RequireComponent(typeof(Npc))]
public class WaypointsFollower : MonoBehaviour
{
    [SerializeField] protected bool waypointRest;
    [SerializeField] protected int restAtWaypoint;
    public GameObject[] waypoints;
    //public Npc npc;
    protected float speed;

    protected int currentIndex;

    private bool stopCurrentIndex;
    protected bool leaveBuilding;
    protected bool reachedTheEnd;

    // Start is called before the first frame update
    protected virtual void Start()
    {    
        reachedTheEnd = false;
        stopCurrentIndex = false;
        leaveBuilding = false;
        currentIndex = 0;
        speed = Random.Range(0.008f,0.01f);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (GameManager.Manager.IsPaused) return;
        MoveTowardsWayPoint();
    }
    
    protected virtual void MoveTowardsWayPoint()
    {
        if (stopCurrentIndex || reachedTheEnd) return;

        //continue moving to next waypoint
        Vector3 distance = waypoints[currentIndex].transform.position - gameObject.transform.position;
        if (ReachedWaypoint(distance.magnitude)) UpdateWaypoint();
        else
        {
            Vector3 velocity = distance.normalized * speed;
            gameObject.transform.position += velocity;
        }
    }

    private bool ReachedWaypoint(float length) => length < 0.5f;

    private void UpdateWaypoint()
    {
        //rest if object is set to rest and given a specific waypoint to rest at
        if(waypointRest) stopCurrentIndex = currentIndex == restAtWaypoint;
        if (stopCurrentIndex && !leaveBuilding) return;
        currentIndex += 1;
        if (currentIndex == waypoints.Length) ResetWaypoint();
    }

    public void LeaveBuilding()
    {
        leaveBuilding = true;
        stopCurrentIndex = false;
    }

    private void ResetWaypoint()
    {
        reachedTheEnd = true;
        currentIndex = 0;
    }

}
