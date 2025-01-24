using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaDrop : MonoBehaviour, IDropable
{
    [SerializeField] private float amount;
    public void OnCollect(PlayerScript playerScript)
    {
        SpellController spellController =  playerScript.GetComponent<SpellController>();

        spellController.IncreaseMana(amount);

        Destroy(gameObject);
    }
}
