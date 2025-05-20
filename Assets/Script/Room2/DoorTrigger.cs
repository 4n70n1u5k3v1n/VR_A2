using System.Collections;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Transform door;
    public Transform pressurePlate;
    public float moveDistance = 6f;
    public float moveSpeed = 2f;
    public float platePressDistance = 0.3f;
    public float plateSpeed = 1f;

    private Vector3 doorOriginalPos;
    private Vector3 doorTargetPos;
    private Vector3 plateOriginalPos;
    private Vector3 platePressedPos;

    void Start()
    {
        if (door == null || pressurePlate == null)
        {
            Debug.LogError("Door or pressure plate not assigned!");
            return;
        }

        doorOriginalPos = door.position;
        doorTargetPos = doorOriginalPos + Vector3.up * moveDistance;

        plateOriginalPos = pressurePlate.position;
        platePressedPos = plateOriginalPos - Vector3.up * platePressDistance;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(MoveDoor(doorTargetPos));
            StartCoroutine(MovePlate(platePressedPos));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(MoveDoor(doorOriginalPos));
            StartCoroutine(MovePlate(plateOriginalPos));
        }
    }

    private IEnumerator MoveDoor(Vector3 target)
    {
        while (Vector3.Distance(door.position, target) > 0.01f)
        {
            door.position = Vector3.MoveTowards(door.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }
        door.position = target;
    }

    private IEnumerator MovePlate(Vector3 target)
    {
        while (Vector3.Distance(pressurePlate.position, target) > 0.001f)
        {
            pressurePlate.position = Vector3.MoveTowards(pressurePlate.position, target, plateSpeed * Time.deltaTime);
            yield return null;
        }
        pressurePlate.position = target;
    }
}
