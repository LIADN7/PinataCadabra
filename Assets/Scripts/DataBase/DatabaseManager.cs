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
    private bool isCloudConnected = false; // Flag to indicate cloud connectivity



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
        if (isCloudConnected)
        {
            LoadData("LeaderboardData");
        }
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
            isCloudConnected = true;

            Debug.Log("Connected to Unity Cloud and signed in as anonymous user");
        }
        catch (System.Exception e)
        {
            isCloudConnected = false;
            Debug.LogError("Error connecting to Unity Cloud: " + e.Message);
        }
    }

    // Save leaderboard data to Unity Cloud.
    public async Task SaveData(string key, string jsonData)
    {
        // If not connected, attempt to reconnect.
        if (!IsInternetConnected())
        {
            Debug.LogWarning("Internet not available. Attempting to reconnect...");
            await ConnectToUnityCloud();
            if (!IsInternetConnected())
            {
                Debug.LogError("Unable to reconnect. Save aborted.");
                return;
            }
        }

        try
        {
            string storageKey = key;
            var playerData = new Dictionary<string, object>
            {
                { storageKey, jsonData }
            };
            await CloudSaveService.Instance.Data.ForceSaveAsync(playerData);
            isCloudConnected = true;
            Debug.Log("Leaderboard data saved to Unity Cloud");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error saving leaderboard to Unity Cloud: " + e.Message);
        }
    }

    // Load leaderboard data from Unity Cloud.
    public async void LoadData(string key)
    {
        if (!IsInternetConnected())
        {
            Debug.LogWarning("Internet not available. Attempting to reconnect...");
            await ConnectToUnityCloud();
            if (!IsInternetConnected())
            {
                Debug.LogError("Unable to reconnect. Load aborted.");
                return;
            }
        }

        try
        {
            string storageKey = key;
            var result = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { storageKey });
            if (result.ContainsKey(storageKey))
            {
                string leaderboardJson = result[storageKey] as string;
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


    // Add a new ScoreEntry to the leaderboard and save it to the cloud.
    public async Task AddNewScoreEntry(ScoreEntry newScoreEntry)
    {
        leaderboard.AddScore(newScoreEntry);
        string json = leaderboard.ToJson();
        await SaveData("LeaderboardData", json);
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



    // Checks for an internet connection.
    private bool IsInternetConnected()
    {
        return Application.internetReachability != NetworkReachability.NotReachable;
    }
}
