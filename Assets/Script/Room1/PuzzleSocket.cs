using UnityEngine;

public class PuzzleSocket : MonoBehaviour
{
    public GameObject correctPiece;
    public bool IsCorrectlyFilled { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == correctPiece)
        {
            IsCorrectlyFilled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == correctPiece)
        {
            IsCorrectlyFilled = false;
        }
    }
}
