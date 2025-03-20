using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// This class is responsible for showing the game's elapsed time.
/// </summary>
public class GameTimeManager : MonoBehaviour
{

    private float elapsedTime = 0f;
    private bool isRunning = false;
    private Coroutine timerCoroutine;

    public TextMeshProUGUI textMesh;
    public float ElapsedTime => elapsedTime;

    void Start()
    {
        StartTimer();
    }

    // Start the timer
    public void StartTimer()
    {
        elapsedTime = 0f;
        isRunning = true;
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }
        timerCoroutine = StartCoroutine(TimerCoroutine());
    }

    // Stop timer
    public void StopTimer()
    {
        isRunning = false;
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
    }

    // Updates the text every second
    private IEnumerator TimerCoroutine()
    {
        while (isRunning)
        {
            yield return new WaitForSeconds(1f);
            if (GameManager.inst.IsState(GameManager.GameState.Play))
            {
                elapsedTime += 1f;
                if (textMesh != null)
                {
                    textMesh.text = ((int)elapsedTime).ToString();
                }
            }
        }
    }
}