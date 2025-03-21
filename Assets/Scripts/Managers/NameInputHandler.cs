using TMPro;
using UnityEngine;

public class ScoreInputHandler : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;

    // Call this method when editing ends or a button is pressed.
    public void UpdateScoreConfigFromInput()
    {
        // if (inputField.text.Length > 0)
        ScoreConfig.Inst.SetPlayerName(inputField.text);

    }
}
