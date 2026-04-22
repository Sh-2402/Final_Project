using UnityEngine;

public class characterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth {  get; private set; }
    public stat damage;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(2);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage. Current health = " + currentHealth);

        if (currentHealth <= 0) {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
        PlayerManager.instance.KillPlayer();
    }
}
