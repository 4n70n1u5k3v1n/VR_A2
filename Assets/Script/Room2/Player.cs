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

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.CompareTag("Tile"))
        {
            Debug.Log("Touch Tile");
            ActivateTile(collision.gameObject.GetComponent<Tile>());
        }
        else if (collision.gameObject.CompareTag("Needle"))
        {
            locomotionSystem.enabled = false;
            if (locomotionSystem != null)
            {
                locomotionSystem.enabled = false;
            }

            if (warningPanel != null)
            {
                warningPanel.SetActive(true);
            }
        }
    }

    private IEnumerator ActivateTile(Tile tile)
    {
        if (tile != null)
        {
            tile.RaiseNeedles();
        }

        yield return new WaitForSeconds(2f);

        if (tile != null)
        {
            tile.LowerNeedles();
        }
    }

    public void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
