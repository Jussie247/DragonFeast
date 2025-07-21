using Unity.VisualScripting;
using UnityEngine;

public class cageHandler : MonoBehaviour
{
    public GameObject babyDragon;
    public Transform instancePos;
    bool isAir = false;
    public LayerMask wallMask, enemyMask;
    public float throwForce = 1000f;
    public Transform player;
    public Transform radicle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("RB_Based_Controller").transform;
        radicle = GameObject.Find("attackPos").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hitCage()
    {
        isAir = true;
        transform.gameObject.AddComponent<Rigidbody>();
        //launch the cage
        Vector3 pointer = radicle.transform.position - player.transform.position;
        Vector3 force = pointer * throwForce;
        GetComponent<Rigidbody>().AddForce(force);
    }

    public void destroy()
    {
        Instantiate(babyDragon, instancePos.transform.position, instancePos.transform.rotation);
        //Destroy self
        Destroy(transform.gameObject);
        //TODO: play particles here
        //
        //
    }

    private void OnCollisionEnter(Collision collision)
    {
        //make bounces configurable without imideatly deleting the game obj
        if (isAir)
        {
            // Check for Collision with a specified layer
            if ((wallMask.value & (1 << collision.gameObject.layer)) != 0)
            {
                print("hit a wall");
                destroy();
            }
            if ((enemyMask.value & (1 << collision.gameObject.layer)) != 0)
            {
                print("hit another enemy");
                Transform enemy = GetClosestObjectTransformByTag("enemy");
                enemy.GetComponent<TestOpponent>().Hit();
                enemy.GetComponent<TestOpponent>().bounce();
                destroy();
            }
        }
    }

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
}
