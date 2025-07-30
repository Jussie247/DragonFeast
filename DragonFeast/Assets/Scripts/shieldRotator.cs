using UnityEngine;

public class shieldRotator : MonoBehaviour
{
    [SerializeField] float direction = 1;
    Transform player;
    float rotationSpeed = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("RB_Based_Controller").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position;
        transform.Rotate(new Vector3(0, 0, direction * rotationSpeed * Time.deltaTime));
    }
}
