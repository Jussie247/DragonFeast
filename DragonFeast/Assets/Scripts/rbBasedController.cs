using System.Threading;
using System.Threading.Tasks;
using FMOD;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class rbBasedController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    private Rigidbody rb;

    [SerializeField] Transform groundCheck, attackPos;
    [SerializeField] LayerMask GroundMask, enemy, cage, destructable;
    [SerializeField] float groundDistance;
    [SerializeField] float groundDrag;

    [SerializeField] GameObject canvas, radicle, destVFX, woodPile;
    [SerializeField] float attackRange = 5;
    [SerializeField] float punchForce;

    //Vital
    [SerializeField] int shield = 0;
    [SerializeField] GameObject shieldAsset;
    GameObject shieldInstance;
    [SerializeField] int HP = 3;
    [SerializeField] int maxHP = 3;
    [SerializeField] float hungies = 100;
    [SerializeField] float hungerLoss = 1;

    [SerializeField] bool attacked, ate;
    [SerializeField] float ATKcooldown = 0.2f, eatCooldown = 0.2f;

    [SerializeField] float animationSpeedFactor = 0.083f;
    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        updateHP();
        hideRadicle();
        animator = GetComponent<Animator>();
        shieldInstance = Instantiate(shieldAsset);
        shieldInstance.SetActive(false);
    }

    void FixedUpdate()
    {
        // get movement input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * x + transform.forward * z;
        if (IsGrounded())
        {
            //walk
            rb.AddForce(movement * moveSpeed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //make the player stick to the ground
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        
        //Look for Keyinputs for walkanimation
        if (Input.GetAxis("Horizontal")!=0 || Input.GetAxis("Vertical")!=0)
        {
            animator.SetBool("walking", true);
            animator.speed = rb.linearVelocity.magnitude * animationSpeedFactor;
            //print(rb.linearVelocity.magnitude * animationSpeedFactor);
        }
        else
        {
            animator.SetBool("walking", false);
            animator.speed = 1;
        }

        //adjust damping on surface and air
        if (IsGrounded())
        {
            rb.linearDamping = groundDrag;
        }
        else
        {
            rb.linearDamping = 0;
        }
        //charge attack
        if (Input.GetMouseButtonDown(0) && !UiHandlerScript.paused)
        {
            animator.SetBool("startAttack", true);
            if (!attacked)
            {
                showRadicle();
            }
        }
        //attack
        if (Input.GetMouseButtonUp(0) && !UiHandlerScript.paused)
        {
            animator.SetBool("startAttack", false);
            if (!attacked)
            {
                transform.GetComponent<PlayerSoundHandler>().playAttackSound();

                hideRadicle();
                print("attack enemy");
                //check if enemy has been attacked
                if (Physics.CheckSphere(attackPos.transform.position, attackRange, enemy))
                {
                    print("hit enemy");
                    Transform enemy = GetClosestObjectTransformByTag("enemy");
                    enemy.GetComponent<TestOpponent>().Hit();
                    enemy.GetComponent<TestOpponent>().bounce();
                }
                //check if cage has been attacked
                else if (Physics.CheckSphere(attackPos.transform.position, attackRange, cage))
                {
                    print("hit cage");
                    Transform cage = GetClosestObjectTransformByTag("cage");
                    cage.GetComponent<cageHandler>().hitCage();
                }
                //check if destructable has been attacked
                else if (Physics.CheckSphere(attackPos.transform.position, attackRange, destructable))
                {
                    print("hit destructable");
                    Transform destObject = GetClosestObjectTransformByTag("destructable");
                    Instantiate(destVFX, destObject.position, new Quaternion(0, 0, 0, 0));
                    GameObject woodPileInstance = Instantiate(woodPile,  new Vector3(destObject.position.x, 0f, destObject.position.z), destObject.rotation);
                    //play destruction sound
                    transform.GetComponent<playDestructionSound>().playDestructables(woodPileInstance);
                    Destroy(destObject.gameObject);
                }
                    attacked = true;
                Invoke(nameof(resetAttack), ATKcooldown);
            }
        }

        //if (Input.GetButtonDown("Jump") && IsGrounded() && !UiHandlerScript.paused)
        //{
        //    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        //}
        //eat
        if (Input.GetKeyDown(KeyCode.F) && !UiHandlerScript.paused)
        {
            transform.GetComponent<PlayerSoundHandler>().playEatSound();
            animator.SetBool("eating", true);
            if (!ate)
            {
                print("eat enemy");
                if (Physics.CheckSphere(attackPos.transform.position, attackRange, enemy))
                {
                    print("hit enemy");
                    Transform enemy = GetClosestObjectTransformByTag("enemy");
                    enemy.GetComponent<TestOpponent>().eat();
                    if (hungies + 50 >= 100)
                    {
                        hungies = 100;
                    }
                    else
                    {
                        hungies += 50;
                    }
                }
                ate = true;
                Invoke(nameof(resetEat), eatCooldown);
            }
        }

        // eating released to stop the animation/bad?/
        if (Input.GetKeyUp(KeyCode.F) && !UiHandlerScript.paused)
        {
            animator.SetBool("eating", false);
        }

        // pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!UiHandlerScript.paused)
            {
                canvas.GetComponent<UiHandlerScript>().pause();
                //Cursor.visible = true;
                
            }
            else
            {
                canvas.GetComponent<UiHandlerScript>().resume();
                //Cursor.visible = false;
                
            }
        }
        //check if player died
        if (HP <= 0)
        {
            hit();
            updateHP();
        }
        //check if player has hunger
        if (hungies <= 0)
        {
            hit();
        }
        if(shield > 0)
        {
            shieldInstance.SetActive(true);
        }
        else
        {
            shieldInstance.SetActive(false);
        }
        //update hungies
        canvas.GetComponent<UiHandlerScript>().updateHungerBar(hungies * 0.01f);
        hungies = hungies - hungerLoss * Time.deltaTime;
    }
    //reset attacks
    void resetAttack()
    {
        attacked = false;
    }
    void resetEat()
    {
        ate = false;
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundDistance, GroundMask);
    }

    //handle hits
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
            //play death sound
            transform.GetComponent<PlayerSoundHandler>().playDeathSound();
            updateHP();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
    //heal player
    public void heal(int _ammount)
    {
        if (HP < maxHP)
        {
            HP += _ammount;
            updateHP();
        }
    }
    //add shield to the player
    public void addShield(int _ammount)
    {
        shield += _ammount;
    }
    //handle radicle visibility
    void showRadicle()
    {
        radicle.SetActive(true);
    }
    void hideRadicle()
    {
        radicle.SetActive(false);
    }
    //update the HP on the UI
    private void updateHP()
    {
        canvas.GetComponent<UiHandlerScript>().updateHP(HP);
    }
    // get closest Transform
    Transform GetClosestObjectTransformByTag(string tag)
    {

        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);

        // Convert GameObjects to Transforms
        Transform[] objectTransforms = new Transform[objects.Length];
        for (int i = 0; i < objects.Length; i++)
        {
            objectTransforms[i] = objects[i].transform;
        }

        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in objectTransforms)
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
    //show gizmos to see attack range in preview
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(attackPos.transform.position, attackRange);
    }
}
