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
                Invoke("TeleportPlayerToSpawnpoint", 2f);
            }
        }
        
        private void TeleportPlayerToSpawnpoint()
        {
            // Змініть цей рядок, щоб отримати посилання на гравця або інший об'єкт, який атакується
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            // Перевірте, чи знайдено об'єкт
            if (playerObject != null)
            {
                // Телепортуємо об'єкт, який атакується, на нову позицію
                playerObject.transform.position = new Vector3(-0.81f, 0.19f, 0f);
        
                // Скидуємо статус смерті для об'єкта, який атакується
                Health playerHealth = playerObject.GetComponent<Health>();
                if (playerHealth != null)
                {
                    playerHealth.anim.SetBool("IsDeath", false);
                }
            }
        }
    }
}