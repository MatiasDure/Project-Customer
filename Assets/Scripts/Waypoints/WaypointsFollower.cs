using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class WaypointsFollower : MonoBehaviour
{
    [SerializeField] protected bool waypointRest;
    [SerializeField] protected int restAtWaypoint;
    [SerializeField] protected float[] rangeOfSpeed;

    public GameObject[] waypoints;
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
        if(rangeOfSpeed.Length < 2) speed = Random.Range(8f,1f);
        else speed = Random.Range(rangeOfSpeed[0], rangeOfSpeed[1]);
        //Debug.Log(gameObject.name + ": "+speed);
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
            Vector3 velocity = distance.normalized * speed * Time.deltaTime;
            gameObject.transform.position += velocity;
        }
    }

    protected bool ReachedWaypoint(float length) => length < 0.5f;

    protected void UpdateWaypoint()
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

    protected void ResetWaypoint()
    {
        reachedTheEnd = true;
        currentIndex = 0;
    }

}
