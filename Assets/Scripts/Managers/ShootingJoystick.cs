using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Provides joystick input for shooting by dragging the joystick
/// </summary>
public class ShootingJoystick : MonoBehaviour
{
    [Header("Warrior Reference")]
    public Warrior warrior; // Assign the Warrior (player) in the Inspector
    [Header("Joystick UI References")]
    public GameObject joystick;
    public GameObject joystickBG;

    private Vector2 joystickVec;
    private Vector2 joystickOriginalPos;
    private float joystickRadius;


    void Start()
    {
        joystickOriginalPos = joystickBG.transform.position;
        joystickRadius = joystickBG.GetComponent<RectTransform>().sizeDelta.y / 2;
    }

    public void PointerDown()
    {
        joystick.transform.position = joystickOriginalPos;
    }

    // Update the position to shoot
    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;

        joystickVec = (dragPos - joystickOriginalPos).normalized;
        float joystickDist = Vector2.Distance(dragPos, joystickOriginalPos);

        if (joystickDist < joystickRadius)
        {
            joystick.transform.position = joystickOriginalPos + joystickVec * joystickDist;
        }
        else
        {
            joystick.transform.position = joystickOriginalPos + joystickVec * joystickRadius;
        }
    }

    // Called when the player releases the shooting joystick
    // Spawns and fires a projectile in the *opposite* direction of the final drag
    public void PointerUp()
    {

        Vector2 shootDirection = -joystickVec;

        // Only fire if there's a significant pull
        if (shootDirection.sqrMagnitude > 0.01f)
        {
            warrior.Shoot(shootDirection);
        }

        // Reset the knob
        joystick.transform.position = joystickOriginalPos;
        joystickVec = Vector2.zero;
    }
}
