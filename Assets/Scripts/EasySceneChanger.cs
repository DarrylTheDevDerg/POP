using UnityEngine;
using UnityEngine.SceneManagement;

public class EasySceneChanger : MonoBehaviour
{
    public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
