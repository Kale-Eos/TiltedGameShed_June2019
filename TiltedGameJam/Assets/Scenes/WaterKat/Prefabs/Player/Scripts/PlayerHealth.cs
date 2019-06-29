using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int health;
    [SerializeField]
    private int maxHealth;


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
    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
