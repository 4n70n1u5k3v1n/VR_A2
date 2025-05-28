using UnityEngine;
using UnityEngine.AI;

public class WallTrigger : MonoBehaviour
{
    //[Header("Door Settings")]
    public GameObject door;
    public float openedHeight = 3.5f;
    public float speed = 1.0f;

    //[Header("Mummy Chase Settings")]
    public GameObject followerModel; // The mummy object
    public Transform player;
    public float followDistance = 0.2f;

    private Vector3 openedPos;
    private Vector3 closedPos;
    private bool isActivated = false;
    private int direction = -1;
    private float interpolate = 0.0f;
    private bool isTouching = false;

    private bool shouldFollow = false;
    private NavMeshAgent agent;

    private AudioSource mummyGroan;
    public AudioSource triggerSound;        // door opening sound
    void Start()
    {
        // Door position setup
        closedPos = door.transform.position;
        openedPos = door.transform.position;
        openedPos.z -= openedHeight;

        // Mummy agent setup
        if (followerModel != null)
        {
            agent = followerModel.GetComponent<NavMeshAgent>();
            mummyGroan = followerModel.GetComponent<AudioSource>();

            if (mummyGroan != null)
            {
                mummyGroan.loop = true; // Ensure it's set to loop
                mummyGroan.playOnAwake = false; // Don't play until triggered
            }
        }
    }

    void Update()
    {
        // Door movement
        if (isActivated)
        {
            interpolate += direction * speed * Time.deltaTime;
            interpolate = Mathf.Clamp01(interpolate);

            door.transform.position = Vector3.Lerp(closedPos, openedPos, interpolate);

            if (interpolate == 0f || interpolate == 1f)
                isActivated = false;
        }

        // Mummy chase logic
        if (shouldFollow && agent != null && player != null)
        {
            float distanceToPlayer = Vector3.Distance(agent.transform.position, player.position);

            // If mummy is not too close, keep updating destination
            if (distanceToPlayer > followDistance)
            {
                agent.SetDestination(player.position);
                isTouching = false;
            }
            else if (!isTouching)
            {
                // Optional: Face the player even when close
                Vector3 lookDirection = (player.position - agent.transform.position);
                lookDirection.y = 0f; // prevent tilting
                Quaternion targetRotation = Quaternion.Euler(0, agent.transform.eulerAngles.y + 180f, 0);
                if (lookDirection != Vector3.zero)
                    agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, targetRotation, Time.deltaTime * 5f);
                isTouching = true;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActivated = true;
            direction = 1;
            shouldFollow = true;

            if (agent != null)
            {
                agent.isStopped = false;
            }

            if (mummyGroan != null && !mummyGroan.isPlaying)
            {
                mummyGroan.Play();
            }

            if (triggerSound != null && !triggerSound.isPlaying)
            {
                triggerSound.Play();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActivated = true;
            direction = -1;
        }
    }
}
