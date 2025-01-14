using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlterationDropScript : MonoBehaviour, IDropable
{
    void IDropable.OnCollect()
    {
        InventoryScript.instance.AddItem();
        Destroy(gameObject);
    }
}
