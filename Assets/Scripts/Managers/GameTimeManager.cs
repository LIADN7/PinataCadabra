using System.Collections;
using TMPro;
using UnityEngine;

public class GameTimeManager : MonoBehaviour
{
    private float elapsedTime = 0f;
    private bool isRunning = false;
    private Coroutine timerCoroutine;

    public TextMeshProUGUI textMesh;

    /// <summary>
    /// Returns the total elapsed time (in seconds).
    /// </summary>
    public float ElapsedTime => elapsedTime;


    void Start()
    {
        StartTimer();
    }


    /// <summary>
    /// Starts the timer: resets the elapsed time and starts the coroutine.
    /// </summary>
    public void StartTimer()
    {
        elapsedTime = 0f;
        isRunning = true;

        // If a timer coroutine is already running, stop it first.
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }
        timerCoroutine = StartCoroutine(TimerCoroutine());
    }

    /// <summary>
    /// Stops the timer and the coroutine.
    /// </summary>
    public void StopTimer()
    {
        isRunning = false;
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
    }

    private IEnumerator TimerCoroutine()
    {
        while (isRunning)
        {
            yield return new WaitForSeconds(1f);

            // Only update the timer if the game is in Play state.
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
