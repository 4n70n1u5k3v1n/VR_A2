using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleActivates : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Player>().PlayerDies();
        }
    }
}
