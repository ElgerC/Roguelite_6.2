using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditScript : AttackingEnemy
{
    [SerializeField] private float normalSpeed;
    [SerializeField] private float runSpeed;

    [SerializeField] private float runMinRange;

    private void RunDetect()
    {
        if (PlayerDetection(runMinRange))
        {
            speed = normalSpeed;
            animator.SetFloat("RunSpeed", 1);
        }
        else
        {
            speed = runSpeed;
            animator.SetFloat("RunSpeed", 2);
        }
    }
    protected override void Chasing()
    {
        base.Chasing();

        RunDetect();
    }
}
