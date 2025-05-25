using UnityEngine;

public class FogTrigger : MonoBehaviour
{
    public bool enableFog = true;
    public Color fogColor = Color.gray;
    public float fogDensity = 0.03f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RenderSettings.fog = enableFog;
            RenderSettings.fogColor = fogColor;
            RenderSettings.fogMode = FogMode.Exponential;
            RenderSettings.fogDensity = fogDensity;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RenderSettings.fog = false; // or set to default values if you want fog elsewhere
        }
    }
}
