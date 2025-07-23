using UnityEngine;

public class playDragonFreedSound : MonoBehaviour
{

    [SerializeField] FMODUnity.EventReference bbDragonFreeSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playbbDragonFreeSound(GameObject o)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(bbDragonFreeSound, o);
    }
}

 

