using UnityEngine;

public class doorManager : MonoBehaviour
{
    //[SerializeField] FMODUnity.EventReference OpenDoorSound;

    Quaternion targetRotation;
    private bool shouldRotate = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //if (shouldRotate)
        //{
        //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2f);

        //    // Stop rotating if we're very close to the target
        //    if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
        //    {
        //        transform.rotation = targetRotation;
        //        shouldRotate = false;
        //    }
        //}
    }

    public void openDoor()
    {
        //transform.gameObject.name = "openDoor";

        //// Set new target rotation (add 90 degrees to Y)
        //targetRotation = Quaternion.Euler(0, transform.eulerAngles.y + 90, 0);

        //// Start rotation
        //shouldRotate = true;
        ////FMODUnity.RuntimeManager.PlayOneShotAttached(OpenDoorSound, transform.gameObject);
        Destroy(gameObject);
    }
}
