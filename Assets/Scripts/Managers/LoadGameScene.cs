using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Determines which game state to set based on the scene name provided 
/// </summary>
public class MainSceneLoader : MonoBehaviour
{
    public void LoadGameScene(string sceneName)
    {
        if (sceneName == "MainScene")
        {
            GameManager.inst.ChangeState(GameManager.GameState.Idle);
        }
        else if (sceneName == "GameScene")
        {
            GameManager.inst.ChangeState(GameManager.GameState.Play);
        }
    }
}
