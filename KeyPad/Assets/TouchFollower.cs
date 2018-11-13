using UnityEngine;

public class TouchFollower : MonoBehaviour {

    public Canvas canvas;

    // Changing this has no effect, only for reading purposes
    public Vector2 posn;

    void Update () {
        var r = canvas.GetComponent<RectTransform>();
        Vector2 x;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(r, Input.mousePosition, null, out x);
        posn = x - new Vector2(-240, 400);
        GetComponent<RectTransform>().anchoredPosition = x;
    }
}
