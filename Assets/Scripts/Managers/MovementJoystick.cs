using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Provides joystick input functionality for movement by dragging the joystick knob relative to a fixed background.
/// </summary>
public class MovementJoystick : MonoBehaviour
{

    [Header("Joystick UI References")]

    public GameObject joystick;
    public GameObject joystickBG;
    public Vector2 joystickVec; // The normalized direction vector indicating joystick input.
    private Vector2 joystickOriginalPos;

    private float joystickRadius; // The maximum distance the joystick knob can move from its center.

    void Start()
    {
        joystickOriginalPos = joystickBG.transform.position;
        joystickRadius = joystickBG.GetComponent<RectTransform>().sizeDelta.y / 2;
    }

    public void PointerDown()
    {
        joystick.transform.position = joystickOriginalPos;
    }

    // Update the Worrior position (Called on Worrior class)
    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;
        joystickVec = (dragPos - joystickOriginalPos).normalized;
        float joystickDist = Vector2.Distance(dragPos, joystickOriginalPos);
        if (joystickDist < joystickRadius)
            joystick.transform.position = joystickOriginalPos + joystickVec * joystickDist;
        else
            joystick.transform.position = joystickOriginalPos + joystickVec * joystickRadius;
    }

    // Reset joystick pos
    public void PointerUp()
    {
        joystickVec = Vector2.zero;
        joystick.transform.position = joystickOriginalPos;
    }
}