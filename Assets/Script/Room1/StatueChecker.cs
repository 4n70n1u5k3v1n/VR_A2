using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.SceneManagement;
using System.Collections;

public class StatueChecker : MonoBehaviour
{
    [System.Serializable]
    public class SocketMatch
    {
        public Transform socket;          // XR Socket Transform - drag here in Inspector
        public GameObject correctStatue;  // The correct statue GameObject to check against
    }

    public SocketMatch[] socketMatches;       // Assign sockets and statues in Inspector

    [Header("Audio Settings")]
    public AudioClip successClip;             // First sound (e.g. click, lock) — loops
    public AudioClip additionalClip;          // Second sound (e.g. chime) — plays once

    [Header("Portal Settings")]
    [SerializeField] private GameObject teleportTrigger;
    [SerializeField] private Material activeTeleportMaterial;

    private AudioSource loopSource;       // Plays successClip on loop
    private AudioSource extraAudioSource; // Plays additionalClip once
    private bool portalActivated = false;

    void Start()
    {
        loopSource = gameObject.AddComponent<AudioSource>();
        loopSource.loop = true;
        loopSource.spatialBlend = 1.0f;

        extraAudioSource = gameObject.AddComponent<AudioSource>();
        extraAudioSource.spatialBlend = 1.0f;

        // Optional: Stop audio when scene changes
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    void Update()
    {
        if (!portalActivated && AllStatuesCorrectlyPlaced())
        {
            Debug.Log("All statues correctly placed! Activating portal...");

            StartCoroutine(PlaySuccessSequence());

            if (teleportTrigger != null && activeTeleportMaterial != null)
            {
                teleportTrigger.GetComponent<BoxCollider>().isTrigger = true;
                teleportTrigger.GetComponent<Renderer>().material = activeTeleportMaterial;
            }

            portalActivated = true;
        }
    }

    private IEnumerator PlaySuccessSequence()
    {
        if (successClip != null)
        {
            loopSource.clip = successClip;
            loopSource.Play();
        }

        yield return new WaitForSeconds(0.2f); // Optional tiny delay

        if (additionalClip != null)
            extraAudioSource.PlayOneShot(additionalClip);
    }

    bool AllStatuesCorrectlyPlaced()
    {
        foreach (SocketMatch match in socketMatches)
        {
            XRSocketInteractor interactor = match.socket.GetComponent<XRSocketInteractor>();

            if (interactor == null)
            {
                Debug.LogWarning("No XRSocketInteractor on socket: " + match.socket.name);
                return false;
            }

            if (!interactor.hasSelection)
            {
                Debug.Log("Socket has no selected object: " + match.socket.name);
                return false;
            }

            IXRSelectInteractable selectedInteractable = interactor.GetOldestInteractableSelected();
            XRBaseInteractable selected = selectedInteractable as XRBaseInteractable;

            if (selected == null || selected.gameObject != match.correctStatue)
            {
                Debug.Log("Wrong statue in socket: " + match.socket.name);
                return false;
            }
        }
        return true;
    }

    private void OnSceneUnloaded(Scene scene)
    {
        if (loopSource != null && loopSource.isPlaying)
            loopSource.Stop();
    }

    private void OnDestroy()
    {
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }
}
