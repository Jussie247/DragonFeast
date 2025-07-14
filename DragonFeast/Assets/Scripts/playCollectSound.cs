using UnityEngine;

public class playCollectSound : MonoBehaviour
{
    [SerializeField] FMODUnity.EventReference CollectEggAud;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playCollect()
    {
        //print("collected an egg");
        FMODUnity.RuntimeManager.PlayOneShot(CollectEggAud);
    }
}
