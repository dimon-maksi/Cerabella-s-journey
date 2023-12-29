using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Enemy;
using UnityEngine;

public class PatrolController : MonoBehaviour
{
    private Patrol _patrol;
    void Awake()
    {
        _patrol = GetComponentInParent<Patrol>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_patrol != null)
        _patrol.enabled = !GetComponent<MeleeAttack>().PlayerInSight();
    }
}
