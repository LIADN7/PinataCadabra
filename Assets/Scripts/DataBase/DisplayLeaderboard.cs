using System.Collections;
using UnityEngine;
using TMPro;

public class DisplayLeaderboard : MonoBehaviour
{
    public GameObject menu;
    public TextMeshProUGUI textMeshPro;
    private void Start()
    {

        // Call the method to update the leaderboard on the start
        DisplayLeaderboardEntries();
        menu.SetActive(false);
    }

    // Method to display leaderboard entries in the TextMeshPro UI
    public void DisplayLeaderboardEntries()
    {
        menu.SetActive(!menu.active);
        textMeshPro.text = DatabaseManager.inst.ToString();
    }



}
