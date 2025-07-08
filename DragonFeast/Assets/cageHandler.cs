using UnityEngine;

public class cageHandler : MonoBehaviour
{
    public GameObject babyDragon;
    public Transform instancePos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
