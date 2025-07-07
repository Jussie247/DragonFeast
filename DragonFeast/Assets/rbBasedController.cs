using System.Threading;
using System.Threading.Tasks;
using FMOD;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
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
    public float attackRange = 5;
    public LayerMask enemy;
    public float punchForce;

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
        hideRadicle();
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
        else
        {
            rb.linearDamping = 0;
        }

        if (Input.GetMouseButtonDown(0))
        {
            showRadicle();
        }

        if (Input.GetMouseButtonUp(0))
        {
            hideRadicle();
            print("attack enemy");
            if (Physics.CheckSphere(transform.position, attackRange, enemy))
            {
                print("hit enemy");
                Transform enemy = GetClosestEnemyByTag("enemy");
                enemy.GetComponent<TestOpponent>().Hit();
                //remove AI
                Destroy(enemy.GetComponent<NavMeshAgent>());
                //add physics
                enemy.AddComponent<Rigidbody>();
                Vector3 pointer = radicle.transform.position - transform.position;
                Vector3 force = pointer * punchForce;
                enemy.GetComponent<Rigidbody>().AddForce(force);
            }
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

        if (HP <= 0)
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
        if (shield > 0)
        {
            shield--;
        }
        else if (HP > 0)
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

    Transform GetClosestEnemyByTag(string tag)
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);

        // Convert GameObjects to Transforms
        Transform[] enemyTransforms = new Transform[enemies.Length];
        for (int i = 0; i < enemies.Length; i++)
        {
            enemyTransforms[i] = enemies[i].transform;
        }

        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in enemyTransforms)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }
}
