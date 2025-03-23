using UnityEngine;

/// <summary>
/// Spawns point prefabs, applies a velocity to simulate a "fly out" effect
/// </summary>
public class PointSpawner : MonoBehaviour
{
    [Header("Prefab and Spawn Settings")]
    [SerializeField] private GameObject[] pointPrefabs; // The prefab to spawn
    [SerializeField] private Transform spawnPoint;   // The spawn position

    [Header("Falling/Flying Settings")]
    [SerializeField] private float fallSpeed = 5f;
    [SerializeField] private float destroyAfter = 5f;

    // Spawns the point prefab at the spawnPoint's position and makes it fly out
    public int SpawnPoint()
    {

        int pointValue = -1;
        GameObject pointPrefab = GetRandomPointPrefab();
        Point tempP = pointPrefab.GetComponent<Point>();
        if (tempP)
        {
            pointValue = tempP.PointValue;
        }

        // Set the spawn position
        Vector3 spawnPos = new Vector3(spawnPoint.position.x, spawnPoint.position.y, -2f);


        // Instantiate the prefab at the spawnPoint's position
        GameObject spawnedPoint = Instantiate(pointPrefab, spawnPos, Quaternion.identity);

        Rigidbody2D rb = spawnedPoint.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Determine a random sideways component and an upward component
            float randomX = Random.Range(-0.5f, 0.5f);
            float randomY = Random.Range(1f, 1.5f);
            rb.linearVelocity = new Vector2(randomX * fallSpeed, randomY * fallSpeed);
            rb.angularVelocity = Random.Range(-200f, 200f);
        }
        else
        {
            float randomX = Random.Range(-1f, 1f);
            Vector3 targetPos = spawnPos + new Vector3(randomX, -5f, 0f);
            LeanTween.move(spawnedPoint, targetPos, destroyAfter).setEase(LeanTweenType.easeInQuad);
        }
        Destroy(spawnedPoint, destroyAfter);
        return pointValue;
    }


    // Get a random point prefab
    public GameObject GetRandomPointPrefab()
    {
        int randomIndex = Random.Range(0, pointPrefabs.Length);
        return pointPrefabs[randomIndex];
    }
}
