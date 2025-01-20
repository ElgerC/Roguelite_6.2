using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingEnemy : WalkingEnemyScript
{
    protected override void Attack()
    {
        base.Attack();
        animator.SetBool("IsAttacking", true);
    }

    protected override void Chasing()
    {
        base.Chasing();
        animator.SetBool("IsAttacking", false);
    }
}
