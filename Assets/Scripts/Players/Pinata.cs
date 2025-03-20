using UnityEngine;

/// <summary>
/// Manages the pinata's behavior, including hit detection, damage effects, and dropping head parts
/// </summary>
public class Pinata : Player
{

    // The hit modulo value - drop a head every 'hitModulo' hits.
    [SerializeField] private int hitModulo = 3;
    [SerializeField] public GameObject[] HeadsObject;
    private int HitCount = 0; // Counter for the number of hits

    public PointSpawner PointSpawner;


    private void Start()
    {
        Rigidbody2D rb = transform.parent.GetComponent<Rigidbody2D>();
        // For a small push on start
        rb.AddTorque(2f, ForceMode2D.Impulse);
    }

    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    public override void Move()
    {
        throw new System.NotImplementedException();
    }

    public override void Shoot(Vector2 direction)
    {
        throw new System.NotImplementedException();
    }

    public override void Win()
    {
        throw new System.NotImplementedException();
    }


    public override void Hit()
    {
        // Create point item and spawn
        int newPoint = PointSpawner.SpawnPoint();
        ScoreManager.Inst.AddScore(newPoint);
        DamageEffect();

        // Every "hitModulo" hits, trigger DropHeadEffect
        if (HitCount % hitModulo == 0)
        {
            DropHeadEffect();
        }
        HitCount++; // Increment hit counter on each hit
    }


    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WorriorSpell"))
        {
            // To count 1 hit
            other.tag = "Finish";
            Hit();
        }
    }

    // Flashing character on hit
    private void DamageEffect()
    {

        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sr in sprites)
        {
            LeanTween.cancel(sr.gameObject);
            // Reset alpha to 1
            Color col = sr.color;
            col.a = 1f;
            sr.color = col;
            LeanTween.alpha(sr.gameObject, 0.5f, 0.5f)
                .setLoopPingPong(2)
                .setEase(LeanTweenType.easeInOutSine);
        }

    }

    // Head fall effect
    private void DropHeadEffect()
    {
        int headNum = (HitCount / hitModulo);
        if (headNum < HeadsObject.Length)
        {
            GameObject head = HeadsObject[headNum];
            head.transform.SetParent(null);

            // Animate the head falling to the bottom of the screen
            float offScreenY = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y - 2f;
            LeanTween.moveY(head, offScreenY, 1f)
                     .setEase(LeanTweenType.easeInQuad);
            LeanTween.rotateZ(head, head.transform.eulerAngles.z + 90f, 1f)
                     .setEase(LeanTweenType.easeInQuad);
        }


    }


}
