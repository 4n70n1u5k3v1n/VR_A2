using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class StatueChecker : MonoBehaviour
{
    [System.Serializable]
    public class SocketMatch
    {
        public Transform socket;          // XR Socket Transform - drag here in Inspector
        public GameObject correctStatue;  // The correct statue GameObject to check against
    }

    public SocketMatch[] socketMatches;       // Assign sockets and statues in Inspector
    public Room1PortalTrigger portalTrigger;  // Assign your portal trigger script here
    public AudioClip successClip;              // Assign success sound clip here

    private AudioSource audioSource;
    private bool portalActivated = false;

    void Start()
    {
        // Add AudioSource component for playing success sound
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (!portalActivated && AllStatuesCorrectlyPlaced())
        {
            Debug.Log("All statues correctly placed! Activating portal...");

            if (successClip != null)
            {
                audioSource.PlayOneShot(successClip);
            }

            if (portalTrigger != null)
            {
                portalTrigger.ActivatePortal();
            }

            portalActivated = true;
        }
    }

    bool AllStatuesCorrectlyPlaced()
    {
        foreach (SocketMatch match in socketMatches)
        {
            // Get the XRSocketInteractor component on the socket transform
            XRSocketInteractor interactor = match.socket.GetComponent<XRSocketInteractor>();

            if (interactor == null)
            {
                Debug.LogWarning("No XRSocketInteractor on socket: " + match.socket.name);
                return false;
            }

            // Check if socket has a selected object
            if (!interactor.hasSelection)
            {
                Debug.Log("Socket has no selected object: " + match.socket.name);
                return false;
            }

            // Get the selected interactable and cast it to XRBaseInteractable
            IXRSelectInteractable selectedInteractable = interactor.GetOldestInteractableSelected();
            XRBaseInteractable selected = selectedInteractable as XRBaseInteractable;

            if (selected == null)
            {
                Debug.LogWarning("Selected interactable is not an XRBaseInteractable on socket: " + match.socket.name);
                return false;
            }

            // Check if the selected GameObject matches the correct statue
            if (selected.gameObject != match.correctStatue)
            {
                Debug.Log("Wrong statue in socket: " + match.socket.name);
                return false;
            }
        }
        return true;
    }
}
