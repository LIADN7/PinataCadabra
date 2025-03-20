using System.Collections;
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
        this.Lifetime = 5f;
        // this.FireRate = 2f; // 1 shot per second
    }

    public override void Fire(Vector2 direction)
    {
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
        if (!IsDead)
        {

            // Implement explosion, visual effects, or any other impact logic
            Debug.Log("WorriorSpell hit something! Triggering explosion or special effect...");
            IsDead = true;
            ExplosionEffect();
        }


    }
    private void ExplosionEffect()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        float duration = 0.5f;

        // Debug.Log("aaa" + transform.localScale);
        // Scale Tween (Once, then destroy)
        LeanTween.scale(gameObject, transform.localScale * 2f, duration)
            .setEase(LeanTweenType.easeOutQuad);

        if (sr != null)
        {
            // Fade Tween
            LeanTween.value(gameObject, sr.color.a, 0f, duration)
                .setOnUpdate((float value) =>
                {
                    Color newColor = sr.color;
                    newColor.a = value;
                    sr.color = newColor;
                })
                .setOnComplete(() => Destroy(gameObject));
        }
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pinata"))
        {
            Hit();
        }
    }

}