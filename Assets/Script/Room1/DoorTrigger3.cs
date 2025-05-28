using UnityEngine;
using System.Collections;

public class DoorTrigger3 : MonoBehaviour
{
    public Transform targetPosition; // Set this to an empty GameObject placed where you want the door to slide to
    public float openSpeed = 2f;

    public AudioClip openDoorSound;  // Drag your door opening sound here in inspector

    private bool isOpen = false;
    private AudioSource audioSource;

    private void Awake()
    {
        // Get or add AudioSource component on this GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        // Don't play automatically on awake
        audioSource.playOnAwake = false;

    }

    public void OpenDoor()
    {
        if (!isOpen)
        {
            isOpen = true;

            // Play the door opening sound
            if (audioSource != null && openDoorSound != null)
            {
                audioSource.clip = openDoorSound;
                audioSource.Play();
            }

            StartCoroutine(SlideOpen());
        }
    }

    private IEnumerator SlideOpen()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = targetPosition.position;

        float elapsed = 0f;
        float duration = Vector3.Distance(startPos, endPos) / openSpeed;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;
    }
}
