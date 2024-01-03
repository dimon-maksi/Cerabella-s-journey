using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] public float AttackCooldown;
    [SerializeField] public int Damage;
    [SerializeField] public float Range;
    [SerializeField] public LayerMask EnemyLayer;
    [SerializeField] public Animator anim;
    [SerializeField] private CapsuleCollider2D capsuleCollider;
    [SerializeField] public float ColliderDistance;
    private float cooldownTimer = Mathf.Infinity;
    private SpriteRenderer sprite;
    
    
    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;
        
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (cooldownTimer >= AttackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("attack");
            }
        }
    }

    private void DamageEnemy()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(
            capsuleCollider.bounds.center + transform.right * Range * transform.localScale.x * ColliderDistance * (sprite.flipX? -1 : 1), 
            new Vector2(capsuleCollider.bounds.size.x * Range, capsuleCollider.bounds.size.y),
            0,EnemyLayer);
        foreach (Collider2D collider in colliders)
        {
            Health enemyHealth = collider.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(Damage);
            }
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(capsuleCollider.bounds.center + transform.right * Range * transform.localScale.x * ColliderDistance * (sprite.flipX? -1 : 1),
            new Vector3(capsuleCollider.bounds.size.x * Range, capsuleCollider.bounds.size.y, capsuleCollider.bounds.size.z));
    }
}
