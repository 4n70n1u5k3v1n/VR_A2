using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class StatueTrigger : MonoBehaviour
{
    public bool isCorrectStatue;
    public DoorTrigger doorTrigger;
    public SpiderSpawner spiderSpawner;

    private XRGrabInteractable grabInteractable;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnStatuePickedUp);
    }

    private void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnStatuePickedUp);
    }

    private void OnStatuePickedUp(SelectEnterEventArgs args)
    {
        if (isCorrectStatue)
        {
            doorTrigger.OpenDoor();
        }
        else
        {
            spiderSpawner.SpawnSpiderAtSpawnPoint();
        }
    }
}
