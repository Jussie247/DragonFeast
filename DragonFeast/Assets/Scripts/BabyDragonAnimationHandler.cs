using UnityEngine;

public class BabyDragonAnimationHandler : MonoBehaviour
{
    Animator animator;
    DragonType dragonType;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        dragonType = transform.parent.GetComponent<babyDragonHandler>().dragonType;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playAttackAnim()
    {
        if (dragonType == DragonType.Attack)
        {
            animator.SetBool("attack", true);
        }
        else if (dragonType == DragonType.Heal)
        {
            animator.SetBool("heal", true);
        }
        else if (dragonType == DragonType.Kamikaze)
        {
            animator.SetBool("attack", true);
        }
    }
    public void stopAttackAnim()
    {
        if (dragonType == DragonType.Attack)
        {
            animator.SetBool("attack", false);
        }
        else if (dragonType == DragonType.Heal)
        {
            animator.SetBool("heal", false);
        }
        else if (dragonType == DragonType.Kamikaze)
        {
            animator.SetBool("attack", false);
        }
    }
    public void playIdleAnim()
    {
        if (dragonType == DragonType.Attack)
        {
            animator.SetBool("idle", true);
        }
        else if (dragonType == DragonType.Heal)
        {
            animator.SetBool("idle", true);
        }
        else if (dragonType == DragonType.Kamikaze)
        {
            animator.SetBool("idle", true);
        }
    }
    public void stopIdleAnim()
    {
        if (dragonType == DragonType.Attack)
        {
            animator.SetBool("idle", false);
        }
        else if (dragonType == DragonType.Heal)
        {
            animator.SetBool("idle", false);
        }
        else if (dragonType == DragonType.Kamikaze)
        {
            animator.SetBool("idle", false);
        }
    }
}
