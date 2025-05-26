using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileActivates : MonoBehaviour
{
    [SerializeField] private Tile tile;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !gameObject.CompareTag("SafeTile"))
        {
            tile.RaiseNeedles();
        }
    }
}
