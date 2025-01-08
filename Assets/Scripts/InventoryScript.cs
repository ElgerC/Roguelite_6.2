using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public List<GameObject> spellSlots = new List<GameObject>();
    public List<GameObject> inventorySlots = new List<GameObject>();
    public List<GameObject> items = new List<GameObject>();

    public int storedItems;

    public static InventoryScript instance;

    [SerializeField] private GameObject AlterationUI;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    public void ActiveSwitch()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        if (gameObject.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void AddItem()
    {
        Transform curSlot = inventorySlots[storedItems].transform;
        Instantiate(AlterationUI, curSlot);
    }
    public void Sort()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].transform.parent = inventorySlots[i].transform;
            items[i].transform.localPosition = Vector2.zero;
        }
    }
}
