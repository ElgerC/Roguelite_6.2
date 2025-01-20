using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemyScript : GeneralEnemyScript
{
    private Rigidbody2D rb;

    [SerializeField] protected float speed;
    public bool canMove = true;

    [SerializeField] private GameObject footHeight;
    [SerializeField] private GameObject kneeHeight;
    [SerializeField] private float stepRayLength;
    [SerializeField] private LayerMask stepLayerMask;
    [SerializeField] private float stepHeight;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        m_OutsideRoam = false;
    }
    protected override void Roaming()
    {
        Step();

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
    }

    protected override void Chasing()
    {
        Step();

        float direction = moveDirection;
        if (player != null)
        {
            direction = (int)(new Vector2(player.transform.position.x, 0) - new Vector2(transform.position.x, 0)).normalized.x;
        }


        transform.forward = new Vector3(0, 0, -direction);

        if (canMove)
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    protected override void Attack()
    {
        rb.velocity = Vector2.zero;
    }

    private void Step()
    {
        Debug.DrawRay(footHeight.transform.position, -transform.right, Color.red, stepRayLength);

        if (Physics2D.Raycast(footHeight.transform.position, -transform.right, stepRayLength, stepLayerMask))
        {
            Debug.Log("Obstacle detected");
            if (!Physics2D.Raycast(kneeHeight.transform.position, -transform.right, stepRayLength, stepLayerMask))
            {
                Debug.Log("Step");
                rb.MovePosition(new Vector2(transform.position.x, transform.position.y + stepHeight));
            }
        }
    }
}
