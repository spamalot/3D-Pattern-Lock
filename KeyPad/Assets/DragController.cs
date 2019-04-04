using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Vector2 posn;
    public bool pressed = false;

    public event Action OnPressed;
    public event Action OnReleased;
    public event Action<Vector2> OnDragged;

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        pressed = true;
        OnPressed?.Invoke();
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        var r = GetComponent<RectTransform>();
        var r2 = r.rect.size;
        Vector2 x;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(r, eventData.position, null, out x);
        x += new Vector2(r2.x, -r2.y) / 2;
        posn = Util.CanvasToPixel(x);
        OnDragged?.Invoke(posn);

    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        pressed = false;
        OnReleased?.Invoke();
    }
}
