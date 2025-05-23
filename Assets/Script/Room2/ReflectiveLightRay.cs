using UnityEngine;

public class ReflectiveLightRay : MonoBehaviour
{
    public int maxReflections = 7;
    public float maxRayDistance = 100f;
    public LineRenderer lineRenderer;
    public string reflectiveTag = "ReflectiveSurface"; // Assign this tag to your reflective objects
    public float passThroughOffset = 0.01f;

    void Update()
    {
        DrawLightRay(transform.position, transform.forward, maxReflections);
    }

    void DrawLightRay(Vector3 startPos, Vector3 direction, int reflectionsRemaining)
    {
        Vector3 currentPos = startPos;
        Vector3 currentDir = direction;
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, currentPos);

        int segmentIndex = 1;

        while (reflectionsRemaining > 0)
        {
            Ray ray = new Ray(currentPos, currentDir);
            if (Physics.Raycast(ray, out RaycastHit hit, maxRayDistance))
            {
                // Check if the hit object has the reflective tag
                currentPos = hit.point;
                lineRenderer.positionCount = segmentIndex + 1;
                lineRenderer.SetPosition(segmentIndex, currentPos);
                segmentIndex++;

                if (hit.collider.CompareTag(reflectiveTag))
                {
                    currentDir = Vector3.Reflect(currentDir, hit.normal);
                    reflectionsRemaining--;
                }
                else
                {
                    // Go through non-reflective surface: continue in same direction
                    currentPos += currentDir * passThroughOffset;
                }
            }
            else
            {
                currentPos += currentDir * maxRayDistance;
                lineRenderer.positionCount = segmentIndex + 1;
                lineRenderer.SetPosition(segmentIndex, currentPos);
                break;
            }
        }
    }
}
