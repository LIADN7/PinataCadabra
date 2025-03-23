using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoreEntry : IComparable<ScoreEntry>
{
    public string playerName;
    public int score;
    public float finalGameTime;

    // Comparison between two ScoreEntry objects - first by score, then by finalGameTime
    public int CompareTo(ScoreEntry other)
    {
        if (this.score != other.score)
        {
            return other.score.CompareTo(this.score); // higher score comes first
        }
        return this.finalGameTime.CompareTo(other.finalGameTime); // lower time comes first
    }
}
[System.Serializable]
public class LeaderboardWrapper
{
    public List<ScoreEntry> leaderboardEntries;

    public LeaderboardWrapper(List<ScoreEntry> leaderboardEntries)
    {
        this.leaderboardEntries = leaderboardEntries;
    }
}

[System.Serializable]
public class Leaderboard
{
    private List<ScoreEntry> leaderboardEntries;

    public Leaderboard()
    {
        leaderboardEntries = new List<ScoreEntry>();
    }

    // Add a score entry and sort the leaderboard
    public void AddScore(ScoreEntry newScoreEntry)
    {
        leaderboardEntries.Add(newScoreEntry);
        leaderboardEntries.Sort();
        if (leaderboardEntries.Count > 10)
        {
            leaderboardEntries.RemoveAt(leaderboardEntries.Count - 1); // Keep only top 10
        }

    }

    // Convert the leaderboard to JSON
    public string ToJson()
    {
        // Wrap the leaderboard entries in a container class to allow serialization
        LeaderboardWrapper wrapper = new LeaderboardWrapper(leaderboardEntries);
        string json = JsonUtility.ToJson(wrapper, true);
        Debug.Log(json);
        return json;
    }

    // Load leaderboard from JSON
    public static Leaderboard FromJson(string json)
    {
        LeaderboardWrapper wrapper = JsonUtility.FromJson<LeaderboardWrapper>(json);
        Leaderboard leaderboard = new Leaderboard();
        leaderboard.leaderboardEntries = wrapper.leaderboardEntries;
        return leaderboard;
    }

    // Get the top 10 leaderboard entries
    public List<ScoreEntry> GetTopScores()
    {
        return leaderboardEntries;
    }
}