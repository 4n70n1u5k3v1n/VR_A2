using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public GameObject[] sockets;           // Drag all 9 socket GameObjects here
    public DoorTrigger3 doorController;    // Drag your door GameObject here
    public string correctTag = "PuzzlePiece"; // Tag of correct pieces

    private bool doorOpened = false;

    void Update()
    {
        if (!doorOpened && AllSocketsCorrect())
        {
            doorOpened = true;
            doorController.OpenDoor();
        }
    }

    bool AllSocketsCorrect()
    {
        foreach (var socket in sockets)
        {
            PuzzleSocket puzzleSocket = socket.GetComponent<PuzzleSocket>();
            if (puzzleSocket == null || !puzzleSocket.IsCorrectlyFilled)
                return false;
        }
        return true;
    }

}
