using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform platform;
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 0.05f;

    int direction = 1;
    private void Update()
    {
        Vector2 target = currentMovementTarget();
        platform.position = Vector2.Lerp(platform.position, target, speed * Time.deltaTime);
        float distance = (target - (Vector2)platform.position).magnitude;
        if (distance <= 0.1f) 
        {
            direction *= -1;
        }
    }
    Vector2 currentMovementTarget()
    {
        if (direction==1)
        {
            return startPoint.position;
        }
        else
        {
            return endPoint.position;
        }
    }
    private void OnDrawGozmos()
    {
        if(platform!=null && startPoint!=null && endPoint!=null)
        {
            Gizmos.DrawLine(platform.transform.position, startPoint.position);
            Gizmos.DrawLine(platform.transform.position, endPoint.position);
        }
    }


    //[SerializeField] private GameObject[] waypoints;
    //private int currentWaypointIndex = 0;
    //[SerializeField] private float speed = 2f;
    //private void Update()
    //{
    //    if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
    //    {
    //        currentWaypointIndex++;
    //        if (currentWaypointIndex >= waypoints.Length)
    //        { currentWaypointIndex = 0; }
    //    }
    //    transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
    //}
}
