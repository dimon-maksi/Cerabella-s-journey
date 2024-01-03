using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] public Transform enemy;
    [SerializeField] public Transform target;
    [SerializeField] public float speed = 200f;
    [SerializeField] public float nextWaypointDistance = 3f;
    [SerializeField] public float triggerRadius = 5f;
    [SerializeField] public LayerMask playerLayer;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;
    private bool playerIsFinded = false;
    

    private Seeker seeker;
    private Rigidbody2D rb;
    
    
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    private void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }
    
    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void Update()
    {
        if (!playerIsFinded && Physics2D.OverlapCircle(transform.position, triggerRadius, playerLayer))
        {
            Debug.Log("Player entered the trigger zone!");
            playerIsFinded = true;
        }
    }

    void FixedUpdate()
    {
        if (playerIsFinded)
        {
            if (path == null)
                return;

            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;
        
            rb.AddForce(force);

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }

            if (force.x >= 0.01f)
            {
                enemy.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (force.x <= -0.01f)
            {
                enemy.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, triggerRadius);
    }
}
