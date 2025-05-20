using UnityEngine;

public class SpiderJumpscare : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float lifetime = 3f;
    private Transform target;

    private void Start()
    {
        GameObject xrOrigin = GameObject.FindGameObjectWithTag("Player");
        if (xrOrigin != null)
        {
            Camera headCamera = xrOrigin.GetComponentInChildren<Camera>();
            if (headCamera != null)
            {
                target = headCamera.transform;
                Debug.Log("Spider target found: " + target.name);
            }
            else
            {
                Debug.LogWarning("Spider jumpscare: No head camera found!");
            }
        }

        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        transform.LookAt(target);
    }
}
