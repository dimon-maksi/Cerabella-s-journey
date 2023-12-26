using System;
using UnityEngine;

namespace DefaultNamespace.Enemy
{
    public class MeleeAttack : MonoBehaviour
    {
        [SerializeField] public float AttackCooldown;
        [SerializeField] public int Damage;
        [SerializeField] public float Range;
        [SerializeField] public int ColliderDistance;
        [SerializeField] private CapsuleCollider2D capsuleCollider;
        [SerializeField] private LayerMask playerLayer;

        private float cooldownTimer = Mathf.Infinity;
        private Animator anim;
        private Health playerHealth;

        private void Awake()
        {
            anim = GetComponent<Animator>();
        }

        private void Update()
        {
            cooldownTimer += Time.deltaTime;

            if (PlayerInSight())
            {
                if (cooldownTimer >= AttackCooldown)
                {
                    cooldownTimer = 0;
                    anim.SetTrigger("Attack1");
                }
            }
        }

        private bool PlayerInSight()
        {
            RaycastHit2D hit =
                Physics2D.BoxCast(
                    capsuleCollider.bounds.center + transform.right * Range * transform.localScale.x * ColliderDistance,
                    new Vector3(capsuleCollider.bounds.size.x * Range, capsuleCollider.bounds.size.y,
                        capsuleCollider.bounds.size.z),
                    0, Vector2.left, 0, playerLayer);
            if (hit.collider != null)
            {
                playerHealth = hit.transform.GetComponent<Health>();
            }

            return hit.collider != null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(
                capsuleCollider.bounds.center + transform.right * Range * transform.localScale.x * ColliderDistance,
                new Vector3(capsuleCollider.bounds.size.x * Range, capsuleCollider.bounds.size.y,
                    capsuleCollider.bounds.size.z));
        }

        private void DamagePlayer()
        {
            if (PlayerInSight())
            {
                // Отримати всі об'єкти, які перетинаються з областю атаки
                Collider2D[] colliders = Physics2D.OverlapBoxAll(
                    capsuleCollider.bounds.center + transform.right * Range * transform.localScale.x * ColliderDistance,
                    new Vector2(capsuleCollider.bounds.size.x * Range, capsuleCollider.bounds.size.y),
                    0, playerLayer);

                foreach (Collider2D collider in colliders)
                {
                    // Перевірити, чи знайдено гравця серед об'єктів
                    Health playerHealth = collider.GetComponent<Health>();
                    if (playerHealth != null)
                    {
                        // Завдати шкоди гравцеві за допомогою метода TakeDamage
                        playerHealth.TakeDamage(Damage);
                    }
                }
            }
        }
    }
}