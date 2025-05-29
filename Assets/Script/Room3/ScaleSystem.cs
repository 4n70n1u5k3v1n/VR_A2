using System.Collections.Generic;
using UnityEngine;

public class ScaleSystem : MonoBehaviour
{
    public AudioSource audioSource;  // Assign in Inspector or add dynamically
    public string playerTag = "Player";  // Tag of the player object

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            audioSource.Play();
        }
    }
}