using UnityEngine;

public class SkeletonGameManager : MonoBehaviour
{
    public Camera playerCamera;
    public float detectionRange = 100f;
    private SkeletonBehavior[] skeletons;

    public GameObject realStatue;        // Drag the real (disabled) statue GameObject from the scene here

    private bool skeletonsActivated = false;
    private bool winTriggered = false;

    void Start()
    {
        skeletons = FindObjectsOfType<SkeletonBehavior>();

        // Just to be safe, ensure the statue is hidden at the start
        if (realStatue != null)
        {
            realStatue.SetActive(false);
        }
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
            winTriggered = true;
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
        yield return new WaitForSeconds(1f);

        // Destroy skeletons
        foreach (SkeletonBehavior skel in skeletons)
        {
            Destroy(skel.gameObject);
        }

        // Activate the hidden statue
        if (realStatue != null)
        {
            realStatue.SetActive(true);
        }

        Debug.Log("Real statue has appeared!");
    }
}
