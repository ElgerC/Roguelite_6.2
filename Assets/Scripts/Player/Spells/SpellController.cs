using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpellController : MonoBehaviour
{
    [SerializeField] private List<GameObject> spells = new List<GameObject>();
    private GameObject currentSpell;

    [SerializeField] private Transform castPoint;

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
            currentAlterations = alterationsSpell1;
        }

    }

    public void Cast2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Cast(spells[1]);
            currentAlterations = alterationsSpell2;
        }

    }

    public void Cast3(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Cast(spells[2]);
            currentAlterations = alterationsSpell3;
        }

    }

    private void Cast(GameObject spell)
    {
        if (!animator.GetBool("IsAttacking") && animator.GetInteger("Jumps") < 2)
        {
            animator.SetTrigger("Cast");

            currentSpell = spell;
        }
    }
    public void EndCast()
    {
        GameObject go = Instantiate(currentSpell, castPoint.position, transform.rotation);
        go.GetComponent<BaseSpell>().alterations = currentAlterations;

        currentSpell = null;
    }
}
