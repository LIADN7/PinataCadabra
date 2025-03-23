using UnityEngine;

public class WandController : MonoBehaviour
{
    // Updates the wand's rotation
    public void UpdateDirection(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
            transform.localRotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    public void ShowWand()
    {
        gameObject.SetActive(true);
    }

    public void HideWand()
    {
        gameObject.SetActive(false);
    }
}
