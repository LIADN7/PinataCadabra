using UnityEngine;
using UnityEngine.SceneManagement;

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
        // SceneManager.LoadScene(sceneName);
    }
}
