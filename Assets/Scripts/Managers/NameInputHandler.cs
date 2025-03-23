using TMPro;
using UnityEngine;

public class ScoreInputHandler : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;

    private void Start()
    {
        inputField.text = ScoreConfig.inst?.playerName;
    }


    // Call this method when editing ends or a button is pressed.
    public void UpdateScoreConfigFromInput()
    {
        ScoreConfig.inst.SetPlayerName(inputField.text);
    }
}
