using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlterationDropScript : MonoBehaviour, IDropable
{
    void IDropable.OnCollect(PlayerScript playerScript)
    {
        SpellStats alteration = DropManager.instance.GenerateAlteration();

        InventoryScript.instance.AddItem(alteration);
        Destroy(gameObject);
    }
}
