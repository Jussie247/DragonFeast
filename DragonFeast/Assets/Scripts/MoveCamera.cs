using Unity.Mathematics;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] float rotationClamp = 15;
    public GameObject CameraPosition;
    Vector3 offset;
    float rotationYAxis = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = CameraPosition.transform.position + offset;
        float yDot;
        //float normalizedYRotation = CameraPosition.transform.rotation.y - ((CameraPosition.transform.rotation.y % 360) * 360);
        if(CameraPosition.transform.forward.z < 0)
        {
            yDot = Vector3.Dot(CameraPosition.transform.forward, new Vector3(0, 0, -1));
        }
        else
        {
            yDot = Vector3.Dot(CameraPosition.transform.forward, new Vector3(0, 0, 1));
        }

            float angle = Mathf.Acos(yDot);

        //float yRot = Input.GetAxis("Mouse X");
        //rotationYAxis += yRot;
        if(CameraPosition.transform.forward.x < 0)
        {
            angle = -angle;
        }
        
        angle = Mathf.Clamp(angle, -rotationClamp, rotationClamp);
        
        transform.rotation = Quaternion.Euler(0, angle * 2 * math.PI, 0);
        print(angle * 2 * math.PI);
    }
}
