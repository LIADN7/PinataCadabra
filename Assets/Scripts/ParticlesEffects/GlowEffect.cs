using UnityEngine;

/// <summary>
/// Automatically applies a glow- effect to the GameObject by continuously
/// </summary>
public class GlowEffect : MonoBehaviour
{
    private void Start()
    {
        LeanTween.alpha(gameObject, 1f, 1f)
            .setFrom(0.5f)
            .setLoopPingPong(-1)
            .setEase(LeanTweenType.easeInOutSine);
        LeanTween.rotateZ(gameObject, 90f, 6)
            .setRepeat(-1)
            .setLoopPingPong(-1)
            .setEase(LeanTweenType.linear);
    }
}
