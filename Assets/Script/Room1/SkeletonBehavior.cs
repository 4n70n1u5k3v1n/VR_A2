using UnityEngine;

public class SkeletonBehavior : MonoBehaviour
{
    public Transform targetPoint;
    public float moveSpeed = 0.5f;
    public float stopDistance = 0.5f;

    public AudioClip boneClip;
    private AudioSource boneAudio;

    private bool isSeen = false;
    private bool hasReachedGoal = false;
    private bool isActivated = false; // <-- NEW FLAG

    void Start()
    {
        boneAudio = gameObject.AddComponent<AudioSource>();
        boneAudio.clip = boneClip;
        boneAudio.playOnAwake = false;
        boneAudio.spatialBlend = 1.0f; // Makes the sound fully 3D
    }

    void Update()
    {
        if (!isActivated || hasReachedGoal) return; // <-- Only move when activated and not done

        if (!isSeen)
        {
            MoveTowardsTarget();
        }

        if (Vector3.Distance(transform.position, targetPoint.position) < stopDistance)
        {
            hasReachedGoal = true;
            if (boneAudio.isPlaying)
                boneAudio.Stop();
        }
    }

    void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);

        if (!boneAudio.isPlaying)
            boneAudio.Play();
    }

    public void SetSeen(bool seen)
    {
        isSeen = seen;
        if (seen && boneAudio.isPlaying)
            boneAudio.Stop();
    }

    public bool HasReachedGoal()
    {
        return hasReachedGoal;
    }

    public void Activate()
    {
        isActivated = true; // <-- Called when GameManager triggers it
    }
}
