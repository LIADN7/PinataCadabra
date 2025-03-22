using UnityEngine;
/// <summary>
/// Spawns weapon prefabs and triggers their fire functionality
/// </summary>
public class WeaponSpawner : MonoBehaviour
{
    [Header("Weapon Prefab to Spawn")]
    public Weapon weaponPrefab;

    [Header("Spawn Settings")]
    public Transform spawnPoint;

    public float FireRate = 2f;

    private float lastFireTime = 0f; // Cooldown tracking


    // Spawns the weapon prefab and calls Fire()
    public void SpawnAndFireWeapon(Vector2 direction)
    {
        // Cooldown check
        if (Time.time - lastFireTime < FireRate)
        {
            Debug.Log("WeaponSpawner: Cooldown active.");
            return;
        }

        lastFireTime = Time.time;
        Weapon newWeapon = Instantiate(weaponPrefab, spawnPoint.position, Quaternion.identity);
        newWeapon.Fire(direction);
    }
}
