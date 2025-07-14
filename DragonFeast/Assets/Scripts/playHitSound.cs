using UnityEngine;

public class playHitSound : MonoBehaviour
{
    [SerializeField] FMODUnity.EventReference ArrowHitAud;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playHit()
    {
        FMODUnity.RuntimeManager.PlayOneShot(ArrowHitAud);
    }
}
