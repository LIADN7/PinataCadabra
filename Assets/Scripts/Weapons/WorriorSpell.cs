using UnityEngine;

/// <summary>
/// Represents the warrior's weapon.
/// </summary>
public class WorriorSpell : Weapon
{
    [Header("Movement Settings")]
    public float projectileSpeed = 5f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        this.Damage = 10;
        this.Lifetime = 3f;
    }

    public override void Fire(Vector2 direction)
    {
        rb.linearVelocity = direction.normalized * projectileSpeed;
        Destroy(gameObject, Lifetime);
    }

    public override void Hit()
    {
        if (!IsDead)
        {
            IsDead = true;
            ExplosionSpellEffect();
        }


    }

    // Scale and opacity tween for "Explosion Effect" of spell
    private void ExplosionSpellEffect()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        float duration = 0.5f;

        LeanTween.scale(gameObject, transform.localScale * 2f, duration)
            .setEase(LeanTweenType.easeOutQuad);
        LeanTween.value(gameObject, sr.color.a, 0f, duration)
                .setOnUpdate((float value) =>
                {
                    Color newColor = sr.color;
                    newColor.a = value;
                    sr.color = newColor;
                })
                .setOnComplete(() => Destroy(gameObject));
    }


    protected void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Pinata"))
        {
            Hit();
        }
    }

}