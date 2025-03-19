using UnityEngine;
using UnityEngine.EventSystems;

public class ShootingJoystick : MonoBehaviour
{
    [Header("Joystick UI References")]
    public GameObject joystick;
    public GameObject joystickBG;

    [Header("Weapon Reference")]
    public Weapon playerWeapon; // Assign in the Inspector (e.g., a ProjectileWeapon)
    [Header("Spawner Reference")]
    public WeaponSpawner spawner;

    // The vector representing the normalized direction from the joystick BG center to the knob
    private Vector2 joystickVec;
    private Vector2 joystickOriginalPos;
    private float joystickRadius;


    void Start()
    {
        // Store the original (fixed) position of the joystick background
        joystickOriginalPos = joystickBG.transform.position;
        // Calculate the maximum allowed distance for the joystick knob relative to the background center
        joystickRadius = joystickBG.GetComponent<RectTransform>().sizeDelta.y / 2;
    }

    /// <summary>
    /// Called when the player touches the shooting joystick.
    /// </summary>
    public void PointerDown()
    {
        // Reset the knob position to the fixed background center
        joystick.transform.position = joystickOriginalPos;
    }

    /// <summary>
    /// Called while dragging the shooting joystick.
    /// </summary>
    /// <param name="baseEventData">The event data containing the current pointer position.</param>
    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;

        // Calculate the direction vector from the background center to the drag position
        joystickVec = (dragPos - joystickOriginalPos).normalized;

        // Calculate the distance between the drag position and the joystick background center
        float joystickDist = Vector2.Distance(dragPos, joystickOriginalPos);

        // Move the knob within the radius limit
        if (joystickDist < joystickRadius)
        {
            joystick.transform.position = joystickOriginalPos + joystickVec * joystickDist;
        }
        else
        {
            joystick.transform.position = joystickOriginalPos + joystickVec * joystickRadius;
        }
    }

    /// <summary>
    /// Called when the player releases the shooting joystick.
    /// Spawns and fires a projectile in the *opposite* direction of the final drag.
    /// </summary>
    public void PointerUp()
    {


        // Calculate the shooting direction as the opposite of the joystick vector
        Vector2 shootDirection = -joystickVec;

        // Only fire if there's a significant pull on the joystick
        if (shootDirection.sqrMagnitude > 0.01f && spawner != null)
        {
            spawner.SpawnAndFireWeapon(shootDirection);
        }

        // Reset the knob and direction
        joystick.transform.position = joystickOriginalPos;
        joystickVec = Vector2.zero;
    }
}
