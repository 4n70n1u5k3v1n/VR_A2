using UnityEngine;

public class DisableOnCollision : MonoBehaviour
{
    [SerializeField] private string targetObjectName;

    private bool hasDisabled = false;

    void OnCollisionEnter(Collision collision)
    {
        TryDisableTarget();
    }

    // Use this if using trigger colliders
    void OnTriggerEnter(Collider other)
    {
        TryDisableTarget();
    }

    private void TryDisableTarget()
    {
        if (hasDisabled) return;

        GameObject target = GameObject.Find(targetObjectName);
        if (target != null)
        {
            target.SetActive(false);
            hasDisabled = true;
            Debug.Log($"Disabled GameObject: {targetObjectName}");
        }
        else
        {
            Debug.LogWarning($"GameObject with name '{targetObjectName}' not found.");
        }
    }
}
