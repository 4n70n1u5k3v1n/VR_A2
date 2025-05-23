using UnityEngine;

public class LivingStatue : MonoBehaviour
{
    public StatueWatcher watcher;
    public Transform[] movementPoints;
    public float moveSpeed = 1.5f;

    private int currentTarget = 0;
    private bool isLocked = false;

    void Update()
    {
        if (isLocked || watcher.isBeingWatched) return;

        MoveToNextPoint();
    }

    void MoveToNextPoint()
    {
        if (currentTarget >= movementPoints.Length)
        {
            isLocked = true;
            return;
        }

        Transform target = movementPoints[currentTarget];
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentTarget++;
        }
    }

    public bool IsAtGoal()
    {
        if (movementPoints.Length == 0) return false;
        return Vector3.Distance(transform.position, movementPoints[movementPoints.Length - 1].position) < 0.1f;
    }
}
