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

    // Static instance for global access
    public static GameManager inst;

    // Current state of the game
    [SerializeField] private GameState currentState = GameState.Idle;

    private void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (inst == null)
        {
            inst = this;
            RestartGame(); // Optionally initialize or restart the game
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
    /// Changes the game state and triggers any state-specific logic.
    /// </summary>
    /// <param name="newState">The new game state to switch to.</param>
    public void ChangeState(GameState newState)
    {
        currentState = newState;
        Debug.Log($"Game state changed to: {currentState}");

        switch (currentState)
        {
            case GameState.Idle:
                // Handle idle state if needed
                break;
            case GameState.Play:
                // Handle play state if needed
                // ScoreManager.Inst.UpdateNameTextUI(ScoreConfig.Inst.playerName);
                break;
            case GameState.Loss:
                break;
            case GameState.Win:
                // Trigger game restart after 3 seconds when endgame is reached
                TriggerEndGame(3, "EndScene");
                break;
        }
    }

    /// <summary>
    /// Checks if the current game state matches the provided state.
    /// </summary>
    /// <param name="state">The state to check against.</param>
    /// <returns>True if the current state matches; otherwise, false.</returns>
    public bool IsState(GameState state)
    {
        return currentState == state;
    }

    /// <summary>
    /// Triggers a restart of the game after a specified delay.
    /// </summary>
    /// <param name="secondsBeforeRestart">Seconds to wait before restarting the game.</param>
    public void TriggerEndGame(int secondsBeforeRestart, string sceneName)
    {
        StartCoroutine(LoadSceneGameCoroutine(secondsBeforeRestart, sceneName));
    }

    /// <summary>
    /// Coroutine that waits for a specified duration before restarting the game.
    /// </summary>
    /// <param name="seconds">Seconds to wait.</param>
    /// <returns>IEnumerator for coroutine.</returns>
    private IEnumerator LoadSceneGameCoroutine(int seconds, string sceneName)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(sceneName);
        // RestartGame();
    }

    /// <summary>
    /// Restarts the game by reloading the active scene.
    /// </summary>
    private void RestartGame()
    {
        // Add any additional reset logic here if needed
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}