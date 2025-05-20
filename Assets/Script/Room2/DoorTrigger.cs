using System.Collections;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Transform door;
    public float moveDistance = 6f;
    public float moveSpeed = 2f;

    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private bool isMoving = false;

    void Start()
    {
        if (door == null)
        {
            Debug.LogError("Door is not assigned!");
            return;
        }

        originalPosition = door.position;
        targetPosition = originalPosition + Vector3.up * moveDistance;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(MoveDoorUp());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(MoveDoorDown());
        }
    }

    private IEnumerator MoveDoorUp()
    {
        while (Vector3.Distance(door.position, targetPosition) > 0.01f)
        {
            door.position = Vector3.MoveTowards(door.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        door.position = targetPosition;
        isMoving = false;
    }

    private IEnumerator MoveDoorDown()
    {
        while (Vector3.Distance(door.position, originalPosition) > 0.01f)
        {
            door.position = Vector3.MoveTowards(door.position, originalPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        door.position = originalPosition;
        isMoving = false;
    }
}
