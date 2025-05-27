using UnityEngine;

public class RoadGuide : MonoBehaviour
{
    public float maxDistance = 20f; // Maximum distance for the raycast
    private LineRenderer lineRenderer;

    void Start()
    {
        // Get or create the LineRenderer component
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        // Customize LineRenderer appearance
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.positionCount = 2;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
    }

    void Update()
    {
        // Starting point of the ray
        Vector3 startPoint = transform.position;

        // Direction of the ray: usually forward
        Vector3 direction = transform.forward;

        // Perform raycast
        RaycastHit hit;
        if (Physics.Raycast(startPoint, direction, out hit, maxDistance))
        {
            // If ray hits an object, set the LineRenderer endpoint to the hit point
            lineRenderer.SetPosition(0, startPoint);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            // If no hit, set the LineRenderer endpoint to max distance
            lineRenderer.SetPosition(0, startPoint);
            lineRenderer.SetPosition(1, startPoint + direction * maxDistance);
        }

        // Optional: Draw the ray in the scene view for debugging
        Debug.DrawRay(startPoint, direction * maxDistance, Color.green);
    }

}
