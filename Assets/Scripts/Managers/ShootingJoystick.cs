using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Provides joystick input for shooting by dragging the joystick
/// </summary>
public class ShootingJoystick : MonoBehaviour
{
    [Header("Warrior Reference")]
    public Warrior warrior; //  Warrior (player) in the Inspector
    [Header("Joystick UI References")]
    public GameObject joystick;
    public GameObject joystickBG;
    [Header("Wand Reference")]
    public WandController wand; // WandController (wand object) in the Inspector
    private Vector2 joystickVec;
    private Vector2 joystickOriginalPos;
    private float joystickRadius;


    void Start()
    {
        joystickOriginalPos = joystickBG.transform.position;
        joystickRadius = joystickBG.GetComponent<RectTransform>().sizeDelta.y / 2;
        wand.HideWand();
    }

    public void PointerDown()
    {
        joystick.transform.position = joystickOriginalPos;
        wand.ShowWand();
    }

    // Update the position to shoot
    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;

        // Clamp the drag position so that the joystick only moves in the lower half of the circle.
        // If the drag is above the joystick's original position, set it to the original y value.
        if (dragPos.y > joystickOriginalPos.y)
        {
            dragPos.y = joystickOriginalPos.y;
        }

        // Calculate the direction vector and its distance
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

        wand.UpdateDirection(joystickVec);
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
        wand.HideWand();
    }
}
