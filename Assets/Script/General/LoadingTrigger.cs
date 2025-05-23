using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Increase room number
            SharedResources.sceneCount++;

            // Set next scene name dynamically
            //SharedResources.sceneName = "Room" + SharedResources.sceneCount;
            string nextScene = SharedResources.sceneName;

            nextScene = SharedResources.sceneName + SharedResources.sceneCount;

            // load the next scene
            //SceneManager.LoadScene(nextScene);
            // Load the loading screen
            SceneManager.LoadScene("LoadingScene");
        }
    }
}
