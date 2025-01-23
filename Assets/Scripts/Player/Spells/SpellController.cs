using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class SpellController : MonoBehaviour
{
    [SerializeField] private List<GameObject> spells = new List<GameObject>();

    private GameObject chosenSpell;
    [SerializeField] private GameObject currentSpell;

    [SerializeField] private Transform castPoint;
    [SerializeField] private bool buttonHeld;

    private Animator animator;

    public List<SpellStats> alterationsSpell1 = new List<SpellStats>();
    public List<SpellStats> alterationsSpell2 = new List<SpellStats>();
    public List<SpellStats> alterationsSpell3 = new List<SpellStats>();
    private List<SpellStats> currentAlterations = new List<SpellStats>();

    public List<List<SpellStats>> alterationOptions = new List<List<SpellStats>>();

    private void Awake()
    {
        animator = GetComponent<Animator>();

        alterationOptions.Add(alterationsSpell1);
        alterationOptions.Add(alterationsSpell2);
        alterationOptions.Add(alterationsSpell3);
    }


    //On press starting the "Cast" animation
    public void Cast1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Cast(spells[0]);
            currentAlterations = alterationsSpell3;
        }
        if (context.canceled)
        {
            EndChannel();
        }
    }

    public void Cast2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Cast(spells[1]);
            currentAlterations = alterationsSpell3;
        }
        if (context.canceled)
        {
            EndChannel();
        }
    }

    public void Cast3(InputAction.CallbackContext context)
    {
        if (context.performed)
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
            currentSpell.GetComponent<BaseSpell>().alterations = currentAlterations;
        }
    }

    public void EndChannel()
    {
        buttonHeld = false;


    }

    public void Release()
    {
        if (buttonHeld == false && currentSpell)
        {
            currentSpell.GetComponent<BaseSpell>().Release();

            currentSpell = null;
            chosenSpell = null;

            animator.SetBool("Channeling", false);
        }
    }
}
