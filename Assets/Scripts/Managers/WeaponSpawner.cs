using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    [Header("Weapon Prefab to Spawn")]
    public Weapon weaponPrefab;   // Assign your WorriorSpell prefab in the Inspector

    [Header("Spawn Settings")]
    public Transform spawnPoint;        // Where the weapon will be spawned (e.g., the Warrior's position)

    public float FireRate = 2f;

    // Internal cooldown tracking
    private float lastFireTime = 0f;


    /// <summary>
    /// Spawns the weapon prefab and calls Fire() in the specified direction.
    /// </summary>
    /// <param name="direction">Direction to fire the weapon.</param>
    public void SpawnAndFireWeapon(Vector2 direction)
    {
        if (weaponPrefab == null)
        {
            Debug.LogWarning("Spawner: weaponPrefab is not assigned.");
            return;
        }

        // // Cooldown check
        // Instantiate the weapon prefab at the chosen spawn point
        if (Time.time - lastFireTime < 1f / FireRate)
        {
            Debug.Log("WeaponSpawner: Cooldown active.");
            return;
        }
        lastFireTime = Time.time;
        Weapon newWeapon = Instantiate(weaponPrefab, spawnPoint.position, Quaternion.identity);

        // Call Fire() with the provided direction (the actual logic depends on the concrete subclass)
        newWeapon.Fire(direction);
    }
}
