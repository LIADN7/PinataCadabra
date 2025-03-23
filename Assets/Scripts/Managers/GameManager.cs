using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Enum representing the different game states
    public enum GameState
    {
        Idle,
        Play,
        Win,
        Loss
    }

    public static GameManager inst;

    private string levelToLoad = "GameScene";

    // Current state of the game
    [SerializeField] private GameState currentState = GameState.Idle;

    private void Awake()
    {
        if (inst == null)
        {
            inst = this;
            RestartGame();
        }
        else
        {
            Destroy(gameObject);
        }

        // Persist the GameManager across scenes
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Gets the current game state.
    /// </summary>
    public GameState CurrentState => currentState;

    /// <summary>
    /// Changes the game state and triggers any state-specific logic
    /// </summary>
    public void ChangeState(GameState newState)
    {
        currentState = newState;
        Debug.Log($"Game state changed to: {currentState}");

        switch (currentState)
        {
            case GameState.Idle:
                RestartGame();
                StartCoroutine(LoadSceneGameCoroutine(0, "MainScene"));
                break;
            case GameState.Play:
                if (ScoreConfig.inst?.playerName.Length > 0)
                {
                    RestartGame();
                    StartCoroutine(LoadSceneGameCoroutine(0, levelToLoad));
                }
                break;
            case GameState.Loss:
                SendScoreToCload();
                TriggerEndGame(2, "EndScene");
                break;
            case GameState.Win:
                SendScoreToCload();
                TriggerEndGame(3, "EndScene");
                break;
        }
    }


    private void SendScoreToCload()
    {

        ScoreEntry newScoreEntry = new ScoreEntry()
        {
            playerName = ScoreConfig.inst.playerName,
            score = ScoreConfig.inst.score,
            finalGameTime = ScoreConfig.inst.finalGameTime
        };

        DatabaseManager.inst.AddNewScoreEntry(newScoreEntry);
    }

    /// <summary>
    /// Checks if the current game state matches the provided state
    /// </summary>
    public bool IsState(GameState state)
    {
        return currentState == state;
    }

    public void TriggerEndGame(int secondsBeforeRestart, string sceneName)
    {
        StartCoroutine(LoadSceneGameCoroutine(secondsBeforeRestart, sceneName));
    }

    /// <summary>
    /// Coroutine that waits for a specified duration before load the scene
    /// </summary>
    private IEnumerator LoadSceneGameCoroutine(int seconds, string sceneName)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(sceneName);
        // RestartGame();
    }

    private void RestartGame()
    {
        ScoreConfig.inst?.ResetScore();
    }


    public void SetLevelToLoad(string level)
    {
        levelToLoad = level;
    }
}