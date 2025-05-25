using UnityEngine;
using UnityEngine.UI;

public class StepSoundAndTextCanvas : MonoBehaviour
{
    public AudioClip soundEffect;
    public GameObject[] popUpCanvases;  // Array of canvases

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Ensure all canvases are hidden at start
        if (popUpCanvases != null)
        {
            foreach (var canvas in popUpCanvases)
            {
                if (canvas != null)
                    canvas.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlaySound();
            ShowCanvases();
        }
    }

    void PlaySound()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.clip = soundEffect;
        audioSource.Play(); // Important to actually play the sound
    }

    void ShowCanvases()
    {
        if (popUpCanvases != null)
        {
            foreach (var canvas in popUpCanvases)
            {
                if (canvas != null)
                    canvas.SetActive(true);
            }
        }
    }

    // Optional: hide canvases when player exits
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HideCanvases();
        }
    }

    void HideCanvases()
    {
        if (popUpCanvases != null)
        {
            foreach (var canvas in popUpCanvases)
            {
                if (canvas != null)
                    canvas.SetActive(false);
            }
        }
    }
}