using UnityEngine;

/// <summary>
/// Manages the pinata's behavior, including hit detection, damage effects, and dropping head parts
/// </summary>
public class Pinata : Player
{

    // The hit modulo value - drop a head every 'hitModulo' hits.
    [SerializeField] private int hitModulo = 3;
    [SerializeField] public GameObject[] headsObject;
    [SerializeField] public GameObject explosionGO;

    [SerializeField] public TrashTalkSoundManager talkSound;
    private int hitCount = 0; // Counter for the number of hits

    public PointSpawner PointSpawner;


    private void Awake()
    {

        this.Life = hitModulo * headsObject.Length;
    }

    private void Start()
    {
        Rigidbody2D rb = transform.parent.GetComponent<Rigidbody2D>();
        // For a small push on start
        rb.AddTorque(2f, ForceMode2D.Impulse);
        talkSound.StartLoopWithStartSound();
    }

    public override void Die()
    {
        talkSound.StopLoop();
        explosionGO.GetComponent<ParticleSystem>().Play();
        explosionGO.GetComponent<AudioSource>()?.Play();
        for (int i = 0; i < 20; i++)
        {
            PointSpawner spawner = FindObjectOfType<PointSpawner>();
            if (spawner != null)
            {
                spawner.SpawnPoint();
            }
        }
        ScoreManager.Inst.StopTimer();
        ScoreConfig.inst.SetFinalGameTime(ScoreManager.Inst.GetLastTimer());
        GameManager.inst.ChangeState(GameManager.GameState.Win);
        Destroy(gameObject);
    }

    public override void Move()
    {
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
        hitCount++; // Increment hit counter on each hit
        // this.UpdateLife(-1);
        // Create point item and spawn
        int newPoint = PointSpawner.SpawnPoint();

        ScoreConfig.inst.AddScore(newPoint);
        ScoreManager.Inst?.UpdateScoreTextUI(ScoreConfig.inst.score);

        DamageEffect();
        // Every "hitModulo" hits, trigger DropHeadEffect
        if (hitCount % hitModulo == 0)
        {
            DropHeadEffect();
        }

    }


    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WorriorSpell"))
        {
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
        int headNum = (hitCount / hitModulo) - 1;
        if (headNum < headsObject.Length && headNum >= 0)
        {
            GameObject head = headsObject[headNum];
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
