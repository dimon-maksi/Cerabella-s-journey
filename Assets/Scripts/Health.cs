using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class Health : MonoBehaviour
    {
        private Animator anim;
        private Rigidbody2D rb;

        public int MaxHealth;
        public int CurrentHealth;

        private void Start()
        {
            rb= GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            CurrentHealth = MaxHealth;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Trap"))
            {
                TakeDamage(MaxHealth);
            }
        }

        public void TakeDamage(int Amount)
        {
            CurrentHealth -= Amount;
            if (CurrentHealth <= 0)
            {
                anim.SetTrigger("death");
                rb.bodyType = RigidbodyType2D.Static;
                //Invoke("TeleportPlayerToSpawnpoint", 2f);
            }
        }

        private void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        
        
        private void TeleportPlayerToSpawnpoint()
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
            {
                playerObject.transform.position = new Vector3(-0.81f, 0.19f, 0f);
        
                Health playerHealth = playerObject.GetComponent<Health>();
                if (playerHealth != null)
                {
                    playerHealth.anim.SetBool("IsDeath", false);
                    CurrentHealth = MaxHealth;
                }
            }
        }
    }
}