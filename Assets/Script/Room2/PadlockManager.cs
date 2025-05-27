using UnityEngine;
using UnityEngine.UI;

public class PadlockManager : MonoBehaviour
{
    [SerializeField] private LetterSlot[] letterSlots;
    [SerializeField] private string correctCode = "SUTEKH-EM-KA";
    [SerializeField] private GameObject teleportTrigger;
    [SerializeField] private Material activeTeleportMaterial;
    [SerializeField] private GameObject padlockCanvas;

    public void SubmitCode()
    {
        string attempt = "";
        foreach (var slot in letterSlots)
        {
            attempt += slot.GetLetter();
        }

        if (attempt.Equals(correctCode.Replace("-", ""), System.StringComparison.OrdinalIgnoreCase))
        {
            Debug.Log("Correct code entered!");
            if (teleportTrigger != null && activeTeleportMaterial != null)
            {
                teleportTrigger.GetComponent<BoxCollider>().isTrigger = true;
                teleportTrigger.GetComponent<Renderer>().material = activeTeleportMaterial;
                if (padlockCanvas != null)
                {
                    padlockCanvas.SetActive(false);
                }
            }
        }
        else
        {
            Debug.Log("Wrong code. Try again.");
            foreach (var slot in letterSlots)
            {
                slot.FlashRed();
            }
        }
    }
}