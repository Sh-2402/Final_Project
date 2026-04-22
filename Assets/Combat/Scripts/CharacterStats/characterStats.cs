using UnityEngine;

public class characterStats : MonoBehaviour
{
    [SerializeField] private Healthbar _healthbar;

    public int maxHealth = 100;
    public int currentHealth {  get; private set; }
    public stat damage;

    private void Awake()
    {
        currentHealth = maxHealth;

        _healthbar.updateHealthBar(maxHealth, currentHealth);
    }

    private void Update()
    {
       
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage. Current health = " + currentHealth);

        if (currentHealth <= 0) {
            Die();
        }
        else
        {
            _healthbar.updateHealthBar(maxHealth, currentHealth);
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
        PlayerManager.instance.KillPlayer();
    }
}
