using System.Collections;
using UnityEngine;
using TMPro;

public class StoryTrigger5 : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI storyText;
    [SerializeField] private AudioSource voiceline5;
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
        if (voiceline5 != null)
        {
            voiceline5.Play();
        }
        

        yield return new WaitForSeconds(6f);
        
        storyText.text += "\n" + "# Solve the Padlock";
        
        if (missionCanvas != null)
        {
            missionCanvas.GetComponent<InputHandler>().ForceOpenMissionSheet();
        }
    }
}
