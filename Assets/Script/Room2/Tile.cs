using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject[] needleParents;
    [SerializeField] private float downDistance = 0.8f;
    [SerializeField] private float moveDuration = 0.1f;

    private List<Transform> needleTransforms = new List<Transform>();
    private List<Vector3> upPositions = new List<Vector3>();
    private bool isMoving = false;

    void Start()
    {
        if (needleParents != null && needleParents.Length > 0)
        {
            foreach (GameObject parent in needleParents)
            {
                if (parent != null)
                {
                    foreach (Transform child in parent.transform)
                    {
                        needleTransforms.Add(child);
                        upPositions.Add(child.position);
                    }
                }
            }
            LowerNeedles();
        }
        else
        {
            Debug.LogWarning("No needle parents found.");
        }
    }

    public void LowerNeedles()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveNeedles(down: true));
        }
    }

    public void RaiseNeedles()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveNeedles(down: false));
        }
    }

    private IEnumerator MoveNeedles(bool down)
    {
        isMoving = true;
        float elapsed = 0f;

        Vector3[] startPositions = new Vector3[needleTransforms.Count];
        Vector3[] endPositions = new Vector3[needleTransforms.Count];

        for (int i = 0; i < needleTransforms.Count; i++)
        {
            startPositions[i] = needleTransforms[i].position;
            Vector3 offset = new Vector3(0, -downDistance, 0);
            endPositions[i] = down ? upPositions[i] + offset : upPositions[i];
        }

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / moveDuration);

            for (int i = 0; i < needleTransforms.Count; i++)
            {
                needleTransforms[i].position = Vector3.Lerp(startPositions[i], endPositions[i], t);
            }

            yield return null;
        }

        for (int i = 0; i < needleTransforms.Count; i++)
        {
            needleTransforms[i].position = endPositions[i];
        }

        isMoving = false;
    }
}
