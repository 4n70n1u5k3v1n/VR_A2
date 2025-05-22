using System.Collections.Generic;
using UnityEngine;

public class ScaleSystem : MonoBehaviour
{
    public float targetMass = 10f; // Target mass to open the door
    private List<Rigidbody> objectsOnScale = new List<Rigidbody>();

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null && !objectsOnScale.Contains(rb))
        {
            objectsOnScale.Add(rb);
            CheckTotalMass();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null && objectsOnScale.Contains(rb))
        {
            objectsOnScale.Remove(rb);
            CheckTotalMass();
        }
    }

    private void CheckTotalMass()
    {
        float totalMass = 0f;
        foreach (Rigidbody rb in objectsOnScale)
        {
            totalMass += rb.mass;
        }

        if (totalMass == targetMass)
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        // Add code to open the door (e.g., move the door, play an animation, etc.)
        Debug.Log("Door Opened!");
    }
}