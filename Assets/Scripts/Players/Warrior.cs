
using UnityEngine;
/// <summary>
/// Manages the warrior's movement, shooting, and game interactions. Uses joystick input to update movement and fire spells
/// </summary>
public class Warrior : Player
{
    public MovementJoystick movementJoystick;
    [Header("Firing Settings")]
    public WeaponSpawner spawner; // Assign WeaponSpawner

    public float playerSpeed;
    public float yOffSet = 0.3f; // Maximum offset allowed from the initial Y position
    private float smoothing = 5f; // Smoothing factor for stop movement

    private float initY;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initY = transform.position.y; // Initial Y position
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
        }
    }

    // Joysticl movement
    public override void Move()
    {
        Vector2 targetVelocity = Vector2.zero;
        if (movementJoystick.joystickVec.y != 0)
        {
            targetVelocity = new Vector2(movementJoystick.joystickVec.x * playerSpeed,
                                         movementJoystick.joystickVec.y * playerSpeed);
        }
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, targetVelocity, smoothing * Time.fixedDeltaTime);
        float clampedY = Mathf.Clamp(transform.position.y, initY - yOffSet, initY + yOffSet);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
    }

    public override void Die()
    {

    }

    public override void Win()
    {

    }

    public override void Hit()
    {

    }
}
