using UnityEngine;
// Using the Unity Cloud Save namespace if you have it installed
// using Unity.Services.CloudSave;

public class ScoreManager : MonoBehaviour
{
    // Singleton instance
    public static ScoreManager Instance { get; private set; }

    [Header("Player Data")]
    public string playerName;

    [Header("Game Duration")]
    public float finalGameTime; // Final time recorded at game end (in seconds)

    public int score;





    private void Awake()
    {
        // Enforce singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Adds points to the player's score.
    /// </summary>
    public void AddScore(int points)
    {
        score += points;
    }

    /// <summary>
    /// Sets the player's name.
    /// </summary>
    public void SetPlayerName(string name)
    {
        playerName = name;
    }

    /// <summary>
    /// Resets the player's score.
    /// </summary>
    public void ResetScore()
    {
        finalGameTime = 0;
        score = 0;
    }

    /// <summary>
    /// Saves the player's name and score to Unity Cloud.
    /// Replace the placeholder code with actual Unity Cloud Save API calls.
    /// </summary>
    public void SaveScoreToCloud()
    {
        Debug.Log("Saving score to Unity Cloud...");

        // Create a data object (could be a JSON or a Dictionary<string, object>)
        var scoreData = new System.Collections.Generic.Dictionary<string, object>
        {
            { "playerName", playerName },
            { "score", score }
        };

        // Pseudo-code for cloud save:
        // Unity.Services.CloudSave.CloudSaveService.Instance.Data.ForceSaveAsync(scoreData)
        //     .ContinueWith(task => {
        //         if (task.IsCompletedSuccessfully)
        //         {
        //             Debug.Log("Score saved successfully!");
        //         }
        //         else
        //         {
        //             Debug.LogError("Error saving score: " + task.Exception);
        //         }
        //     });

        // For testing, you might want to simply use PlayerPrefs:
        // PlayerPrefs.SetString("PlayerName", playerName);
        // PlayerPrefs.SetInt("Score", score);
        // PlayerPrefs.Save();
        // Debug.Log("Score saved locally using PlayerPrefs.");
    }
}
