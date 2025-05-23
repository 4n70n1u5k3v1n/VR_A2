using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Transform targetPoint;       // Set this in Inspector
    public float openSpeed = 2f;

    private AudioSource audioSource;
    public AudioClip doorOpenSound;     // Assign this in Inspector

    public ParticleSystem openParticlesPrefab;  // Drag your particle prefab here
    public Transform particleSpawnPoint; // Assign the empty GameObject here

    private bool isOpening = false;
    private bool hasPlayedSound = false;

    void Start()
    {
        // Try to get AudioSource component from the same GameObject
        audioSource = GetComponent<AudioSource>();

        // Add one if it doesn't exist
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    void Update()
    {
        if (isOpening && targetPoint != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, Time.deltaTime * openSpeed);

            if (!hasPlayedSound && doorOpenSound != null)
            {
                audioSource.PlayOneShot(doorOpenSound);
                hasPlayedSound = true;

                if (openParticlesPrefab != null && particleSpawnPoint != null)
                {
                    Instantiate(openParticlesPrefab, particleSpawnPoint.position, particleSpawnPoint.rotation);
                }
            }
        }
    }

    public void OpenDoor()
    {
        if (!isOpening)
        {
            Debug.Log("Sliding door open to target point...");
            isOpening = true;
        }
    }
}
