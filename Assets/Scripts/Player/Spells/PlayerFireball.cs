using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireball : BaseSpell
{
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    protected override void Start()
    {
        base.Start();

        rb.velocity = transform.right*projectileSpeed;

        transform.localScale = new Vector3(projectileSize, projectileSize, projectileSize);

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
