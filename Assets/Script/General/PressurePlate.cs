using System.Collections;
using TMPro;
using UnityEngine;

public class PressurePlate: MonoBehaviour
{
    [SerializeField] private Transform door;
    [SerializeField] private Transform pressurePlate;
    [SerializeField] private ParticleSystem[] sandParticles;
    [SerializeField] private float moveDistance = 6f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float platePressDistance = 0.3f;
    [SerializeField] private float plateSpeed = 1f;
    [SerializeField] private AudioSource doorOpenSFX;
    [SerializeField] private TextMeshProUGUI storyText;
    [SerializeField] private AudioSource voiceline2;
    [SerializeField] private GameObject missionCanvas;

    private Vector3 doorOriginalPos;
    private Vector3 doorTargetPos;
    private Vector3 plateOriginalPos;
    private Vector3 platePressedPos;
    private bool firstTime = true;
    private int objectNumber = 0;

    void Start()
    {
        if (door == null || pressurePlate == null)
        {
            Debug.LogError("Door or pressure plate not assigned!");
            return;
        }

        doorOriginalPos = door.position;
        doorTargetPos = doorOriginalPos + Vector3.up * moveDistance;

        plateOriginalPos = pressurePlate.position;
        platePressedPos = plateOriginalPos - Vector3.up * platePressDistance;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!firstTime && (other.CompareTag("Player") || other.gameObject.name.Equals("Mirror")))
        {
            objectNumber++;
            StopAllCoroutines();
            StartCoroutine(MoveDoor(doorTargetPos));
            StartCoroutine(MovePlate(platePressedPos));
        }
        else if (firstTime && (other.CompareTag("Player") || other.gameObject.name.Equals("Mirror")))
        {
            objectNumber++;
            firstTime = false;
            StartCoroutine(AddMission());
            StartCoroutine(MoveDoor(doorTargetPos));
            StartCoroutine(MovePlate(platePressedPos));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.gameObject.name.Equals("Mirror"))
        {
            objectNumber--;
            if (objectNumber <= 0)
            {
                StopAllCoroutines();
                StartCoroutine(MoveDoor(doorOriginalPos));
                StartCoroutine(MovePlate(plateOriginalPos));
            }
        }
    }

    private IEnumerator MoveDoor(Vector3 target)
    {
        if (doorOpenSFX != null)
        {
            doorOpenSFX.Play();
            doorOpenSFX.loop = true;
        }
        while (Vector3.Distance(door.position, target) > 0.01f)
        {
            door.position = Vector3.MoveTowards(door.position, target, moveSpeed * Time.deltaTime);
            if (sandParticles.Length>0)
            {
                foreach (ParticleSystem particleSystem in sandParticles)
                {
                    particleSystem.Play();
                }
            }
            yield return null;
        }

        if (doorOpenSFX != null)
        {
            doorOpenSFX.Stop();
        }

        if (sandParticles.Length > 0)
        {
            foreach (ParticleSystem particleSystem in sandParticles)
            {
                particleSystem.Pause();
                particleSystem.Clear();
            }
        }
        door.position = target;
    }

    private IEnumerator MovePlate(Vector3 target)
    {
        while (Vector3.Distance(pressurePlate.position, target) > 0.001f)
        {
            pressurePlate.position = Vector3.MoveTowards(pressurePlate.position, target, plateSpeed * Time.deltaTime);
            yield return null;
        }
        pressurePlate.position = target;
    }

    private IEnumerator AddMission()
    {
        if (voiceline2 != null)
        {
            voiceline2.Play();
        }
        

        yield return new WaitForSeconds(7f);

        storyText.text += "\n" + "# Find a Heavy Object";
        
        if (missionCanvas != null)
        {
            missionCanvas.GetComponent<InputHandler>().ForceOpenMissionSheet();
        }
    }
}
