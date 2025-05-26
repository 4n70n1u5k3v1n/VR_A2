using System.Collections;
using UnityEngine;
using TMPro;

public class StoryTriggerRoom3 : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI storyText; // Reference to the UI text element
    [SerializeField] private AudioClip triggerSound; // Sound to play when the trigger is activated
    private AudioSource audioSource; // AudioSource component to play the sound
    private bool hasTriggered = false; // Flag to track if the trigger has been activated

    private void Start()
    {
        // Get or add an AudioSource component to this GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered) // Check if the collider is the player and the trigger hasn't been activated
        {
            string[] lines;

            // Customize lines based on the trigger's name
            if (gameObject.name == "Trigger1")
            {
                lines = new string[]
                {
                    "1.Follow Shining ball",
                  
                };
            }
            else if (gameObject.name == "Trigger2")
            {
                lines = new string[]
                {
                 "2.Listen to The Skeleton"
                    
                };
            }
            else if (gameObject.name == "Trigger3")
            {
                lines = new string[]
                {
                    "3.Find the Objects",
                    "4.Follow each light to find the object"

                };
            }
            else if (gameObject.name == "Trigger4")
            {
                lines = new string[]
                {
                    "5.Find the Way out",
                    "6.The Plate will help you to out"

                };
            }
            else
            {
                lines = new string[] { "------------------------------" }; // Fallback for unknown triggers
            }

            // Append each line to the storyText
            foreach (string line in lines)
            {
                storyText.text += "\n" + line; // Add a new line and the text
            }

            // Play the sound if it's assigned
            if (triggerSound != null)
            {
                audioSource.PlayOneShot(triggerSound);
            }

            hasTriggered = true; // Mark the trigger as activated
        }
    }
}