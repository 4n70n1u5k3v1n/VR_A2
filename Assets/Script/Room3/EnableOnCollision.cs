using UnityEngine;

public class EnableOnCollision : MonoBehaviour
{
    [SerializeField] private GameObject targetObject; // Drag the inactive GameObject here
    [SerializeField] private string triggeringTag = "Player"; // Tag for the object that should trigger

    private bool hasActivated = false;

    void OnTriggerEnter(Collider other)
    {
        if (!hasActivated && other.CompareTag(triggeringTag))
        {
            if (targetObject != null)
            {
                targetObject.SetActive(true);
                hasActivated = true;
                Debug.Log("Target GameObject enabled!");
            }
            else
            {
                Debug.LogWarning("Target GameObject reference is missing.");
            }
        }
    }
}