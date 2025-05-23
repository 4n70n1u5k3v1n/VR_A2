using UnityEngine;

public class ScaleDoor : MonoBehaviour
{
    public void OpenDoor()
    {
        // Example: Move the door up
        transform.Translate(Vector3.up * 2f);
    }
}

