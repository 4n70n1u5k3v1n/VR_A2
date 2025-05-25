using UnityEngine;

public class StatueChecker : MonoBehaviour
{
    [System.Serializable]
    public class SocketMatch
    {
        public Transform socket;          // The socket's Transform
        public GameObject correctStatue;  // The exact statue that must be in this socket
    }

    public SocketMatch[] socketMatches;       // Set in Inspector
    public Room1PortalTrigger portalTrigger;  // Reference to your portal

    void Update()
    {
        if (AllStatuesCorrectlyPlaced())
        {
            Debug.Log("All statues correctly placed! Activating portal...");
            portalTrigger.ActivatePortal();
            enabled = false; // Stop checking once done
        }
    }

    bool AllStatuesCorrectlyPlaced()
    {
        foreach (SocketMatch match in socketMatches)
        {
            if (match.socket.childCount == 0) return false;

            Transform placedStatue = match.socket.GetChild(0);
            if (placedStatue.gameObject != match.correctStatue)
                return false;
        }
        return true;
    }
}
