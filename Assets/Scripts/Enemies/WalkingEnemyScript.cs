using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemyScript : GeneralEnemyScript
{
    private Rigidbody2D rb;

    [SerializeField] private float speed;

    //-1 = left, 1 = right
    [SerializeField] private int moveDirection;
    [SerializeField] private GameObject roamPoint;
    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
    }
    protected override void Roaming()
    {
        Vector3 move = new Vector3(moveDirection * speed, 0, 0);
        rb.velocity = move;
    }

    protected override void Chasing()
    {
        
    }

    protected override void Attack()
    {
      
    }
}
