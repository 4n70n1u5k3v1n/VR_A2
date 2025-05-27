//using UnityEngine;

//public class FinalDoorTrigger : MonoBehaviour
//{
//    public GameObject door = null;      // door
//    public float openedHeight = 3.5f;   // opened door height
//    public float speed = 1.0f;          // door opening speed

//    Vector3 openedPos = Vector3.zero;   // position of the opened door
//    Vector3 closedPos = Vector3.zero;   // position of the closed door
//    bool isActivated = false;           // whether the door is activated
//    int direction = -1;                 // opening or closing
//    float interpolate = 0.0f;           // interpolation amount

//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {
//        closedPos = door.transform.position;    // set closed position to current position

//        openedPos = door.transform.position;    // set opened position based on height variable
//        openedPos.z -= openedHeight;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if(isActivated)
//        {
//            // calculate the interpolation amount
//            interpolate += direction * speed * Time.deltaTime;

//            // if door completely opened
//            if (interpolate > 1.0f)
//            {
//                interpolate = 1.0f;
//                isActivated = false;
//            }
//            // if door completely closed
//            else if (interpolate < 0.0f)
//            {
//                interpolate = 0.0f;
//                isActivated = false;
//            }

//            // interpolate between open and closed positions
//            door.transform.position = Vector3.Lerp(closedPos, openedPos, interpolate);
//        }
//    }

//    // called when a GameObject collides with the collider
//    void OnTriggerEnter(Collider other)
//    {
//        //GameObject.FindGameObjectWithTag("DirectionalLight").SetActive(false);
//        // activate the door and set direction to open
//        isActivated = true;
//        direction = 1;
//    }

//    // called when a GameObject exits the collider
//    void OnTriggerExit(Collider other)
//    {
//        // activate the door and set direction to close
//        isActivated = true;
//        direction = -1;
//    }
//}

//using UnityEngine;

//public class FinalDoorTrigger : MonoBehaviour
//{
//    public GameObject door = null;
//    public float openedHeight = 3.5f;
//    public float speed = 1.0f;

//    public GameObject followerModel;
//    public Transform player;
//    public float followSpeed = 2.0f;
//    public float followDistance = 1.5f;

//    private Vector3 openedPos = Vector3.zero;
//    private Vector3 closedPos = Vector3.zero;
//    private bool isActivated = false;
//    private int direction = -1;
//    private float interpolate = 0.0f;

//    private bool shouldFollow = false;

//    void Start()
//    {
//        closedPos = door.transform.position;
//        openedPos = door.transform.position;
//        openedPos.z -= openedHeight;
//    }

//    void Update()
//    {
//        if (isActivated)
//        {
//            interpolate += direction * speed * Time.deltaTime;
//            interpolate = Mathf.Clamp01(interpolate);

//            door.transform.position = Vector3.Lerp(closedPos, openedPos, interpolate);

//            if (interpolate == 0f || interpolate == 1f)
//                isActivated = false;
//        }

//        //if (shouldFollow && followerModel != null && player != null)
//        //{
//        //    float distance = Vector3.Distance(followerModel.transform.position, player.position);
//        //    if (distance > followDistance)
//        //    {
//        //        Vector3 directionToPlayer = (player.position - followerModel.transform.position).normalized;
//        //        followerModel.transform.position += directionToPlayer * followSpeed * Time.deltaTime;

//        //        // Optional: face the player
//        //        followerModel.transform.LookAt(new Vector3(player.position.x, followerModel.transform.position.y, player.position.z));
//        //    }
//        //}
//        if (shouldFollow && followerModel != null && player != null)
//        {
//            Vector3 directionToPlayer = (player.position - followerModel.transform.position).normalized;
//            followerModel.transform.position += directionToPlayer * followSpeed * Time.deltaTime;

//            // Optional: face the player
//            followerModel.transform.LookAt(new Vector3(player.position.x, followerModel.transform.position.y, player.position.z));
//        }

//    }

//    void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Player")) // Make sure XR Origin has the "Player" tag
//        {
//            isActivated = true;
//            direction = 1;
//            shouldFollow = true;
//        }
//    }

//    void OnTriggerExit(Collider other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            isActivated = true;
//            direction = -1;
//        }
//    }
//}

using UnityEngine;
using UnityEngine.AI;

public class FinalDoorTrigger : MonoBehaviour
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
        //if (shouldFollow && agent != null && player != null)
        //{
        //    agent.SetDestination(player.position);
        //}
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
