using UnityEngine;
using FMODUnity;

public class BossSoundManager : MonoBehaviour
{
    [SerializeField] EventReference BossAttackSound;
    [SerializeField] EventReference BossDeathSound;
    [SerializeField] EventReference BossEntranceSound;




    public void PlayBossAttackSound(GameObject o)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(BossAttackSound, o);
    }

    public void PlayBossDeathSound(GameObject o)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(BossDeathSound, o);
    }

    public void PlayBossEntranceSound(GameObject o)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(BossEntranceSound, o);
    }


}
