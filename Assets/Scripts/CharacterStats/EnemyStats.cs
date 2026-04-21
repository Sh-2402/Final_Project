using UnityEngine;

public class EnemyStats : characterStats
{
    public override void Die()
    {
        base.Die();

        // add ragdoll effect/ death animation

        Destroy(gameObject);
    }
}
