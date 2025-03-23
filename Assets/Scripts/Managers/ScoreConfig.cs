using System;
using UnityEngine;

/// <summary>
/// Manages the player's score
/// </summary>
public class ScoreConfig : MonoBehaviour
{
    // Singleton instance
    public static ScoreConfig inst { get; private set; }

    [Header("Player Data")]
    public string playerName;
    public int score;

    [Header("Game Duration")]
    public float finalGameTime; // Final time recorded at game end (in seconds)

    private void Awake()
    {
        if (inst == null)
        {

            inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (inst != this)
        {
            Destroy(gameObject);
        }

    }

    // Add points
    public void AddScore(int points)
    {
        score += points;
    }


    // Sets player name
    public void SetPlayerName(string name)
    {
        playerName = name;
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
    }


}
