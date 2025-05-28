using System.Collections;
using UnityEngine;
using TMPro;

public class StoryTrigger3 : MonoBehaviour
{
    [SerializeField] private AudioSource voiceline3;
    private bool hasActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasActivated && other.CompareTag("Player"))
        {
            hasActivated = true;
            if (voiceline3 != null)
            {
                voiceline3.Play();
            }
        }
    }
}
