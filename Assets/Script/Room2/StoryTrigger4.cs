using System.Collections;
using UnityEngine;
using TMPro;

public class StoryTrigger4 : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI storyText;
    [SerializeField] private AudioSource voiceline4;
    [SerializeField] private GameObject missionCanvas;
    private bool hasActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasActivated && other.CompareTag("Player"))
        {
            hasActivated = true;
            StartCoroutine(AddMission());
        }
    }

    private IEnumerator AddMission()
    {
        if (voiceline4 != null)
        {
            voiceline4.Play();
        }
        storyText.text += "\n" + "# Grab the Mirrors and Plates";

        yield return new WaitForSeconds(5f);

        if (missionCanvas != null)
        {
            missionCanvas.SetActive(true);
        }
    }
}
