using UnityEngine;

public class entityFootsteps : MonoBehaviour
{
    [SerializeField] float playbackSpeed = 3f;
    [SerializeField] float positionOffset = 0.75f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;

    bool isStone;
    bool isGrass;
    bool isGravel;
    bool isCarpet;

    Vector3 oldPos;

    [SerializeField] FMODUnity.EventReference EntityStoneStepAud;
    //[SerializeField] FMODUnity.EventReference CarpetStepAud;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("CallFootSteps", 0, playbackSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        isStone = true;
    }

    void CallFootSteps()
    {
        if (Vector3.Distance(transform.position, oldPos) > positionOffset)
        {
            if (isStone)
            {
                FMODUnity.RuntimeManager.PlayOneShotAttached(EntityStoneStepAud, transform.gameObject);
                playbackSpeed = 3f; //TODO: adjust playback speed to movement speed
            }
            oldPos = transform.position;
        }
    }
}
