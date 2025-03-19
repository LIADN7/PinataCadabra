using UnityEngine;

// Abstract base class for weapons. All weapon types should inherit from this class.
public abstract class Weapon : MonoBehaviour
{
    // The damage value that this weapon inflicts
    public int Damage { get; protected set; }

    // The rate of fire for this weapon (e.g., shots per second)
    public float FireRate { get; protected set; }

    // Abstract method for firing the weapon in a specified direction.
    public abstract void Fire(Vector2 direction);

}