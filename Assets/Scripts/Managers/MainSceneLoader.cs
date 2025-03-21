using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneLoader : MonoBehaviour
{
    public void LoadMainScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
