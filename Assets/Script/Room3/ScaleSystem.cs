using System.Collections.Generic;
using UnityEngine;

public class ScaleSystem : MonoBehaviour
{
    public float targetMass = 10f; // The mass needed to open the door
    private float currentMass = 0f; // Tracks the current mass on the scale

    void OnTriggerEnter(Collider other)
    {
        // Check if the object has a Rigidbody, and add its mass to the currentMass
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            currentMass += rb.mass;

            // Check if the currentMass meets or exceeds the targetMass
            if (currentMass >= targetMass)
            {
                OpenDoor();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Subtract the mass of the object that's leaving the scale
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            currentMass -= rb.mass;
        }
    }

    void OpenDoor()
    {
        // Logic to open the door
        Debug.Log("Door Opens!");
        // Insert the functionality to open the door in your VR environment
    }
}