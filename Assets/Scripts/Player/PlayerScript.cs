using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour, IDamagabele
{
    #region Health
    [SerializeField] private int health;
    [SerializeField] private int imunityDuration;
    private bool canTakeDmg = true;
    [SerializeField] private Slider healthBar;
    #endregion

    #region Movement
    private Rigidbody2D rb;
    private Vector2 movementInput;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float attackingMovementSpeed;

    //Jump
    [SerializeField] private float jumpHeight;
    [SerializeField] private LayerMask groundCheckLayerMask;
    [SerializeField] private float groundCheckRadius;
    #endregion

    [SerializeField] private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        healthBar.maxValue = health;
        healthBar.value = health;
    }
    public void TakeDamage(int amount)
    {
        //Player can only be damaged if the bool is true
        if (canTakeDmg)
        {
            health -= amount;

            //Checking if the player is still alive
            if (health > 0)
            {
                animator.SetTrigger("Hurt");
            }
            else
            {
                animator.SetTrigger("Die");
            }

            ChangeHealthUI();

            StartCoroutine(ImunityTimer());
        }
    }

    public void OnDeath()
    {

    }

    private IEnumerator ImunityTimer()
    {
        canTakeDmg = false;
        yield return new WaitForSeconds(imunityDuration);
        canTakeDmg = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Trying to find a dropable interface
        IDropable dropable = collision.gameObject.GetComponent<IDropable>();

        //Check if there was a drop
        if (dropable != null)
        {
            dropable.OnCollect();
        }
    }
    public void OnMove(InputAction.CallbackContext ctx)
    {
        //Checking if a button was pressed
        if (ctx.performed)
        {
            //Saving movement input in variable
            movementInput = new Vector2(ctx.ReadValue<Vector2>().x, 0);
        }
    }
    public void OnJump(InputAction.CallbackContext ctx)
    {
        //Checking if the player has pressed the jum button and can jump
        if (ctx.performed && animator.GetInteger("Jumps") < 2)
        {
            rb.velocity = Vector3.zero;

            Vector2 jump = Vector2.up * jumpHeight;
            rb.AddForce(jump, ForceMode2D.Impulse);

            //Setting a animator variable
            animator.SetInteger("Jumps", animator.GetInteger("Jumps") + 1);

            //Growing check radius while in the air
            if (animator.GetInteger("Jumps") > 1)
            {
                groundCheckRadius = 1.1f;
            }
        }
    }

    private void GroundCheck()
    {
        //Checking if the player is in the air
        if (animator.GetInteger("Jumps") > 0)
        {
            //Making a circle that detects nearby objects
            Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, groundCheckRadius, groundCheckLayerMask);

            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i].gameObject != gameObject)
                {
                    //Resting the animtor and check radius
                    animator.SetInteger("Jumps", 0);
                    groundCheckRadius = 1f;
                }

            }
        }
    }

    private void Update()
    {
        //converting the movement to the right speed
        Vector2 move;
        if (animator.GetBool("IsAttacking"))
        {
            move = new Vector2(movementInput.x, 0).normalized * attackingMovementSpeed;
        }
        else
        {
            move = new Vector2(movementInput.x, 0).normalized * movementSpeed;
        }
        rb.velocity = new Vector2(move.x, rb.velocity.y);

        //Setting the animator speed to the absolute* of the movement *(1 = 1 ,-1 = 1)
        animator.SetFloat("Speed", Mathf.Abs(movementInput.x));

        //Rotating the player to look in the right direction
        if (movementInput.x > 0)
            transform.forward = new Vector2(0, 0);
        else if (movementInput.x < 0)
            transform.forward = new Vector3(0, 0, -1);

        GroundCheck();
    }

    public void Attack(InputAction.CallbackContext context)
    {
        //On press the player starts attacking
        if (context.performed)
        {
            animator.SetBool("IsAttacking", true);
        }
        //On release the player stops attacking
        if (context.canceled)
        {
            animator.SetBool("IsAttacking", false);
        }
    }

    //On press starting the "Cast" animation
    public void Cast(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            animator.SetTrigger("Cast");
        }
    }

    //Checking if the player ended the attack and stopping the animation if they did
    public void AttackCheck()
    {
        if (animator.GetBool("IsAttacking") == false)
        {
            animator.SetTrigger("EndAttack");
        }
    }

    //Changing the UI elements equal to the player healths
    public void ChangeHealthUI()
    {
        healthBar.value = health;
    }
}