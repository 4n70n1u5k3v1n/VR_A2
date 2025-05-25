using UnityEngine;
using UnityEngine.SceneManagement; // <-- Needed for SceneManager

public class Room1PortalTrigger : MonoBehaviour
{
    private bool portalIsActive = false;

    public void ActivatePortal()
    {
        portalIsActive = true;
        // Optional: play sound, FX, open door animation
    }

    void OnTriggerEnter(Collider other)
    {
        if (portalIsActive && other.CompareTag("Player"))
        {
            SharedResources.sceneCount++;
            string nextScene = SharedResources.sceneName + SharedResources.sceneCount;
            SceneManager.LoadScene(nextScene);
        }
    }
}
