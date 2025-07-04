using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameObject radicle;

    bool paused = false;

    //Vital
    public int shield = 0;
    public int HP = 3;
    public float hungies = 100;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        updateHP();
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


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            Time.timeScale = paused ? 0 : 1;
            if (paused)
            {
                canvas.GetComponent<UiHandlerScript>().resume();
            }
            else
            {
                canvas.GetComponent<UiHandlerScript>().pause(); 
            }
        }

        if(HP <= 0)
        {
            hit();
        }

        //read input for attack
        //use showRadicle() and hideRadicle() functions
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundDistance, GroundMask);
    }

    public void hit()
    {
        print("player got hit");
        if(shield > 0)
        {
            shield--;
        }else if(HP > 0)
        {
            HP--;
            updateHP();
        }
        else
        {
            //die
            updateHP();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    public void heal(int _ammount)
    {
        HP += _ammount;
        updateHP();
    }

    public void addShield(int _ammount)
    {
        shield += _ammount;
    }

    void showRadicle()
    {
        radicle.SetActive(true);
    }

    void hideRadicle()
    {
        radicle.SetActive(false);
    }

    private void updateHP()
    {
        canvas.GetComponent<UiHandlerScript>().updateHP(HP);
    }
    
}
