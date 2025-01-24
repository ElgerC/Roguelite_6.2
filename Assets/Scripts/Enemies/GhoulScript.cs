using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class GhoulScript : WalkingEnemyScript
{
    [Header("Ghoul variables")]
    [SerializeField] private int exploTimer;
    [SerializeField] private float SpeedBoost;

    private bool timerActive = false;

    [SerializeField] private GameObject explosion;
    protected override void ChasingCheck()
    {
        if(state == States.Inair)
        {
            if (!PlayerDetection(detectionRange))
            {
                int direction = (int)(new Vector2(roamPoint.transform.position.x, 0) - new Vector2(transform.position.x, 0)).normalized.x;
                moveDirection = direction;
                m_OutsideRoam = true;
                state = States.Roaming;
            } else
            {
                state = States.Chasing;
            }
        }
    }

    protected override bool PlayerDetection(float range)
    {
        Collider2D playerDetect = Physics2D.OverlapCircle(transform.position, range, detectionLayerMask);
        if (playerDetect != null)
        {
            player = playerDetect.gameObject;

            if(!timerActive)
            {
                StartCoroutine(SpeedUp());
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator SpeedUp()
    {

        timerActive = true;

        for (int i = exploTimer; i > 0; i--)
        {
            yield return new WaitForSeconds(1);

            Debug.Log("Increasing speed");

            speed += SpeedBoost;
            animator.SetFloat("RunSpeed", animator.GetFloat("RunSpeed") + SpeedBoost/4);
        }
        
        Explode();
    }
    public override void TakeDamage(int amount)
    {
        Explode();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == player)
        {
            Explode();
        }
    }

    private void Explode()
    {
        Instantiate(explosion,transform.position,Quaternion.identity);

        Destroy(gameObject);
    }
}
