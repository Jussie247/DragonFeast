using UnityEngine;
using UnityEngine.SceneManagement;

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

    void Awake()
    {
        player = GameObject.Find("RB_Based_Controller").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.CheckSphere(transform.position, collectDistance, playerMask))
        {
            player.GetComponent<PlayerSoundHandler>().playCollectSound();
            SceneManager.LoadScene(1);
            Destroy(transform.gameObject);
        }
    }
}
