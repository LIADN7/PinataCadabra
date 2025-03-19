using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour
{
    public MovementJoystick movementJoystick;
    public float playerSpeed;
    public float yOffSet = 0.3f; // Maximum offset allowed from the initial Y position
    private float smoothing = 5f; // Smoothing factor for velocity interpolation

    private float initY;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initY = transform.position.y; // Save the initial Y position
    }

    // FixedUpdate is called once per physics frame
    void FixedUpdate()
    {
        // Calculate the target velocity based on joystick input
        Vector2 targetVelocity = Vector2.zero;
        if (movementJoystick.joystickVec.y != 0)
        {
            targetVelocity = new Vector2(movementJoystick.joystickVec.x * playerSpeed,
                                         movementJoystick.joystickVec.y * playerSpeed);
        }

        // Smoothly interpolate from the current velocity to the target velocity
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, targetVelocity, smoothing * Time.fixedDeltaTime);

        // Clamp the Y position between initY - val and initY + val to restrict vertical movement
        float clampedY = Mathf.Clamp(transform.position.y, initY - yOffSet, initY + yOffSet);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
    }
}
