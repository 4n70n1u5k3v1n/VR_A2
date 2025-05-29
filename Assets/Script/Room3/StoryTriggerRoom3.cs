using System.Collections;
using UnityEngine;
using TMPro;

public class StoryTriggerRoom3 : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI storyText; 
    [SerializeField] private AudioClip triggerSound;
    [SerializeField] private GameObject objectiveCanvas;
    private AudioSource audioSource; 
    private bool hasTriggered = false; 

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered) 
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
                    "4.Follow each light"

                };
            }
            else if (gameObject.name == "Trigger4")
            {
                lines = new string[]
                {
                    "5.Find the Way out"
                    

                };
            }
            else if (gameObject.name == "Trigger5")
            {
                lines = new string[]
                {
                    
                    "6.Follow the light"

                };
            }
            else if (gameObject.name == "Trigger6")
            {
                lines = new string[]
                {
                    "7.The Plate will help",
                    "9. Only 1 plate is the key"

                };
            }
            else if (gameObject.name == "Trigger7")
            {
                lines = new string[]
                {
                 
                    "10. Go into the Portal"

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
            if (objectiveCanvas != null)
            {
                objectiveCanvas.SetActive(true);
            }

            hasTriggered = true; // Mark the trigger as activated
        }
    }
}