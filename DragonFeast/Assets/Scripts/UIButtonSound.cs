using UnityEngine;

public class UIButtonSound : MonoBehaviour
{
    [SerializeField] FMODUnity.EventReference ButtonClickAud;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playButtonClickSouns()
    {
        FMODUnity.RuntimeManager.PlayOneShot(ButtonClickAud);
    }
}
