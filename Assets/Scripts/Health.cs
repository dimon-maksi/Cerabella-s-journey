using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Health : MonoBehaviour
    {
        private Animator anim;
        
        public int MaxHealth;
        public int CurrentHealth;

        private void Start()
        {
            anim = GetComponent<Animator>();
            CurrentHealth = MaxHealth;
        }

        public void TakeDamage(int Amount)
        {
            CurrentHealth -= Amount;
            if (CurrentHealth <= 0)
            {
                anim.SetBool("IsDeath", true);
                TeleportPlayerToSpawnpoint();
            }
        }
        
        void TeleportPlayerToSpawnpoint()
        {
            Vector3 newPosition = new Vector3(-0.81f, 0.19f, 0f);
            transform.position = newPosition;
            anim.SetBool("IsDeath", false);
        }
    }
}