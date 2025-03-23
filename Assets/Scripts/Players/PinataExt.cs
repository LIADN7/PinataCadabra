using System.Collections;
using UnityEngine;

/// <summary>
/// Extends the Pinata class to add more behaviors and functionality
/// </summary>
public class PinataExt : Pinata
{
    [SerializeField] private WeaponSpawner weaponSpawner; // Assign via Inspector
    [SerializeField] private float minShootDelay = 3f;
    [SerializeField] private float maxShootDelay = 7f;
    private Coroutine shootCoroutine;

    private void Start()
    {
        Rigidbody2D rb = transform.parent.GetComponent<Rigidbody2D>();
        rb.AddTorque(2f, ForceMode2D.Impulse);
        talkSound.StartLoopWithStartSound();
        this.StartShooting();
    }

    public override void Shoot(Vector2 direction)
    {

        if (GameManager.inst.IsState(GameManager.GameState.Play) && weaponSpawner != null)
        {
            weaponSpawner.SpawnAndFireWeapon(direction);
        }
    }

    // Stops the shooting coroutine before calling the base Die
    public override void Die()
    {
        StopShooting();
        base.Die();
    }

    // Starts the continuous shooting coroutine if it's not already running
    public void StartShooting()
    {
        if (shootCoroutine == null)
        {
            shootCoroutine = StartCoroutine(ShootLoop());
        }
    }

    // Stops the continuous shooting coroutine if it is running
    public void StopShooting()
    {
        if (shootCoroutine != null)
        {
            StopCoroutine(shootCoroutine);
            shootCoroutine = null;
        }
    }

    // Coroutine that repeatedly with a random delay to create Shoot
    private IEnumerator ShootLoop()
    {
        while (true)
        {
            float delay = Random.Range(minShootDelay, maxShootDelay);
            yield return new WaitForSeconds(delay);

            // Calculate a random downward direction with a variation of ±30°
            float randomAngle = Random.Range(-30f, 30f);
            Vector2 randomDownDirection = Quaternion.Euler(0, 0, randomAngle) * Vector2.down;

            // Fire the projectile using the overridden Shoot method.
            Shoot(randomDownDirection);
        }
    }
}
