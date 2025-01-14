using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public abstract class GeneralEnemyScript : MonoBehaviour, IDamagabele
{
    private enum States
    {
        Roaming,
        Chasing,
        Attack
    }

    private States state = States.Roaming;

    [SerializeField] private float detectionRange;
    [SerializeField] private LayerMask detectionLayerMask;

    [SerializeField] private float attackRange;

    [SerializeField] private float health;
    [SerializeField] private float value;

    private Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        switch (state)
        {
            case States.Roaming:
                Roaming();

                if (PlayerDetection(detectionRange))
                {
                    state = States.Chasing;
                }
                break;

            case States.Chasing:
                Chasing();

                if (PlayerDetection(attackRange))
                {
                    state = States.Attack;
                }
                else if (!PlayerDetection(detectionRange))
                {
                    state = States.Roaming;
                }
                break;

            case States.Attack:
                Attack();

                if (!PlayerDetection(attackRange))
                {
                    state = States.Chasing;
                }
                break;
        }
    }

    private bool PlayerDetection(float range)
    {
        if (Physics2D.OverlapCircle(transform.position, range, detectionLayerMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected virtual void Roaming()
    {

    }

    protected virtual void Chasing()
    {

    }

    protected virtual void Attack()
    {

    }

    public void TakeDamage(int amount)
    {
        if (health > 0)
        {
            animator.SetTrigger("Hurt");
        } else
        {
            animator.SetTrigger("Death");
        }
    }
}
