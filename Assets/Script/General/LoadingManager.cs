using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public float waitTime = 3f;

    void Start()
    {
        StartCoroutine(LoadAfterDelay());
    }

    System.Collections.IEnumerator LoadAfterDelay()
    {
        yield return new WaitForSeconds(waitTime);

        // Load the dynamic next room (e.g., Room2, Room3)
        SceneManager.LoadScene(SharedResources.sceneName);
    }
}
