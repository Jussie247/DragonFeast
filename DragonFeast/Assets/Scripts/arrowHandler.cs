using UnityEngine;
using UnityEngine.VFX;

public class arrowHandler : MonoBehaviour
{
    public float lifetime = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
        //stick to what it hit
        transform.parent = collision.gameObject.transform;
        Destroy(GetComponent<Rigidbody>());
        Invoke(nameof(destroyArrow), lifetime);
    }

    public void destroyArrow()
    {
        Destroy(transform.gameObject);
    }
}
