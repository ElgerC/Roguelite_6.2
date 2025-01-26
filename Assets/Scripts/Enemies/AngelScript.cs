using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelScript : GeneralEnemyScript
{
    [SerializeField] private float speed;
    public bool canMove = true;

    private bool canAtk = true;
    [SerializeField] private float atkCd;
    [SerializeField] private GameObject attackPoint;

    private float minHeight;
    private float maxHeight;

    [SerializeField] private float minHeightOffset;
    [SerializeField] private float maxHeightOffset;

    [SerializeField] private bool outsideRange = true;

    [SerializeField] private int flyDirection = -1;
    [SerializeField] private float flySpeed;

    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
    
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Roaming()
    {
        FlyCheck();

        RoamPointCheck(roamPoint);

        minHeight = roamPoint.transform.position.y + minHeightOffset;
        maxHeight = roamPoint.transform.position.y + maxHeightOffset;

        transform.forward = new Vector3(0, 0, -moveDirection);

        if (canMove)
        {
            rb.velocity = new Vector2(moveDirection * speed, flyDirection * flySpeed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        moveDirection = -moveDirection;
        flyDirection = -flyDirection;
    }
    protected override void ChasingCheck()
    {

    }

    protected override void Chasing()
    {
        FlyCheck();

        RoamPointCheck(player);

        minHeight = player.transform.position.y + minHeightOffset;
        maxHeight = player.transform.position.y + maxHeightOffset;

        if (canMove)
        {
            rb.velocity = new Vector2(moveDirection * speed, flyDirection * flySpeed);
        } else
        {
            rb.velocity = Vector2.zero;
        }

        if(canAtk)
        {
            canMove = false;
            animator.SetTrigger("Attack");
            StartCoroutine(AtkCooldown());
        }
    }

    private void FlyCheck()
    { 
        if (transform.position.y < minHeight || transform.position.y > maxHeight)
        {
            if (!outsideRange)
            {
                outsideRange = true;
                flyDirection = -flyDirection;
            }
        }
        else
            outsideRange = false;
    }

    private void RoamPointCheck(GameObject obj)
    {
        if (Vector2.Distance(transform.position, obj.transform.position) >= roamMaxDist)
        {
            if (!m_OutsideRoam)
            {
                m_OutsideRoam = true;
                moveDirection = -moveDirection;
            }
        }
        else
        {
            m_OutsideRoam = false;
            StartCoroutine(SecondCheck(obj));
        }

    }

    public void SpawnEnemy()
    {
        Debug.Log("Spawned enemy");
        canMove = true;

        int choice = Random.Range(0, enemies.Count - 1);

        GameObject go = Instantiate(enemies[choice],attackPoint.transform.position,Quaternion.identity);
        go.GetComponent<GeneralEnemyScript>().roamPoint = roamPoint;
    }
    private IEnumerator AtkCooldown()
    {
        canAtk = false;
        yield return new WaitForSeconds(atkCd);    
        canAtk = true;
    }

    public override void OnDeath()
    {
        GameManager.instance.AddRune();

        base.OnDeath();
    }

    private IEnumerator SecondCheck(GameObject obj)
    {
        yield return new WaitForSeconds(1);
        if (Vector2.Distance(transform.position, obj.transform.position) >= roamMaxDist)
        {
            if (!m_OutsideRoam)
            {
                m_OutsideRoam = true;
                moveDirection = -moveDirection;
            }
        }
        else
            m_OutsideRoam = false;
    }
}
