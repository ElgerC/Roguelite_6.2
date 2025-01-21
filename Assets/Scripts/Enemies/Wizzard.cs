using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizzard : GeneralEnemyScript
{
    private bool canAttack = true;

    [Header("Wizzard Variables")]
    [SerializeField] private GameObject firebal;
    [SerializeField] private GameObject attackPoint;

    [SerializeField] private float AtkCd;

    protected override void Attack()
    {
        //Rotating the enemy to face the player
        float direction = moveDirection;
        if (player != null)
        {
            direction = (int)(new Vector2(player.transform.position.x, 0) - new Vector2(transform.position.x, 0)).normalized.x;
        }
        transform.forward = new Vector3(0, 0, -direction);

        if (canAttack)
        {
            animator.SetTrigger("Attack");
            StartCoroutine(AttackCooldown());
        }
    }

    public void SpawnFirebal()
    {
        Vector2 direction;

        direction = (new Vector2(player.transform.position.x, player.transform.position.y) - new Vector2(transform.position.x, transform.position.y)).normalized;

        GameObject go = Instantiate(firebal,attackPoint.transform.position,Quaternion.identity);
        go.GetComponent<FirebalScript>().direction = direction;
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(AtkCd);
        canAttack = true;
    }
}
