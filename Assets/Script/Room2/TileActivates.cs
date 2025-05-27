using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileActivates : MonoBehaviour
{
    [SerializeField] private Tile tile;
    [SerializeField] private GameObject NeedleTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ActivateTile());
        }
    }

    private IEnumerator ActivateTile()
    {
        if (tile != null)
        {
            tile.RaiseNeedles();
            if (NeedleTrigger != null)
            {
                NeedleTrigger.SetActive(true);
            }
        }

        yield return new WaitForSeconds(2f);

        if (tile != null)
        {
            tile.LowerNeedles();
            if (NeedleTrigger != null)
            {
                NeedleTrigger.SetActive(false);
            }
        }
    }
}
