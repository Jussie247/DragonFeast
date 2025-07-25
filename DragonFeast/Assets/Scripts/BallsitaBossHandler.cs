using UnityEngine;

public class BallsitaBossHandler : MonoBehaviour
{

    [SerializeField] private int HP = 10;
    //[SerializeField] private int arrows = 3;
    bool playerInAttackRange = false;
    [SerializeField] LayerMask playerMask;
    [SerializeField] Transform[] instancePositions;
    [SerializeField] GameObject arrow, egg, player;
    [SerializeField] float arrowSpeed, attackCooldown = 0.5f, attackRange = 60;
    bool attacked = false;

    Transform eggSpawn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("RB_Based_Controller");
    }
    void Awake()
    {
        player = GameObject.Find("RB_Based_Controller");
        
    }

    // Update is called once per frame
    void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);
        if (playerInAttackRange)
        {
            playerInRange();
        }

        if (HP <= 0)
        {
            //die
            die();
            GetComponent<BossSoundManager>().PlayBossDeathSound(transform.gameObject);
        }
    }

    void playerInRange()
    {
        print("player in attack range");
        transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position, new Vector3(0,1,0));
        if (!attacked)
        {
            shoot();
            attacked = true;
            Invoke(nameof(resetAttack), attackCooldown);
        }
    }

    void shoot()
    {
        GetComponent<BossSoundManager>().PlayBossAttackSound(transform.gameObject);

        GetComponent<BallistaAnimationManager>().playShootAnimation();
        GetComponent<BallistaAnimationManager>().stopShootAnimation();
        print("ballista shooting");
        for (int i = 0; i < instancePositions.Length; i++)
        {
            GameObject awwow = Instantiate(arrow, instancePositions[i].position + instancePositions[i].forward + new Vector3(0, 1, 0), Quaternion.LookRotation(transform.forward));
            awwow.AddComponent<Rigidbody>();
            awwow.GetComponent<Rigidbody>().AddForce(transform.forward * arrowSpeed);
        }
    }

    private void resetAttack()
    {
        attacked = false;
    }

    void die()
    {
        eggSpawn = GameObject.Find("eggSpawn").transform;
        Instantiate(egg, eggSpawn.position, eggSpawn.rotation);
        Destroy(transform.gameObject);
    }

    public void hit()
    {
        HP--;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, attackRange);
    }
}
