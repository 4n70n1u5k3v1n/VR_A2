using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StatueCollector : MonoBehaviour
{
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable statue;          // Drag your statue GameObject here
    public DoorTrigger2 doorTrigger;           // Drag the GameObject with DoorTrigger2 here

    private bool hasOpenedDoor = false;

    void OnEnable()
    {
        if (statue != null)
            statue.selectEntered.AddListener(OnStatueGrabbed);
    }

    void OnDisable()
    {
        if (statue != null)
            statue.selectEntered.RemoveListener(OnStatueGrabbed);
    }

    private void OnStatueGrabbed(SelectEnterEventArgs args)
    {
        if (!hasOpenedDoor)
        {
            doorTrigger.OnStatuePickedUp();
            hasOpenedDoor = true;
        }
    }
}
