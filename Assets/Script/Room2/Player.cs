using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Locomotion;
using static UnityEngine.XR.OpenXR.Features.Interactions.HTCViveControllerProfile;

public class Player : MonoBehaviour
{
    public LocomotionMediator locomotionSystem;
    public GameObject warningPanel;

    public void PlayerDies()
    {
        if (locomotionSystem != null)
        {
            locomotionSystem.enabled = false;
        }

        if (warningPanel != null)
        {
            warningPanel.SetActive(true);
        }
    }

    public void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
