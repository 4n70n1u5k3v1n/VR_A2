using UnityEngine;

public class SkeletonTrigger : MonoBehaviour
{
    public SkeletonGameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.ActivateSkeletons();
            Debug.Log("Player entered the trigger zone. Skeletons activated.");
            Destroy(gameObject); // Optional: prevent re-triggering
        }
    }
}
