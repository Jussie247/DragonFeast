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

        // Raycast to detect collision
        if (Physics.Raycast(cameraPivot.position, -cameraPivot.forward, out RaycastHit hit, distance, collisionMask))
        {
            // If something is hit, move the camera to the hit point (with a small offset)
            desiredCameraPos = hit.point + cameraPivot.forward * 0.2f;  // Prevent clipping into the wall
        }

        // Smoothly move the camera
        transform.position = Vector3.SmoothDamp(transform.position, desiredCameraPos, ref currentVelocity, 0.05f);
        transform.LookAt(cameraPivot.position);
    }
}
