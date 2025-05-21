using UnityEngine;

public class ReflectiveLightRay : MonoBehaviour
{
    public int maxReflections = 5;
    public float maxRayDistance = 100f;
    public LineRenderer lineRenderer;
    public LayerMask reflectiveLayers;

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
            if (Physics.Raycast(ray, out RaycastHit hit, maxRayDistance, reflectiveLayers))
            {
                currentPos = hit.point;
                lineRenderer.positionCount = segmentIndex + 1;
                lineRenderer.SetPosition(segmentIndex, currentPos);

                currentDir = Vector3.Reflect(currentDir, hit.normal);
                reflectionsRemaining--;

                segmentIndex++;
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
