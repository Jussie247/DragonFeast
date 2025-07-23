using UnityEngine;
using FMODUnity;

public class EnemySoundManager : MonoBehaviour
{
    [SerializeField] EventReference ArcherAttack;
    //[SerializeField] EventReference ArcherHit;
    [SerializeField] EventReference KnightAttack;

    [SerializeField] EventReference EnemyDeath;
    //[SerializeField] EventReference FootstepsEnemy;
    [SerializeField] EventReference SpotPlayer;

    [SerializeField] EventReference KnightHit;
    [SerializeField] EventReference ArcherHit;



    public void PlayArcherAttackSound(GameObject o)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(ArcherAttack, o);
    }

    //public void PlayArcherHitSound(GameObject o)
    //{
    //    FMODUnity.RuntimeManager.PlayOneShotAttached(ArcherHit, o);
    //}

    public void PlayKnightAttackSound(GameObject o)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(KnightAttack, o);
    }

    public void PlayEnemyDeathSound(GameObject o)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(EnemyDeath, o);
    }

    //public void PlayFootstepsEnemySound(GameObject o)
    //{
    //    FMODUnity.RuntimeManager.PlayOneShotAttached(FootstepsEnemy, o);
    //}

    public void PlaySpotPlayerSound(GameObject o)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(SpotPlayer, o);
    }

    public void PlayKnightHit(GameObject o)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(KnightHit, o);
    }

    public void PlayArcherHit(GameObject o)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(ArcherHit, o);
    }

}
