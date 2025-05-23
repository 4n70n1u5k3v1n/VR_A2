using UnityEngine;

public class StatueWatcher : MonoBehaviour
{
    public Transform playerHead;
    public float viewAngle = 60f;
    public bool isBeingWatched = false;

    void Update()
    {
        Vector3 toPlayer = playerHead.position - transform.position;
        float angle = Vector3.Angle(transform.forward, toPlayer);

        isBeingWatched = angle < viewAngle && IsInSight();
    }

    bool IsInSight()
    {
        Vector3 direction = playerHead.position - transform.position;
        Ray ray = new Ray(transform.position, direction);
        if (Physics.Raycast(ray, out RaycastHit hit, direction.magnitude))
        {
            return hit.transform.CompareTag("MainCamera");
        }
        return false;
    }
}
