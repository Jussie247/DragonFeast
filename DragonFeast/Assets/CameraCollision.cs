using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public Transform target;           // Player
    public Transform cameraPivot;      // Pivot point
    public float distance = 5f;        // Desired camera distance
    public float smoothSpeed = 10f;    // Smooth movement
    public LayerMask collisionMask;    // Layers to collide with

    private Vector3 currentVelocity;

    private void Start()
    {
        target = GameObject.Find("CameraPosition").transform;
        cameraPivot = GameObject.Find("CameraOffset").transform;
    }

    void LateUpdate()
    {
        Vector3 desiredCameraPos = cameraPivot.position - cameraPivot.forward * distance;

        float sphereRadius = 0.3f;
        if (Physics.SphereCast(cameraPivot.position, sphereRadius, -cameraPivot.forward, out RaycastHit hit, distance, collisionMask))
        {
            desiredCameraPos = cameraPivot.position - cameraPivot.forward * (hit.distance - 0.1f); // maintain a little distance
        }

        // Smoothly move the camera
        transform.position = Vector3.SmoothDamp(transform.position, desiredCameraPos, ref currentVelocity, 0.05f);
        transform.LookAt(cameraPivot.position);
    }
}
