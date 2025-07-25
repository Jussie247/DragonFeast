using UnityEngine;

public class BabyDragonSoundManager : MonoBehaviour
{
    [SerializeField] FMODUnity.EventReference AttackSound;
    [SerializeField] FMODUnity.EventReference HealSound;
    [SerializeField] FMODUnity.EventReference ExplosionSound;
    [SerializeField] FMODUnity.EventReference FlyingSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playAttackSound(GameObject o)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(AttackSound, o);
    }

    public void playHealSound(GameObject o)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(HealSound, o);
    }

    public void playExplosionSound(GameObject o)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(ExplosionSound, o);
    }

    public void playFlyingSound(GameObject o)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(FlyingSound, o);
    }
}
