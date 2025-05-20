using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class StatueTrigger : MonoBehaviour
{
    [Header("Statue Type")]
    public bool isCorrectStatue = false;

    [Header("Spider Jumpscare (for fake statue)")]
    public GameObject spiderPrefab;
    public float spiderDistanceFromHead = 0.5f;
    public float spiderLifetime = 3f;

    [Header("Door Control (for correct statue)")]
    public Transform door;
    public Vector3 doorOpenOffset = new Vector3(0, 0, 2); // How far the door should slide
    public float doorOpenSpeed = 2f;

    private Transform playerHead;
    private bool hasTriggered = false;

    private void Start()
    {
        // ✅ Find the headset camera under the XR Origin tagged as "Player"
        GameObject xrOrigin = GameObject.FindGameObjectWithTag("Player");
        if (xrOrigin != null)
        {
            Camera headCamera = xrOrigin.GetComponentInChildren<Camera>();
            if (headCamera != null)
            {
                playerHead = headCamera.transform;
            }
        }

        if (playerHead == null)
        {
            Debug.LogWarning("Player head camera not found! Make sure XR Origin is tagged as 'Player' and has a Camera child.");
        }

        // Subscribe to grab event
        var grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnStatueGrabbed);
        }
        else
        {
            Debug.LogWarning("XRGrabInteractable not found on this statue.");
        }
    }

    private void OnStatueGrabbed(SelectEnterEventArgs args)
    {
        if (hasTriggered) return;
        hasTriggered = true;

        if (isCorrectStatue)
        {
            OpenDoor();
        }
        else
        {
            TriggerSpiderJumpscare();
        }

        // Optional: destroy statue after interaction
        // Destroy(gameObject);
    }

    private void TriggerSpiderJumpscare()
    {
        if (spiderPrefab == null || playerHead == null) return;

        // Spawn spider in front of headset
        Vector3 spawnPosition = playerHead.position + playerHead.forward * spiderDistanceFromHead;
        Quaternion spawnRotation = Quaternion.LookRotation(playerHead.forward);

        GameObject spider = Instantiate(spiderPrefab, spawnPosition, spawnRotation);
        Destroy(spider, spiderLifetime);
    }

    private void OpenDoor()
    {
        if (door == null) return;
        StartCoroutine(SlideDoorOpen());
    }

    private System.Collections.IEnumerator SlideDoorOpen()
    {
        Vector3 startPos = door.position;
        Vector3 targetPos = startPos + doorOpenOffset;
        float elapsed = 0f;

        while (elapsed < 1f)
        {
            door.position = Vector3.Lerp(startPos, targetPos, elapsed);
            elapsed += Time.deltaTime * doorOpenSpeed;
            yield return null;
        }

        door.position = targetPos;
    }
}
