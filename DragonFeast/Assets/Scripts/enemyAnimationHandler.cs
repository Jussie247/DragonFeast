using UnityEngine;

public class enemyAnimationHandler : MonoBehaviour
{
    EnemyType enemyType;

    Animator animator;
    // Awake is called when the object gets instantiated
    void Awake()
    {
        animator = GetComponent<Animator>();
        enemyType = transform.parent.GetComponent<TestOpponent>().enemyType;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void idleAnim()
    {
        if(enemyType == EnemyType.Knight)
        {
            animator.SetBool("knightidle", true);
        }else if(enemyType == EnemyType.Archer)
        {
            animator.SetBool("archeridle", true);
        }
        else if (enemyType == EnemyType.Lancer)
        {
            animator.SetBool("lanceridle", true);
        }
    }

    public void walkAnim()
    {
        if (enemyType == EnemyType.Knight)
        {
            animator.SetBool("knightwalk", true);
        }
        else if (enemyType == EnemyType.Archer)
        {
            animator.SetBool("archerwalk", true);
        }
        else if (enemyType == EnemyType.Lancer)
        {
            animator.SetBool("lancerwalk", true);
        }
    }

    public void attackAnim()
    {
        if (enemyType == EnemyType.Knight)
        {
            animator.SetBool("knightattack", true);
        }
        else if (enemyType == EnemyType.Archer)
        {
            animator.SetBool("archerattack", true);
            transform.GetChild(0).GetComponent<Animator>().SetBool("drawbow", true);
        }
        else if (enemyType == EnemyType.Lancer)
        {
            animator.SetBool("lancerattack", true);
        }
    }

    public void endAttackAnim()
    {
        if (enemyType == EnemyType.Knight)
        {
            animator.SetBool("knightattack", false);
        }
        else if (enemyType == EnemyType.Archer)
        {
            animator.SetBool("archerattack", false);
            transform.GetChild(0).GetComponent<Animator>().SetBool("drawbow", false);
        }
        else if (enemyType == EnemyType.Lancer)
        {
            animator.SetBool("lancerattack", false);
        }
    }

    public void endWalkAnim()
    {
        if (enemyType == EnemyType.Knight)
        {
            animator.SetBool("knightwalk", false);
        }
        else if (enemyType == EnemyType.Archer)
        {
            animator.SetBool("archerwalk", false);
        }
        else if (enemyType == EnemyType.Lancer)
        {
            animator.SetBool("lancerwalk", false);
        }
    }
}
