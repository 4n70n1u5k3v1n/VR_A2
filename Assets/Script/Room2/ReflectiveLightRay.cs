using UnityEngine;

public class ReflectiveLightRay : MonoBehaviour
{
    [SerializeField] private int maxReflections = 7;
    [SerializeField] private float maxRayDistance = 100f;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private string reflectiveTag = "ReflectiveSurface";
    [SerializeField] private string receiverTag = "LightReceiver";
    [SerializeField] private float passThroughOffset = 0.01f;

    [Header("Highlighting")]
    [SerializeField] private Material correctTileMaterial;
    [SerializeField] private GameObject[] correctTiles;
    [SerializeField] private Material lightReceiverMaterial;
    [SerializeField] private GameObject lightReceiver;

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
                else if (hit.collider.CompareTag(receiverTag))
                {
                    Renderer receiverRenderer = hit.collider.GetComponent<Renderer>();
                    if (receiverRenderer != null && correctTileMaterial != null)
                        receiverRenderer.material = correctTileMaterial;

                    foreach (GameObject tile in correctTiles)
                    {
                        if (tile != null)
                        {
                            Renderer rend = tile.GetComponent<Renderer>();
                            if (rend != null)
                            {
                                rend.material = correctTileMaterial;
                            }
                        }
                    }

                    Renderer renderer = lightReceiver.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.material = lightReceiverMaterial;
                    }

                    break;
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
