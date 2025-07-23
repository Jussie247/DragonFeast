using UnityEngine;

public class BallistaAnimationManager : MonoBehaviour
{
    [SerializeField] GameObject boss;
    [SerializeField] GameObject ballista;
    Animator bossAnimator, ballistaAnimator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bossAnimator = boss.GetComponent<Animator>();
        ballistaAnimator = ballista.GetComponent<Animator>();
    }

    private void Awake()
    {
        bossAnimator = boss.GetComponent<Animator>();
        ballistaAnimator = ballista.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playShootAnimation()
    {
        bossAnimator.SetBool("attack", true);
        ballistaAnimator.SetBool("attack", true);
    }

    public void stopShootAnimation()
    {
        bossAnimator.SetBool("attack", false);
        ballistaAnimator.SetBool("attack", false);
    }
}
