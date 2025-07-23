using UnityEngine;

public class playDoorOpenSound : MonoBehaviour
{
    [SerializeField] FMODUnity.EventReference DoorSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playDoorSound(GameObject o)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(DoorSound, o);
    }
}

