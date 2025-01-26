using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GustScript : BaseSpell
{
    private Rigidbody2D rb;
    private BoxCollider2D squareCol;
    private Animator animator;

    [SerializeField] private Sprite airSlash;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        squareCol = GetComponent<BoxCollider2D>();

        animator = GetComponentInChildren<Animator>();
    }
    private void Start()
    { 
        squareCol.enabled = false;
        transform.localScale = new Vector3(projectileSize, projectileSize, projectileSize);
    }

    public override void Release()
    {
        squareCol.enabled = true;

        animator.SetBool("Released", true);

        rb.velocity = transform.right * projectileSpeed;

        StartCoroutine(DestroyTimer());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D enemyRb = collision.GetComponent<Rigidbody2D>();
        GeneralEnemyScript generalEnemyScript = collision.GetComponent<GeneralEnemyScript>();

        Debug.Log(enemyRb);
        if (generalEnemyScript)
            generalEnemyScript.state = GeneralEnemyScript.States.Inair;
        enemyRb.AddForce(transform.right * damage, ForceMode2D.Impulse);
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(range);
        Destroy(gameObject);
    }
}
