using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBolt : BaseSpell
{
    [SerializeField] private LayerMask damageLayerMask;

    private float curRange = 0f;

    [SerializeField] private GameObject attackPoint;
    private void Start()
    {
        StartCoroutine(RangeGrow());
    }
    public override void Release()
    {
        StopAllCoroutines();

        Collider2D[] coliders = Physics2D.OverlapCircleAll(attackPoint.transform.position, curRange, damageLayerMask);

        for (int i = 0; i < coliders.Length; i++)
        {
            IDamagabele dmg = coliders[i].GetComponent<IDamagabele>();

            if (dmg != null)
            {
                dmg.TakeDamage((int)damage);
            }
        }

        Destroy(gameObject);
    }

    private IEnumerator RangeGrow()
    {
        for (int i = 0; i <= range*10; i++) 
        {
            curRange += range / (range * 10);

            attackPoint.transform.position += new Vector3(transform.right.x * (curRange/60), 0, 0);

            transform.localScale = new Vector3(curRange*2, curRange*2, curRange * 2);

            yield return new WaitForSeconds(range / (range * 10));
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(attackPoint.transform.position,curRange);   
    }
}
