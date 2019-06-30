using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace WaterKat
{
    public class PlayerHealth : MonoBehaviour, IEntity
    {
        [SerializeField]
        private int health;
        [SerializeField]
        private int maxHealth;

        public int Health { get { return health; } set { health = value; } }

        public void Damage(int amount)
        {
            if (amount == 0) { amount = 10; }
            health -= amount;
            if (amount <= 0)
            {
                Die();
            }
        }

        public void Heal(int amount)
        {
            health += amount;
            health = (int)Mathf.Min(health, maxHealth);
        }

        [ContextMenu("DealDamage")]
        public void Die()
        {
            WKAudio.PlayAudio("Death");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void Update()
        {
            if (Input.GetKey("i"))
            {
                Die();
            }
        }
    }
}