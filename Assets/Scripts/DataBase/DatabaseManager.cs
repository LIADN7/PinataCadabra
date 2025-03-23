using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using System.Threading.Tasks;
using Unity.Services.CloudSave;
using System.Collections.Generic;

/// <summary>
/// Manages the leaderboard data from Unity Cloud
/// Handles connecting to Unity Services, and ensuring leaderboard data is synchronized across devices
/// </summary>
public class DatabaseManager : MonoBehaviour
{
    private Leaderboard leaderboard;
    public static DatabaseManager inst;


    private void Awake()
    {

        if (inst == null)
        {
            inst = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private async void Start()
    {
        leaderboard = new Leaderboard();
        await ConnectToUnityCloud();
        LoadData();
    }

    // Connect to Unity Cloud
    private async Task ConnectToUnityCloud()
    {
        try
        {
            // Initialize Unity Services
            await UnityServices.InitializeAsync();
            // Sign in
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

            Debug.Log("Connected to Unity Cloud and signed in as anonymous user");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error connecting to Unity Cloud: " + e.Message);
        }
    }

    // Save leaderboard data to Unity Cloud
    public async Task SaveData()
    {
        string leaderboardJson = leaderboard.ToJson();
        await SaveToCloudAsync(leaderboardJson);
    }

    private async Task SaveToCloudAsync(string leaderboardJson)
    {
        try
        {
            string key = "LeaderboardData";  // Define a key for storing leaderboard data
            var playerData = new Dictionary<string, object>
        {
            { key, leaderboardJson }
        };
            await CloudSaveService.Instance.Data.ForceSaveAsync(playerData);
            Debug.Log("Leaderboard data saved to Unity Cloud");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error saving leaderboard to Unity Cloud: " + e.Message);
        }
    }

    // Load leaderboard data from Unity Cloud
    public async Task LoadData()
    {
        try
        {
            string key = "LeaderboardData";
            // Retrieve leaderboard data from Unity Cloud
            var result = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { key });
            if (result.ContainsKey(key))
            {
                string leaderboardJson = result[key] as string;
                if (!string.IsNullOrEmpty(leaderboardJson))
                {
                    leaderboard = Leaderboard.FromJson(leaderboardJson);
                }
                else
                {
                    Debug.Log("No leaderboard data found in Unity Cloud.");
                }
            }
            else
            {
                Debug.Log("No leaderboard data found in Unity Cloud.");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error loading leaderboard from Unity Cloud: " + e.Message);
        }
    }


    // Add a new ScoreEntry to the leaderboard and save it
    public async Task AddNewScoreEntry(ScoreEntry newScoreEntry)
    {
        leaderboard.AddScore(newScoreEntry);
        await SaveData();
    }


    // Converts the leaderboard entries into a formatted string representing the top scores
    public string ToString()
    {

        string res = "";
        int rank = 1;
        foreach (var entry in leaderboard.GetTopScores())
        {
            res += $"{rank}. Name: {entry.playerName} - Score: {entry.score} - Time: {entry.finalGameTime} sec\n";
            rank++;
        }
        return res;
    }
}
