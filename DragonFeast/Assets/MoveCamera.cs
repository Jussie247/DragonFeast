using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public GameObject CameraPosition;
    Vector3 offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = CameraPosition.transform.position + offset;
    }
}
