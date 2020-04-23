using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class HoverBehaviour : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    public Color startColor;
    public Color hoverColor;

    Image image;
    void Start() {
      image = GetComponent<Image>();
    }
    public void OnPointerEnter(PointerEventData eventData)
     {
         Debug.Log("Mouse enter");
         image.color = hoverColor;
     }
 
     public void OnPointerExit(PointerEventData eventData)
     {
         Debug.Log("Mouse exit");
         image.color = startColor;
     }
}
