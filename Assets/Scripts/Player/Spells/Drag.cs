using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Drag : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    public Vector2 linkedPosition;

    private InventoryScript inventoryScript;
    private RectTransform rectTransform;

    private Transform savedTransform;

    public SpellStats alteration;

    [SerializeField] private string[] alterationNames;


    [SerializeField] private GameObject hoverText;
    [SerializeField] private GameObject curHoverText;
    private RectTransform textTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = FindObjectOfType<Canvas>();

    }
    private void Start()
    {
        inventoryScript = InventoryScript.instance;

        transform.parent = inventoryScript.inventorySlots[inventoryScript.items.Count].transform;
        rectTransform.localPosition = Vector2.zero;

        inventoryScript.items.Add(gameObject);


    }

    public void ShowStats()
    {
        curHoverText = Instantiate(hoverText,rectTransform);

        float xPos = rectTransform.sizeDelta.x * 3 - (rectTransform.sizeDelta.x / 3);
        float yPos = rectTransform.sizeDelta.y * 2 + (rectTransform.sizeDelta.x / 3);

        curHoverText.GetComponent<RectTransform>().localPosition = new Vector2(-xPos, -yPos);

        curHoverText.GetComponent<TextTab>().AddText(alterationNames, TranslateAlteration(alteration), 8);
    }

    public void HideStats()
    {
        //Destroy(curHoverText);
        //curHoverText = null;
    }
    private List<float> TranslateAlteration(SpellStats stats)
    {
        List<float> result = new List<float>();

        result.Add(stats.damage);
        result.Add(stats.chargeTime);
        result.Add(stats.range);
        result.Add(stats.projectileSize);
        result.Add(stats.projectileSpeed);
        result.Add(stats.projectiles);
        result.Add(stats.selfDamage);
        result.Add(stats.manaCost);

        return result;
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
            if (Vector2.Distance(transform.position, curSlot.transform.position) < 30)
            {
                if(curSlot.transform.childCount != 0)
                {
                    Destroy(curSlot.transform.GetChild(0).gameObject);
                }

                FindObjectOfType<SpellController>().alterationOptions[curSlot.transform.parent.GetComponent<UISpell>().spell].Add(alteration);
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

    private void OnDestroy()
    {
        SpellController controler =  FindObjectOfType<SpellController>();

        if( controler != null )
        {
            controler.alterationOptions[transform.parent.transform.parent.GetComponent<UISpell>().spell].Remove(alteration);
        }
    }
}
