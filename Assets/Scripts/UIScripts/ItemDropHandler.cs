using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropHandler : MonoBehaviour, IDropHandler
{
    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        RectTransform invPanel = transform as RectTransform;

        if (!RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition))
        {
            Debug.Log("Drop item");
        }
        //throw new System.NotImplementedException();
    }
}
