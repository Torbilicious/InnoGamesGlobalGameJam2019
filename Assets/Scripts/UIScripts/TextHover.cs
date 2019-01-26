using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Text theText;
    Color newColor = new Color(1f, 0.6f, 0.5f, 1f);
    public void OnPointerEnter(PointerEventData eventData)
    {
        theText.color = newColor; //Or however you do your color
    }

        public void OnPointerExit(PointerEventData eventData)
        {
            theText.color = Color.white; //Or however you do your color
        }
}