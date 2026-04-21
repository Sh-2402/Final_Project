using System.Collections;
using UnityEngine;

[RequireComponent (typeof(characterStats))]
public class CharacterCombat : MonoBehaviour
{
    public float attackSpeed = 1f;
    private float attackCooldown = 0f;

    public float attackDelay = .6f;

    public event System.Action OnAttack;

    characterStats myStats;

    void Start()
    {
        myStats = GetComponent<characterStats>();
    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime;

        if (attackCooldown < 0f) 
        {
            attackCooldown = 0f;
        }
    }
    public void Attack(characterStats targetStats)
    {
   
        if (attackCooldown <= 0f)
        {
            StartCoroutine(DoDamage(targetStats, attackDelay));

            if (OnAttack != null)
                OnAttack();

            int damageValue = myStats.damage.GetValue();


            targetStats.TakeDamage(myStats.damage.GetValue());
            attackCooldown = 1f / attackSpeed;
        }
    }

    IEnumerator DoDamage (characterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);

        stats.TakeDamage(myStats.damage.GetValue());   
    }
}