using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireball : BaseSpell
{
    private Rigidbody2D rb;
    private CircleCollider2D circleCol;
    protected override void Awake()
    {
        base.Awake();

        rb = GetComponent<Rigidbody2D>();
        circleCol = GetComponent<CircleCollider2D>();
    }
    protected void Start()
    {
        circleCol.enabled = false;
        transform.localScale = new Vector3(projectileSize, projectileSize, projectileSize);
    }

    public override void Release()
    {
        circleCol.enabled = true;

        rb.velocity = transform.right*projectileSpeed;

        StartCoroutine(DestroyTimer());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagabele Idmg = collision.GetComponent<IDamagabele>();

        if (Idmg != null)
        {
            Idmg.TakeDamage((int)damage);
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(range);
        Destroy(gameObject);
    }
}
