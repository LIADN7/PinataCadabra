using TMPro;
using UnityEngine;
// using Unity.Services.CloudSave;

/// <summary>
/// Manages the player's score
/// </summary>
public class ScoreConfig : MonoBehaviour
{
    // Singleton instance
    public static ScoreConfig Inst { get; private set; }

    [Header("Player Data")]
    public string playerName;
    public int score;

    [Header("Game Duration")]
    public float finalGameTime; // Final time recorded at game end (in seconds)

    private void Awake()
    {
        if (Inst == null)
        {

            Inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Inst != this)
        {
            Destroy(gameObject);
        }

    }

    // Add points
    public void AddScore(int points)
    {
        score += points;
        // UpdateScoreTextUI();
    }


    // Sets player name
    public void SetPlayerName(string name)
    {
        playerName = name;
        // UpdateNameTextUI();
    }
    public void SetFinalGameTime(float time)
    {
        finalGameTime = time;
    }


    // Resets the player's score and time
    public void ResetScore()
    {
        finalGameTime = 0;
        score = 0;
        // UpdateScoreTextUI();

    }

    // Update leadboard on Unity Cloud
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
