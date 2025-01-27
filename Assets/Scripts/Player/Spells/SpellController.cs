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
    [SerializeField] public GameObject currentSpell;
    [SerializeField] private BaseSpell currentSpellScript;

    [SerializeField] private Transform castPoint;
    [SerializeField] public bool buttonHeld;

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
    [SerializeField] private Slider manaSlider;
    [SerializeField] private RectTransform sliderTransform;

    public float chargeTime;

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
        manaSlider = GameObject.FindWithTag("ManaBar").GetComponent<Slider>();
        sliderTransform = manaSlider.GetComponent<RectTransform>();

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
        if (animator.GetBool("Channeling"))
        {
            chargeTime += Time.deltaTime;
        }

        if (currentSpellScript)
        {
            playerScript.aditionalSpeed = currentSpellScript.slow;
        }

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

            currentSpellScript = currentSpell.GetComponent<BaseSpell>();

            currentSpellScript.alterations = currentAlterations;
            currentSpellScript.GetAlterations();

            if (currentSpellScript.manaCost <= mana)
            {

                mana -= currentSpellScript.manaCost;
                manaSlider.value = mana;

                currentSpellScript.playerScript = playerScript;
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

    public void CastFail()
    {
        Destroy(currentSpell);


        ResetVariables();


    }
    public void Release()
    {
        if (buttonHeld == false && currentSpell)
        {
            chargeTime = 0;

            if (currentSpellScript.chargeTime > chargeTime && chosenSpell != spells[1])
            {
                CastFail();
            }
            else
            {
                currentSpellScript.Release();

                if(currentSpellScript.projectiles > 1)
                {
                    StartCoroutine(SpawnExtraProjectiles(currentSpell,currentSpellScript));
                }


                if (chosenSpell == spells[2])
                {
                    rb.AddForce(new Vector2(-transform.right.x * currentSpellScript.selfDamage, 0));
                    playerScript.launched = true;
                } else
                {
                    playerScript.TakeDamage(currentSpellScript.selfDamage);
                }

                ResetVariables();
            }      
        }
    }

    private void ResetVariables()
    {
        currentSpell = null;
        chosenSpell = null;

        currentSpellScript = null;
        playerScript.aditionalSpeed = 0;

        animator.SetTrigger("CastFail");
        animator.SetBool("Channeling", false);
    }

    public void IncreaseMana(float amount)
    {
        maxMana += amount;
        mana = maxMana;

        sliderTransform.sizeDelta += new Vector2(amount / 5, 0);
        sliderTransform.position += new Vector3(amount/4, 0);  

        manaSlider.value = mana;
    }

    private IEnumerator SpawnExtraProjectiles(GameObject spell, BaseSpell script)
    {
        for (int i = 0; i < script.projectiles-1; i++)
        {
            yield return new WaitForSeconds(0.2f * (script.projectileSize/2));
            GameObject go = Instantiate(spell, castPoint.position, transform.rotation);

            BaseSpell currentSpellScriptTemp = go.GetComponent<BaseSpell>();

            currentSpellScriptTemp.baseStats = null;
            currentSpellScriptTemp.Release();
        }
    }
}
