using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class DoorController : MonoBehaviour
{
    public XRSocketInteractor socket1;
    public XRSocketInteractor socket2;
    public XRSocketInteractor socket3;
    public XRSocketInteractor socket4;
    public XRSocketInteractor socket5;

    public GameObject door;
    public float doorSpeed = 2f;
    public Vector3 doorTargetPosition; // Manually set this in the Inspector

    public ParticleSystem doorParticles; // Particle effect for door movement
    public AudioSource doorAudioSource; // Audio source for sound effects
    public AudioClip doorOpenSound; // Sound when the door opens
    public AudioClip doorCloseSound; // Sound when the door closes

    private bool isDoorOpening = false;
    private bool isDoorClosing = false;
    private Vector3 doorInitialPosition;

    private void Start()
    {
        // Store the initial position of the door
        doorInitialPosition = door.transform.position;

        // Subscribe to socket events
        socket1.selectEntered.AddListener(OnItemAttached);
        socket2.selectEntered.AddListener(OnItemAttached);
        socket3.selectEntered.AddListener(OnItemAttached);
        socket4.selectEntered.AddListener(OnItemAttached);
        socket5.selectEntered.AddListener(OnItemAttached);


        socket1.selectExited.AddListener(OnItemRemoved);
        socket2.selectExited.AddListener(OnItemRemoved);
        socket3.selectExited.AddListener(OnItemRemoved);
        socket4.selectExited.AddListener(OnItemRemoved);
        socket5.selectExited.AddListener(OnItemRemoved);

    }

    private void Update()
    {
        if (isDoorOpening)
        {
            // Move the door to the target position
            door.transform.position = Vector3.MoveTowards(door.transform.position, doorTargetPosition, doorSpeed * Time.deltaTime);

            // Stop moving when the door reaches the target position
            if (door.transform.position == doorTargetPosition)
            {
                isDoorOpening = false;
                StopParticles(); // Stop particles when the door stops moving
            }
        }
        else if (isDoorClosing)
        {
            // Move the door back to the initial position
            door.transform.position = Vector3.MoveTowards(door.transform.position, doorInitialPosition, doorSpeed * Time.deltaTime);

            // Stop moving when the door reaches the initial position
            if (door.transform.position == doorInitialPosition)
            {
                isDoorClosing = false;
                StopParticles(); // Stop particles when the door stops moving
            }
        }
    }

    private void OnItemAttached(SelectEnterEventArgs arg)
    {
        // Check if all sockets have items attached
        if (socket1.hasSelection && socket2.hasSelection && socket3.hasSelection)
        {
            // Start opening the door
            isDoorOpening = true;
            isDoorClosing = false;

            // Play particles and sound
            PlayParticles();
            PlaySound(doorOpenSound);
        }
    }

    private void OnItemRemoved(SelectExitEventArgs arg)
    {
        // If any item is removed, start closing the door
        isDoorClosing = true;
        isDoorOpening = false;

        // Play particles and sound
        PlayParticles();
        PlaySound(doorCloseSound);
    }

    private void PlayParticles()
    {
        if (doorParticles != null && !doorParticles.isPlaying)
        {
            doorParticles.Play(); // Start the particle effect
        }
    }

    private void StopParticles()
    {
        if (doorParticles != null && doorParticles.isPlaying)
        {
            doorParticles.Stop(); // Stop the particle effect
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (doorAudioSource != null && clip != null)
        {
            doorAudioSource.PlayOneShot(clip); // Play the sound effect
        }
    }
}