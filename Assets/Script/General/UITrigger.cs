using UnityEngine;

public class UITrigger : MonoBehaviour
{
    public QuitHandler quitHandler;
    public AudioSource triggerSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && quitHandler != null)
        {
            quitHandler.ShowMenu();

            if (triggerSound != null && !triggerSound.isPlaying)
            {
                triggerSound.Play();
            }
        }
    }
}
