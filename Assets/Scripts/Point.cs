using UnityEngine;

/// <summary>
/// Represents a point object that carries a specific score value
/// </summary>
public class Point : MonoBehaviour
{
    [Header("Point Settings")]
    [SerializeField] private int pointValue = 1; // The score value this point is worth.

    // Gets the value of this point.
    public int PointValue => pointValue;
}
