using UnityEngine;

// Abstract Player class to enforce implementation in subclasses
public abstract class Player : MonoBehaviour
{
    // Player Life
    public int Life { get; protected set; }

    // Player Weapon reference (can be a separate Weapon class)
    public Weapon PlayerWeapon { get; protected set; }

    public string Name { get; protected set; }

    // Abstract Methods that subclasses must implement
    public abstract void Shoot(Vector2 direction);
    public abstract void Move();
    public abstract void Die();
    public abstract void Win();

    // Concrete implementation shared by subclasses
    public virtual void UpdateLife(int amount)
    {
        Life += amount;

        // Life boundary check
        if (Life <= 0)
        {
            Life = 0;
            Die();
        }
    }
}