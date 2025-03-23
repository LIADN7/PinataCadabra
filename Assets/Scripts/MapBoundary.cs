using UnityEngine;

/// <summary>
/// Positions the attached GameObject to stick to either the left or right side of the main camera
/// </summary>
public class MapBoundary : MonoBehaviour
{
    [SerializeField] private bool isRightBoundary = true; // true = right boundary, false = left boundary
    [SerializeField] private float horizontalOffset = 0.5f;   // Additional offset from the camera edge
    [SerializeField] private float verticalPosition = 0.5f;   // Viewport Y position (0 = bottom, 1 = top; 0.5 = center)

    private Camera mainCamera;
    private float zDistance;

    private void Start()
    {
        mainCamera = Camera.main;
        // Calculate the distance between camera and this object along Z
        zDistance = Mathf.Abs(mainCamera.transform.position.z - transform.position.z);
        UpdateBoundaryPosition();
    }


    // Update the boundary position on the game
    private void UpdateBoundaryPosition()
    {
        Vector3 boundaryPos;
        if (isRightBoundary)
        {
            // Right edge of the viewport (x=1)
            boundaryPos = mainCamera.ViewportToWorldPoint(new Vector3(1, verticalPosition, zDistance));
            boundaryPos.x += horizontalOffset;
        }
        else
        {
            // Left edge of the viewport (x=0)
            boundaryPos = mainCamera.ViewportToWorldPoint(new Vector3(0, verticalPosition, zDistance));
            boundaryPos.x -= horizontalOffset;
        }

        transform.position = boundaryPos;
    }
}
