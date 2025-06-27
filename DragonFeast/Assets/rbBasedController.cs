using Unity.VisualScripting;
using UnityEngine;

public class rbBasedController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private Rigidbody rb;

    public Transform groundCheck;
    public LayerMask GroundMask;
    public float groundDistance;
    public float groundDrag;

    public GameObject canvas;

    bool paused = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * x + transform.forward * z;
        if (IsGrounded())
        {
            rb.AddForce(movement * moveSpeed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGrounded())
        {
            rb.linearDamping = groundDrag;
        }
        else {
            rb.linearDamping = 0;
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }


        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            canvas.GetComponent<UiHandlerScript>().pause();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && paused)
        {
            canvas.GetComponent<UiHandlerScript>().resume();
        }
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundDistance, GroundMask);
    }
}
