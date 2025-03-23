using System.Collections;
using TMPro;
using UnityEngine;
// using Unity.Services.CloudSave;

/// <summary>
/// Manages the player's life UI
/// </summary>
public class LifeManager : MonoBehaviour
{

    public static LifeManager Inst { get; private set; }

    [SerializeField] private TextMeshProUGUI lifeText;

    private void Start()
    {
        Inst = this;
    }

    public void UpdateLife(float life)
    {
        lifeText.text = life.ToString();

    }


}
