using UnityEngine;

public class playerFootsteps : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float playbackSpeed = 0;
    [SerializeField] float playbackRate = 0.75f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;

    bool isStone;
    bool isGrass;
    bool isGravel;
    bool isCarpet;

    Vector3 oldPos;

    [SerializeField] FMODUnity.EventReference StoneStepAud;
    //[SerializeField] FMODUnity.EventReference CarpetStepAud;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("CallFootSteps", 0, playbackSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CallFootSteps()
    {
        if (Vector3.Distance(player.position, oldPos) > playbackRate)
        {
            if (isStone)
            {
                FMODUnity.RuntimeManager.PlayOneShot(StoneStepAud);
                playbackSpeed = 3f;
            }
            oldPos = player.position;
        }
    }
}
