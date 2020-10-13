using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Transform inventorySlot;
    private Vector3 startPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        inventorySlot = transform.parent;
        canvasGroup.blocksRaycasts = false;
        startPosition = transform.position;
        transform.SetParent(canvas.transform);
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        if (transform.parent == canvas.transform)
        {
            transform.position = startPosition;
            transform.SetParent(inventorySlot);
        }
            

    }
    public void OnPointerDown(PointerEventData eventData)
    {

    }
}
