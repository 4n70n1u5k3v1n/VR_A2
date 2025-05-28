using UnityEngine;

public class SpiderSpawner : MonoBehaviour
{
    public GameObject spiderPrefab;
    public AudioClip spiderSound;
    public Transform[] spawnPoints;  // Assign in Inspector

    public void SpawnSpiderAtSpawnPoint()
    {
        if (spiderPrefab == null)
        {
            Debug.LogWarning("Spider prefab not set.");
            return;
        }

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points assigned.");
            return;
        }

        // Pick a random spawn point
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Camera mainCam = Camera.main;
        if (mainCam == null)
        {
            Debug.LogWarning("Main Camera not found.");
            return;
        }

        // Face toward the camera (player)
        Vector3 directionToCamera = mainCam.transform.position - spawnPoint.position;
        Quaternion lookRotation = Quaternion.LookRotation(directionToCamera);

        // Instantiate the spider
        GameObject spider = Instantiate(spiderPrefab, spawnPoint.position, lookRotation);

        // Add and play sound if AudioClip assigned
        if (spiderSound != null)
        {
            AudioSource audio = spider.AddComponent<AudioSource>();
            audio.spatialBlend = 1.0f;
            audio.clip = spiderSound;
            audio.playOnAwake = false;
            audio.loop = false;
            audio.Play();
        }

        // Auto destroy spider after 4 seconds
        Destroy(spider, 4f);
    }
}
