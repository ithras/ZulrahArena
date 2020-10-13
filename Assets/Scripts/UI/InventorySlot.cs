using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Canvas canvas;
    public GameObject ItemPrefab;
    Item item;
    GameObject itemGO;

    public void AddItem(Item newItem)
    {
        if (item != null)
            return;

        itemGO = Instantiate(ItemPrefab);
        itemGO.transform.SetParent(transform);
        itemGO.transform.localScale = Vector3.one;
        itemGO.transform.localPosition  = new Vector3(20f, -20f, 0f);
        itemGO.GetComponent<DragDrop>().canvas = canvas;
        Image icon = itemGO.GetComponentInChildren<Image>();

        item = newItem;
        icon.sprite = item.icon;
        icon.color = new Color(1f, 1f, 1f, 1f);
    }

    public void ClearSlot ()
    {
        item = null;
        Destroy(itemGO);
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use(true);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("On Drop");
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = new Vector2(20f, -20f);
            //eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            //eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
        }
    }
}
