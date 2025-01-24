using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextTab : MonoBehaviour
{
    [SerializeField] private GameObject textObject;
    private int amountOfTekst = 0;

    [SerializeField] private float offSet;

    private float startPointX;
    private float startPointY;

    private RectTransform rectTransform;
    public RectTransform tectObjRectTransform;

    private void Start()
    {

    }

    public void AddText(string[] description,List<float> value,int amount)
    {
        rectTransform = GetComponent<RectTransform>();
        tectObjRectTransform = textObject.GetComponent<RectTransform>();

        startPointX = rectTransform.sizeDelta.x / 2;
        startPointY = rectTransform.sizeDelta.y / 2.5f;


        for (int i = 0; i < amount; i++)
        {
            Vector2 pos = new Vector2(startPointX + offSet, startPointY - (offSet * amountOfTekst) - (tectObjRectTransform.sizeDelta.y * amountOfTekst));

            GameObject go = Instantiate(textObject, transform);
            go.transform.localPosition = pos;

            go.GetComponent<TMP_Text>().text = description[i] + "=" + value[i];

            amountOfTekst++;
        }
    }
}
