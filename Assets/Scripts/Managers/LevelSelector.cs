using TMPro;
using UnityEngine;

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
