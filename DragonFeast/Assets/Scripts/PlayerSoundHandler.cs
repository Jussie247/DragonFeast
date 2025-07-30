using UnityEngine;

public class PlayerSoundHandler : MonoBehaviour
{
    [SerializeField] FMODUnity.EventReference CollectEggAud;
    [SerializeField] FMODUnity.EventReference EatSound;
    [SerializeField] FMODUnity.EventReference DeathSound;

    [SerializeField] FMODUnity.EventReference MainTheme;
    [SerializeField] FMODUnity.EventReference AttackSound;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FMODUnity.RuntimeManager.PlayOneShot(MainTheme);
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
}
