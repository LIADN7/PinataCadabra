using UnityEngine;

// Abstract base class for weapons. All weapon types should inherit from this class.
public abstract class Weapon : MonoBehaviour
{
    // The damage value that this weapon inflicts
    public int Damage { get; protected set; }

    // The spell will be destroyed after X seconds
    public float Lifetime { get; protected set; }
    public bool IsDead = false;

    // Abstract method for firing the weapon in a specified direction.
    public abstract void Fire(Vector2 direction);

    // Called when the projectile (or spell) hits something.
    public abstract void Hit();

}