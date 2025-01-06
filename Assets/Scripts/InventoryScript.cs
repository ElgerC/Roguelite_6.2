using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public List<GameObject> spellSlots = new List<GameObject>(); 
    public List<GameObject> inventorySlots = new List<GameObject>();

    public int storedItems;

    public static InventoryScript instance;
    private void Awake()
    {
        if(instance == null)
                instance = this;
        else
            Destroy(this);
    }
    public void ActiveSwitch()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
