using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebalScript : MonoBehaviour
{
    public Vector2 direction;
    [SerializeField] private float speed;
    [SerializeField] private int damage;

    [SerializeField] private float time;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(DestroyTimer());
    }

    private void Update()
    {
        rb.velocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagabele Idmg = collision.GetComponent<IDamagabele>();

        if(Idmg != null)
        {
            Idmg.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject );
    }
}