using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelScript : GeneralEnemyScript
{
    [SerializeField] private float speed;
    public bool canMove = true;

    [SerializeField] private float minRange;
    [SerializeField] private float maxRange;

    private Rigidbody2D rb;
    
    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
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


        transform.forward = new Vector3(0, 0, -moveDirection);

        if (canMove)
            rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);

        rb.velocity = new Vector2(moveDirection * speed, 0);
    }
    protected override void Chasing()
    {
        

        rb.velocity = new Vector2(moveDirection * speed, 0);
    }
}
