using UnityEngine;

public class dragonEggHandler : MonoBehaviour
{
    [SerializeField] float collectDistance = 1.5f;
    [SerializeField] LayerMask playerMask;
    [SerializeField] Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("RB_Based_Controller").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.CheckSphere(transform.position, collectDistance, playerMask))
        {
            player.GetComponent<playCollectSound>().playCollect();
            Destroy(transform.gameObject); 
        }
    }
}
