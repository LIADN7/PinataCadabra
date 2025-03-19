using UnityEngine;
using UnityEngine.EventSystems;

public class MovementJoystick : MonoBehaviour
{
    // Joystick knob and background GameObjects
    public GameObject joystick;
    public GameObject joystickBG;

    // The vector representing the normalized direction from the joystick background center to the knob
    public Vector2 joystickVec;

    // Initial position of the joystick background (fixed)
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
    /// Called when the player touches the joystick button.
    /// The background remains fixed while the joystick knob will move relative to it.
    /// </summary>
    public void PointerDown()
    {
        // Instead of moving the background, we simply set the starting position for the knob relative to the fixed BG.
        joystick.transform.position = joystickOriginalPos;
    }

    /// <summary>
    /// Called while dragging the joystick.
    /// The joystick knob moves relative to the fixed background.
    /// </summary>
    /// <param name="baseEventData">The event data containing the current pointer position.</param>
    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;

        // Calculate the direction vector from the fixed background center to the current drag position
        joystickVec = (dragPos - joystickOriginalPos).normalized;

        // Calculate the distance between the drag position and the joystick background center
        float joystickDist = Vector2.Distance(dragPos, joystickOriginalPos);

        // Limit the movement of the joystick knob to within the joystickRadius
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
    /// Called when the player releases the joystick button.
    /// Resets the joystick knob position and the direction vector.
    /// </summary>
    public void PointerUp()
    {
        // Reset the direction vector to zero so that no movement is triggered
        joystickVec = Vector2.zero;
        // Reset the knob back to the fixed background position
        joystick.transform.position = joystickOriginalPos;
    }
}
