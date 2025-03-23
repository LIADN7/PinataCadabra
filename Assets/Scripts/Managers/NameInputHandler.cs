using TMPro;
using UnityEngine;

/// <summary>
/// Reads user input from a InputField to update the player's name in ScoreConfig (On MainGame)
/// </summary>
public class ScoreInputHandler : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;

    private void Start()
    {
        inputField.text = ScoreConfig.inst?.playerName;
    }

    public void UpdateScoreConfigFromInput()
    {
        ScoreConfig.inst.SetPlayerName(inputField.text);
    }
}
