using UnityEngine;

public class MovementPinata : MonoBehaviour
{
    [Header("Pendulum Movement Settings")]
    [SerializeField] private float swingAmplitude = 30f;  // Maximum rotation in degrees
    [SerializeField] private float swingSpeed = 1f;       // Speed of the swing

    private float initialZRotation;

    private void Start()
    {
        // Save the initial local rotation around the Z axis
        initialZRotation = transform.localEulerAngles.z;
    }

    private void Update()
    {
        // Calculate a new rotation around the Z axis using a sinusoidal function
        float angle = initialZRotation + swingAmplitude * Mathf.Sin(Time.time * swingSpeed);

        // Apply the rotation to the rope
        transform.localRotation = Quaternion.Euler(0f, 0f, angle);
    }
}
