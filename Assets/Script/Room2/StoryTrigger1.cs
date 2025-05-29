using System.Collections;
using UnityEngine;
using TMPro;

public class StoryTrigger1 : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI storyText;
    [SerializeField] private GameObject[] tileParents;
    [SerializeField] private AudioSource voiceline1;
    [SerializeField] private GameObject missionCanvas;
    private bool hasActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasActivated && other.CompareTag("Player"))
        {
            hasActivated = true;
            StartCoroutine(ActivateTile());
            StartCoroutine(AddMission());
        }
    }

    private IEnumerator ActivateTile()
    {
        foreach (GameObject parent in tileParents)
        {
            if (parent == null)
            {
                continue;
            }

            foreach (Transform tile in parent.transform)
            {
                // Each tile should have the script that controls its needles
                Tile tileController = tile.GetComponent<Tile>();
                if (tileController != null)
                {
                    tileController.RaiseNeedles();
                }
            }
        }

        yield return new WaitForSeconds(2f);

        foreach (GameObject parent in tileParents)
        {
            if (parent == null)
            {
                continue;
            }

            foreach (Transform tile in parent.transform)
            {
                Tile tileController = tile.GetComponent<Tile>();
                if (tileController != null)
                {
                    tileController.LowerNeedles();
                }
            }
        }
    }

    private IEnumerator AddMission()
    {
        if (voiceline1 != null)
        {
            voiceline1.Play();
        }

        yield return new WaitForSeconds(5f);

        storyText.text += "# Disable the Trap";
        if (missionCanvas != null)
        {
            missionCanvas.GetComponent<InputHandler>().ForceOpenMissionSheet();
        }
    }
}
