using System.Collections;
using UnityEngine;
using TMPro;

public class DisplayLeaderboard : MonoBehaviour
{
    public GameObject menu;
    public TextMeshProUGUI textMeshPro;
    private void Start()
    {
        menu.SetActive(false);
    }

    // Method to display leaderboard entries
    public void DisplayLeaderboardEntries()
    {
        menu.SetActive(!menu.active);
        textMeshPro.text = DatabaseManager.inst.ToString();
    }



}
