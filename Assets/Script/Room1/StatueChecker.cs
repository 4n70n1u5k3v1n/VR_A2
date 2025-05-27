using UnityEngine;

public class StatueChecker : MonoBehaviour
{
    [System.Serializable]
    public class SocketMatch
    {
        public Transform socket;          // XR Socket Transform
        public GameObject correctStatue;  // Drag the exact statue GameObject here
    }

    public SocketMatch[] socketMatches;       // Assign in Inspector
    public Room1PortalTrigger portalTrigger;  // Drag the portal trigger GameObject here

    private bool portalActivated = false;

    void Update()
    {
        if (!portalActivated && AllStatuesCorrectlyPlaced())
        {
            Debug.Log("All statues correctly placed! Activating portal...");
            portalTrigger.ActivatePortal();
            portalActivated = true;
        }
    }

    bool AllStatuesCorrectlyPlaced()
    {
        foreach (SocketMatch match in socketMatches)
        {
            if (match.socket.childCount == 0)
                return false;

            Transform placed = match.socket.GetChild(0);

            // Compare exact GameObject reference (no naming issues)
            if (placed.gameObject != match.correctStatue)
                return false;
        }
        return true;
    }
}