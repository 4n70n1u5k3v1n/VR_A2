using UnityEngine;

public class DoorTrigger2 : MonoBehaviour
{
    [Header("Door Settings")]
    public GameObject door;                   // The door GameObject
    public Transform openPosition;           // Target position when door opens
    public Transform closedPosition;         // Target position when door closes
    public float moveSpeed = 2f;

    [Header("Statue Settings")]
    public GameObject statue;                // The statue to pick up
    private bool hasPickedUpStatue = false;

    [Header("Audio & Particles (Optional)")]
    public AudioClip doorSound;
    public ParticleSystem doorParticles;
    private AudioSource audioSource;

    private bool doorShouldBeClosed = false;
    private bool doorShouldBeOpened = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (door == null) return; // Safety check

        Transform doorTransform = door.transform;

        if (doorShouldBeClosed)
        {
            doorTransform.position = Vector3.MoveTowards(doorTransform.position, closedPosition.position, moveSpeed * Time.deltaTime);
        }
        else if (doorShouldBeOpened)
        {
            doorTransform.position = Vector3.MoveTowards(doorTransform.position, openPosition.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPickedUpStatue)
        {
            doorShouldBeClosed = true;
            doorShouldBeOpened = false;
            PlayEffects();
        }
    }

    public void OnStatuePickedUp()
    {
        hasPickedUpStatue = true;
        doorShouldBeClosed = false;
        doorShouldBeOpened = true;
        PlayEffects();
    }

    private void PlayEffects()
    {
        if (doorSound && audioSource)
            audioSource.PlayOneShot(doorSound);

        if (doorParticles)
            doorParticles.Play();
    }
}
