using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBolt : BaseSpell
{
    [SerializeField] private LayerMask damageLayerMask;
    protected override void Start()
    {
        base.Start();
    }

    public override void Release()
    {
        Collider2D[] coliders = Physics2D.OverlapCircleAll(transform.position, range, damageLayerMask);

        for (int i = 0; i < coliders.Length; i++)
        {
            coliders[i].GetComponent<IDamagabele>().TakeDamage((int)damage);
        }
    }
}
