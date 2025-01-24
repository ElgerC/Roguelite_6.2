using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SpellController : MonoBehaviour
{
    [SerializeField] private List<GameObject> spells = new List<GameObject>();

    private GameObject chosenSpell;
    [SerializeField] private GameObject currentSpell;

    [SerializeField] private Transform castPoint;
    [SerializeField] private bool buttonHeld;

    private Animator animator;
    private PlayerScript playerScript;
    private Rigidbody2D rb;

    public List<SpellStats> alterationsSpell1 = new List<SpellStats>();
    public List<SpellStats> alterationsSpell2 = new List<SpellStats>();
    public List<SpellStats> alterationsSpell3 = new List<SpellStats>();
    private List<SpellStats> currentAlterations = new List<SpellStats>();

    public List<List<SpellStats>> alterationOptions = new List<List<SpellStats>>();

    public float mana;
    public float maxMana;
    public Slider manaSlider;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerScript = gameObject.GetComponent<PlayerScript>();
        rb = GetComponent<Rigidbody2D>();

        alterationOptions.Add(alterationsSpell1);
        alterationOptions.Add(alterationsSpell2);
        alterationOptions.Add(alterationsSpell3);
    }

    private void Start()
    {
        manaSlider.maxValue = maxMana;
        manaSlider.value = mana;
    }

    //On press starting the "Cast" animation
    public void Cast1(InputAction.CallbackContext context)
    {
        if (context.performed && !chosenSpell)
        {
            Cast(spells[0]);
            currentAlterations = alterationsSpell1;
        }
        if (context.canceled)
        {
            EndChannel();
        }
    }

    public void Cast2(InputAction.CallbackContext context)
    {
        if (context.performed && !chosenSpell)
        {
            Cast(spells[1]);
            currentAlterations = alterationsSpell2;
        }
        if (context.canceled)
        {
            EndChannel();
        }
    }

    public void Cast3(InputAction.CallbackContext context)
    {
        if (context.performed && !chosenSpell)
        {
            Cast(spells[2]);
            currentAlterations = alterationsSpell3;
        }
        if (context.canceled)
        {
            EndChannel();
        }
    }

    private void Update()
    {
        Debug.Log(transform.right);

        if (buttonHeld && currentSpell)
        {
            currentSpell.transform.position = castPoint.position;
            currentSpell.transform.forward = transform.forward;
        }
    }
    private void Cast(GameObject spell)
    {
        if (!buttonHeld && !currentSpell)
        {
            buttonHeld = true;

            if (!animator.GetBool("IsAttacking") && animator.GetInteger("Jumps") < 2)
            {
                animator.SetTrigger("Cast");

                chosenSpell = spell;
            }
        }
    }
    public void StartChannel()
    {
        if (buttonHeld)
        {
            animator.SetBool("Channeling", true);
        }

        if (chosenSpell)
        {
            currentSpell = Instantiate(chosenSpell, castPoint.position, transform.rotation);

            BaseSpell curSpelScript = currentSpell.GetComponent<BaseSpell>();

            if (curSpelScript.manaCost <= mana)
            {
                mana -= curSpelScript.manaCost;
                manaSlider.value = mana;

                curSpelScript.alterations = currentAlterations;
                curSpelScript.playerScript = playerScript;
            }
            else
            {
                CastFail();
            }
        }
    }

    public void EndChannel()
    {
        buttonHeld = false;
    }

    private void CastFail()
    {
        Destroy(currentSpell);

        currentSpell = null;
        chosenSpell = null;

        animator.SetTrigger("CastFail");
    }
    public void Release()
    {
        if (buttonHeld == false && currentSpell)
        {
            currentSpell.GetComponent<BaseSpell>().Release();

            if (chosenSpell == spells[0])
            {
                rb.AddForce(new Vector2(transform.right.x * currentSpell.GetComponent<BaseSpell>().selfDamage, 0));
            }

            currentSpell = null;
            chosenSpell = null;

            animator.SetBool("Channeling", false);
        }
    }
}
