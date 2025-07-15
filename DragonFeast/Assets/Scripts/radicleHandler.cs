using UnityEngine;

public class radicleHandler : MonoBehaviour
{
    [SerializeField] float mouseSensitivityMultiplier = 200f;

    public Transform playerBody;

    //float xRotation = 0;
    float yRotation = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityMultiplier * UiHandlerScript.sensitivity * Time.deltaTime;
        //float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //xRotation -= mouseY;
        //xRotation = Mathf.Clamp(xRotation, -90, 90);

        yRotation += mouseX;

        playerBody.rotation = Quaternion.Euler(0, yRotation, 0);
        //transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
