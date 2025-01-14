using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour, IDamagabele
{
    [SerializeField] private int health;
    [SerializeField] private int imunityDuration;
    private bool canTakeDmg = true;

    private Rigidbody2D rb;
    private Vector2 movementInput;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpHeight;

    [SerializeField] private Animator animator;

    [SerializeField] private LayerMask groundCheckLayerMask;
    [SerializeField] private float groundCheckRadius;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void TakeDamage(int amount)
    {
        if (canTakeDmg)
        {
            health -= amount;
            if(health > 0)
            {
                animator.SetTrigger("Hurt");
            }
            else
            {
                animator.SetTrigger("Die");
            }

            StartCoroutine(ImunityTimer());
        }
    }

    private IEnumerator ImunityTimer()
    {
        canTakeDmg = false;
        yield return new WaitForSeconds(imunityDuration);
        canTakeDmg = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDropable dropable = collision.gameObject.GetComponent<IDropable>();

        if (dropable != null)
        {
            dropable.OnCollect();
        }
    }
    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            movementInput = new Vector2(ctx.ReadValue<Vector2>().x, 0);
        }
    }
    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && animator.GetInteger("Jumps") < 2)
        {
            Vector2 jump = Vector2.up * jumpHeight;
            rb.AddForce(jump, ForceMode2D.Impulse);
            animator.SetInteger("Jumps", animator.GetInteger("Jumps") + 1);
            if (animator.GetInteger("Jumps") > 1)
            {
                groundCheckRadius = 1.1f;
            }
        }
    }

    private void GroundCheck()
    {
        if (animator.GetInteger("Jumps") > 0)
        {
            Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, groundCheckRadius, groundCheckLayerMask);
            Debug.Log(objects.Length);
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i].gameObject != gameObject)
                {
                    animator.SetInteger("Jumps", 0);
                    groundCheckRadius = 1;
                }
                    
            }
        }
    }
    private void Update()
    {
        Vector2 move = new Vector2(movementInput.x, 0).normalized * movementSpeed;
        rb.velocity = new Vector2(move.x, rb.velocity.y);

        animator.SetFloat("Speed", Mathf.Abs(movementInput.x));

        if (movementInput.x > 0)
            transform.forward = new Vector2(0, 0);
        else if (movementInput.x < 0)
            transform.forward = new Vector3(0, 0, -1);

        GroundCheck();
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            animator.SetBool("IsAttacking", true);
        }
        if (context.canceled)
        {
            animator.SetBool("IsAttacking", false);
        }
    }
    public void Cast(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            animator.SetTrigger("Cast");
        }
    }

    public void AttackCheck()
    {
        if(animator.GetBool("IsAttacking") == false)
        {
            animator.SetTrigger("EndAttack");
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, groundCheckRadius);
    }
}