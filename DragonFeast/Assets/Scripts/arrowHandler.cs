using UnityEngine;

public class arrowHandler : MonoBehaviour
{
    public float lifetime = 1;
    [SerializeField] LayerMask playerMask;
    [SerializeField] GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("RB_Based_Controller");
    }

    private void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((playerMask.value & (1 << collision.gameObject.layer)) != 0)
        {
            print("hit the player");
            player.GetComponent<rbBasedController>().hit();
        }

        print("arrow collided");
        //stick to what it hit
        transform.parent = collision.gameObject.transform;
        GetComponent<playHitSound>().playHit();
        Destroy(GetComponent<Rigidbody>());
        Invoke(nameof(destroyArrow), lifetime);
    }

    public void destroyArrow()
    {
        Destroy(transform.gameObject);
    }
}
