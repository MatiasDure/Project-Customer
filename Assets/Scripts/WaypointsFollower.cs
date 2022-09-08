using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

[RequireComponent(typeof(Npc))]
public class WaypointsFollower : MonoBehaviour
{
    [SerializeField] private int restAtWaypoint;
    public GameObject[] waypoints;
    public Npc npc;
    private float speed;

    private int currentIndex;

    private bool stopCurrentIndex;
    private bool leaveBuilding;

    // Start is called before the first frame update
    void Start()
    {
        stopCurrentIndex = false;
        leaveBuilding = false;
        currentIndex = 0;
        speed = Random.Range(0.01f,0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Manager.IsPaused) return;
        MoveTowardsWayPoint();
    }
    
    private void MoveTowardsWayPoint()
    {
        if(npc.HandedPet || npc.ImOut) LeaveBuilding();

        //Stop in front of desk
        if (stopCurrentIndex) return;

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
        if(!leaveBuilding)stopCurrentIndex = currentIndex == restAtWaypoint;
        if (stopCurrentIndex && !leaveBuilding) return;
        currentIndex += 1;
        if(currentIndex == waypoints.Length) Destroy(gameObject);
    }

    public void LeaveBuilding()
    {
        leaveBuilding = true;
        stopCurrentIndex = false;
    }

}
