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
    [Header("States")]
    [SerializeField] private States state = States.Roaming;
    [SerializeField] protected GameObject roamPoint;
    [SerializeField] protected float roamMaxDist;
    protected bool m_OutsideRoam;
    
    [Header("Detection")]
    [SerializeField] private float detectionRange;
    [SerializeField] protected LayerMask detectionLayerMask;
    [SerializeField] private float attackRange;
    protected GameObject player;
    
    [Header("Atributes")]
    [SerializeField] private float health;
    [SerializeField] private float value;

    //-1 = left, 1 = right
    [SerializeField] protected int moveDirection;


    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected void Update()
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

    protected bool PlayerDetection(float range)
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
        health--;
        if (health > 0)
        {
            animator.SetTrigger("Hurt");
        } else
        {
            animator.SetTrigger("Die");
        }
    }

    public void OnDeath()
    {
        Destroy(gameObject);
    }
}
