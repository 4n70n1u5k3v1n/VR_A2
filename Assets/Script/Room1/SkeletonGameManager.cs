using UnityEngine;

public class SkeletonGameManager : MonoBehaviour
{
    public Camera playerCamera;
    public float detectionRange = 100f;
    private SkeletonBehavior[] skeletons;

    public GameObject statuePrefab;      // Assign your statue prefab in the Inspector
    public Transform statueSpawnPoint;   // Assign the altar or spawn point in the Inspector

    private bool skeletonsActivated = false;
    private bool winTriggered = false;

    void Start()
    {
        skeletons = FindObjectsOfType<SkeletonBehavior>();
    }

    void Update()
    {
        if (!skeletonsActivated || winTriggered) return;

        foreach (SkeletonBehavior skel in skeletons)
        {
            skel.SetSeen(false);
        }

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * detectionRange, Color.red);

        if (Physics.Raycast(ray, out hit, detectionRange))
        {
            if (hit.collider.CompareTag("Skeleton"))
            {
                SkeletonBehavior seenSkeleton = hit.collider.GetComponent<SkeletonBehavior>();
                if (seenSkeleton != null)
                {
                    seenSkeleton.SetSeen(true);
                }
            }
        }

        bool allReached = true;
        foreach (SkeletonBehavior skel in skeletons)
        {
            if (!skel.HasReachedGoal())
            {
                allReached = false;
                break;
            }
        }

        if (allReached)
        {
            winTriggered = true; // Ensure this runs once
            Debug.Log("All skeletons reached the center! You win!");
            StartCoroutine(HandleWinSequence());
        }
    }

    public void ActivateSkeletons()
    {
        skeletonsActivated = true;
        foreach (SkeletonBehavior skel in skeletons)
        {
            skel.Activate();
        }
    }

    private System.Collections.IEnumerator HandleWinSequence()
    {
        yield return new WaitForSeconds(3f);

        // Destroy skeletons
        foreach (SkeletonBehavior skel in skeletons)
        {
            Destroy(skel.gameObject);
        }

        // Spawn the real statue
        if (statuePrefab != null && statueSpawnPoint != null)
        {
            Instantiate(statuePrefab, statueSpawnPoint.position, statueSpawnPoint.rotation);
        }

        Debug.Log("Real statue has appeared!");
    }
}
