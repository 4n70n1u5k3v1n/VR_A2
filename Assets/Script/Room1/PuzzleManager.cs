using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public LivingStatue[] statues;

    void Update()
    {
        if (AllStatuesInPlace())
        {
            Debug.Log("✅ Puzzle Solved!");
            // You can trigger door open, lights, effects here
            enabled = false; // stop checking
        }
    }

    bool AllStatuesInPlace()
    {
        foreach (var statue in statues)
        {
            if (!statue.IsAtGoal())
                return false;
        }
        return true;
    }
}
