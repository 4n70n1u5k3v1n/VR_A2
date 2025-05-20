using TMPro;

using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public TextMeshProUGUI objectiveText;

    public Transform[] statues; // Assign statue objects
    public Transform[] correctPositions; // Assign correct positions

    public float positionTolerance = 0.2f; // Allowable error for placement

    private bool allPlaced = false;

    void Start()
    {
        objectiveText.text = "Objective: Find and place the statue in the correct position";
    }

    void Update()
    {
        if (!allPlaced && AllStatuesPlaced())
        {
            allPlaced = true;
            if (AreStatuesCorrect())
            {
                objectiveText.text = "Correct";
            }
            else
            {
                objectiveText.text = "Wrong";
            }
        }
    }

    bool AllStatuesPlaced()
    {
        foreach (Transform statue in statues)
        {
            if (!statue.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>().isSelected &&
                statue.parent == null) // assumes statues are unparented when placed
            {
                continue;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    bool AreStatuesCorrect()
    {
        for (int i = 0; i < statues.Length; i++)
        {
            float distance = Vector3.Distance(statues[i].position, correctPositions[i].position);
            if (distance > positionTolerance)
                return false;
        }
        return true;
    }
}
