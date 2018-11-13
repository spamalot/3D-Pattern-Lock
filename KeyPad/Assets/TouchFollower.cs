using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchFollower : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    public Canvas canvas;

	// Update is called once per frame
	void Update () {
        var r = canvas.GetComponent<RectTransform>();
        Vector2 x;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(r, Input.mousePosition, null, out x);
        Debug.Log(x - new Vector2(-240, 400));
        GetComponent<RectTransform>().anchoredPosition = x;
	}

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("down");
    }

    public void OnDrag(PointerEventData data)
    {
        //Debug.Log("drag"+data.position);
        //GetComponent<RectTransform>().position = data.position;
    }

  
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("up");
    }
}
