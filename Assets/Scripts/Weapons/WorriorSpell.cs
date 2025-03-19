using UnityEngine;

public class WorriorSpell : Weapon
{
    [Header("Movement Settings")]
    public float projectileSpeed = 5f;
    // public float FireRate = 3f;

    // public float ;  // The spell will be destroyed after 5 seconds

    // // Internal cooldown tracking
    // private float lastFireTime = 0f;
    // Cached reference to this object's Rigidbody2D
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // Example stats
        this.Damage = 10;
        this.Lifetime = 3f;
        // this.FireRate = 2f; // 1 shot per second
    }

    public override void Fire(Vector2 direction)
    {
        // Check if the weapon is on cooldown
        // if (Time.time - lastFireTime < 1f / FireRate)
        //     return;

        // lastFireTime = Time.time;

        // Instead of instantiating a new projectile, use this object's Rigidbody2D to move it
        if (rb != null)
        {
            rb.linearVelocity = direction.normalized * projectileSpeed;
        }

        // Schedule the spell for destruction after its lifetime expires
        Destroy(gameObject, Lifetime);
    }

    public override void Hit()
    {
        // Implement explosion, visual effects, or any other impact logic
        Debug.Log("WorriorSpell hit something! Triggering explosion or special effect...");
        // For example, you might instantiate an explosion effect:
        // Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // Optionally, destroy the spell immediately on hit
        Destroy(gameObject);
    }
}