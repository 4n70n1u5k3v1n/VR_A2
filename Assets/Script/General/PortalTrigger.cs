using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTrigger : MonoBehaviour
{
    private string currentScene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (currentScene == "OriginScene")
            {
                SharedResources.sceneName = "Room1";
            }
            else if (currentScene == "Room1")
            {
                SharedResources.sceneName = "Room2";
            }
            else if (currentScene == "Room2")
            {
                SharedResources.sceneName = "Room3";
            }
            else if (currentScene == "Room3")
            {
                SharedResources.sceneName = "FinalScene";
            }
            SceneManager.LoadScene("LoadingScene");
        }
    }
}
