using System.Collections;
using UnityEngine;
using TMPro;

public class StoryTrigger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI storyText;
    [SerializeField] private GameObject[] tileParents;
    private bool hasActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasActivated && other.CompareTag("Player"))
        {
            hasActivated = true;
            storyText.text += "\n" + "Mission 2" + "\n" + "Mission 3";
            StartCoroutine(ActivateTile());
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
}
