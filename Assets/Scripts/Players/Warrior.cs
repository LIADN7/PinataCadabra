
using UnityEngine;
/// <summary>
/// Manages the warrior's movement, shooting, and game interactions. Uses joystick input to update movement and fire spells
/// </summary>
public class Warrior : Player
{
    public MovementJoystick movementJoystick;
    [Header("Firing Settings")]
    public WeaponSpawner spawner; // Assign WeaponSpawner
    [Header("Warrior Sound Manager")]
    public WarriorSoundManager sounds;

    public float playerSpeed;
    private float smoothing = 5f; // Smoothing factor for stop movement

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        this.Life = 3;
        LifeManager.Inst?.UpdateLife(this.Life);
    }

    void FixedUpdate()
    {
        // Move when the game is on "play" and the joystick is on Drag
        if (GameManager.inst.IsState(GameManager.GameState.Play))
        {
            this.Move();
        }
    }

    // Use the spawner to shoot the weapon in the given direction.
    public override void Shoot(Vector2 direction)
    {
        if (GameManager.inst.IsState(GameManager.GameState.Play))
        {
            spawner.SpawnAndFireWeapon(direction);
            sounds.PlaySpellAndCastEffect();
        }
    }

    // Joystick movement
    public override void Move()
    {
        Vector2 targetVelocity = new Vector2(
            movementJoystick.joystickVec.x * playerSpeed,
            movementJoystick.joystickVec.y * playerSpeed);

        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, targetVelocity, smoothing * Time.fixedDeltaTime);
    }

    public override void Die()
    {
        ScoreManager.Inst.StopTimer();
        ScoreConfig.inst.SetFinalGameTime(ScoreManager.Inst.GetLastTimer());
        GameManager.inst.ChangeState(GameManager.GameState.Loss);
        Destroy(gameObject);
    }


    public override void Hit()
    {
        //Update life text
        LifeManager.Inst?.UpdateLife(this.Life);
        DamageEffect();
    }


    // Hit visual effect
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
}
