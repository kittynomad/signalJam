using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private float _sceneTransitionTime = 0f;
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneDelayed(string sceneName)
    {
        StartCoroutine(DelayCoroutine(sceneName));
    }

    IEnumerator DelayCoroutine(string sceneName)
    {
        yield return new WaitForSeconds(_sceneTransitionTime);
        LoadScene(sceneName);
    }
}
