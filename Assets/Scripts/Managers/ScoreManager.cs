using System.Collections;
using TMPro;
using UnityEngine;
// using Unity.Services.CloudSave;

/// <summary>
/// Manages the player's score UI
/// </summary>
public class ScoreManager : MonoBehaviour
{
    // Singleton instance
    public static ScoreManager Inst { get; private set; }

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI scoreText;

    private GameTimeManager timeManager;

    private void Start()
    {
        Inst = this;
        timeManager = new GameTimeManager(this);


        if (GameManager.inst.IsState(GameManager.GameState.Play))
        {
            StartTimer();
        }
        else
        {
            StopTimer();
            UpdateTime(ScoreConfig.Inst.finalGameTime);
        }
        UpdateNameTextUI(ScoreConfig.Inst.playerName);
        UpdateScoreTextUI(ScoreConfig.Inst.score);
    }

    public void StartTimer()
    {
        timeManager.StartTimer();
    }

    public void StopTimer()
    {
        timeManager.StopTimer();
    }
    public float GetLastTimer()
    {
        return timeManager.GetElapsedTime();
    }

    public void UpdateNameTextUI(string name)
    {
        nameText.text = name.ToString();
    }
    public void UpdateScoreTextUI(int score)
    {

        scoreText.text = score.ToString();

    }

    public void UpdateTime(float time)
    {
        timeText.text = time.ToString();

    }



    // Nested private class that handles game time logic.
    private class GameTimeManager
    {
        private float elapsedTime = 0f;
        private bool isRunning = false;
        private Coroutine timerCoroutine;
        private ScoreManager parent;

        public GameTimeManager(ScoreManager parent)
        {
            this.parent = parent;
        }

        public void StartTimer()
        {
            elapsedTime = 0f;
            isRunning = true;
            if (timerCoroutine != null)
            {
                parent.StopCoroutine(timerCoroutine);
            }
            timerCoroutine = parent.StartCoroutine(TimerCoroutine());
        }

        public void StopTimer()
        {
            isRunning = false;
            if (timerCoroutine != null)
            {
                parent.StopCoroutine(timerCoroutine);
                timerCoroutine = null;
            }
        }

        public float GetElapsedTime() { return elapsedTime; }
        private IEnumerator TimerCoroutine()
        {
            while (isRunning)
            {
                yield return new WaitForSeconds(1f);
                if (GameManager.inst.IsState(GameManager.GameState.Play))
                {
                    elapsedTime += 1f;
                    if (parent.timeText != null)
                    {
                        parent.timeText.text = ((int)elapsedTime).ToString();
                    }
                }
            }
        }
    }
}
