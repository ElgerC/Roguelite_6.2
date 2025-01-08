using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    public Vector2 linkedPosition;

    private InventoryScript inventoryScript;
    private RectTransform rectTransform;

    private Transform savedTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        
    }
    private void Start()
    {
        inventoryScript = InventoryScript.instance;

        transform.parent = inventoryScript.inventorySlots[inventoryScript.items.Count].transform;
        rectTransform.localPosition = Vector2.zero;

        inventoryScript.items.Add(gameObject);
    }
    public void DragHandeler(BaseEventData data)
    {
        
        transform.SetParent(canvas.transform);

        PointerEventData pointerEventData = (PointerEventData)data;

        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, pointerEventData.position, canvas.worldCamera, out position);

        
        transform.position = canvas.transform.TransformPoint(position);
    }
    public void Drop()
    {
        transform.parent = savedTransform;
        for (int i = 0; i < inventoryScript.spellSlots.Count; i++)
        {
            GameObject curSlot = inventoryScript.spellSlots[i];
            if (Vector2.Distance(transform.position, curSlot.transform.position) < 30 && curSlot.transform.childCount == 0)
            {
                transform.parent = inventoryScript.spellSlots[i].transform;
            }
        }
        
        if(transform.parent != savedTransform)
        {
            inventoryScript.items.Remove(gameObject);
            inventoryScript.Sort();
        }
        rectTransform.localPosition = Vector2.zero;
    }
    public void Grab()
    {
        savedTransform = transform.parent;
    }
}
