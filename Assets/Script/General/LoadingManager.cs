using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private float waitTime = 3f;

    void Start()
    {
        StartCoroutine(LoadAfterDelay());
    }

    System.Collections.IEnumerator LoadAfterDelay()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(SharedResources.sceneName);
    }
}
