using UnityEngine;

public class PlayerSoundHandler : MonoBehaviour
{
    [SerializeField] FMODUnity.EventReference CollectEggAud;
    [SerializeField] FMODUnity.EventReference EatSound;
    [SerializeField] FMODUnity.EventReference DeathSound;

    [SerializeField] FMODUnity.EventReference MainTheme;
    [SerializeField] FMODUnity.EventReference AttackSound;

    GameObject player;
    FMOD.Studio.EventInstance mainTheme;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainTheme = FMODUnity.RuntimeManager.CreateInstance(MainTheme);
        mainTheme.start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playCollectSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(CollectEggAud);
    }

    public void playEatSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(EatSound);
    }

    public void playDeathSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(DeathSound);
    }

    public void playAttackSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(AttackSound);
    }

    public void stopMainTheme()
    {
        mainTheme.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
