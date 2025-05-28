using UnityEngine;

public class PlateFall : MonoBehaviour
{
    [SerializeField] private AudioSource objectFallSFX;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Tile"))
        {
            if (objectFallSFX != null)
            {
                objectFallSFX.Play();
            }
        }
    }
}
