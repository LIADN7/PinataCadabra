using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Central game manager that controls the game flow and state transitions
/// </summary>
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

    // Gets the current game state.
    public GameState CurrentState => currentState;

    // Changes the game state and triggers any state-specific logic
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

    // Sends the current score entry to the cloud database
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

    // Checks if the current game state matches the provided state
    public bool IsState(GameState state)
    {
        return currentState == state;
    }

    // Triggers the end game sequence by loading a specified scene after a delay
    public void TriggerEndGame(int secondsBeforeRestart, string sceneName)
    {
        StartCoroutine(LoadSceneGameCoroutine(secondsBeforeRestart, sceneName));
    }

    // Coroutine that waits for a specified duration before load the scene
    private IEnumerator LoadSceneGameCoroutine(int seconds, string sceneName)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(sceneName);
        // RestartGame();
    }

    // Resets the score.
    private void RestartGame()
    {
        ScoreConfig.inst?.ResetScore();
    }

    // Sets the level to be loaded when in the Play state.
    public void SetLevelToLoad(string level)
    {
        levelToLoad = level;
    }
}