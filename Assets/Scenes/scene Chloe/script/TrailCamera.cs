using UnityEngine;

public class TrailCamera : MonoBehaviour
{
    public Transform target;
    public float Distance = 5.0f;
    public float heightOffset = 3.0f;
    public float cameraDelay = 0.02f;

    void Update()
    {
        Vector3 followPos = target.position - target.forward * Distance;

        followPos.y += heightOffset;
        transform.position += (followPos - transform.position) * cameraDelay;

        transform.LookAt(target.transform);
    }
}
