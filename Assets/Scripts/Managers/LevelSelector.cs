using TMPro;
using UnityEngine;

/// <summary>
/// Allows the player to select a level using a Dropdown
/// </summary>
public class LevelSelector : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown levelDropdown;

    private void Start()
    {
        GameManager.inst?.SetLevelToLoad("GameScene");
        levelDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    private void OnDropdownValueChanged(int index)
    {
        string level = "GameScene";
        if (index == 1)
        {
            level = "GameExtScene";
        }
        GameManager.inst?.SetLevelToLoad(level);
    }

}
