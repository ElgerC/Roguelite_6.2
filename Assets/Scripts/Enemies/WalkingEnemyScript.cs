using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemyScript : GeneralEnemyScript
{
    private Rigidbody2D rb;

    [SerializeField] private float speed;

    //-1 = left, 1 = right



    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        m_OutsideRoam = false;
    }
    protected override void Roaming()
    {
        if (Vector2.Distance(transform.position, roamPoint.transform.position) >= roamMaxDist)
        {
            if (!m_OutsideRoam)
            {
                m_OutsideRoam = true;
                moveDirection = -moveDirection;
            }
        }
        else
            m_OutsideRoam = false;

        rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);
    }

    protected override void Chasing()
    {
        float direction = (int) (new Vector2(player.transform.position.x,0) - new Vector2(transform.position.x,0)).normalized.x;

        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    protected override void Attack()
    {

    }
}
