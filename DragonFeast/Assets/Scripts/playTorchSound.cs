using UnityEngine;

public class playTorchSound : MonoBehaviour
{
    [SerializeField] FMODUnity.EventReference TorchSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayTorchSound(GameObject o)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(TorchSound, o);
    }
}
