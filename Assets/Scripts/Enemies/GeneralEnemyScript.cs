using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class GeneralEnemyScript : MonoBehaviour, IDamagabele
{
    private enum States
    {
        Roaming,
        Chasing,
        Attack
    }

    private States state = States.Roaming;

    protected GameObject player;

    [SerializeField] protected GameObject roamPoint;
    [SerializeField] protected float roamMaxDist;
    protected bool m_OutsideRoam;

    [SerializeField] private float detectionRange;
    [SerializeField] private LayerMask detectionLayerMask;

    [SerializeField] private float attackRange;

    [SerializeField] private float health;
    [SerializeField] private float value;

    [SerializeField] protected int moveDirection;

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
                    int direction = (int)(new Vector2(roamPoint.transform.position.x, 0) - new Vector2(transform.position.x, 0)).normalized.x;
                    moveDirection = direction;
                    m_OutsideRoam = true;
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
        Collider2D playerDetect = Physics2D.OverlapCircle(transform.position, range, detectionLayerMask);
        if (playerDetect != null)
        {
            player = playerDetect.gameObject;
            return true;
        }
        else
        {
            player = null;
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
