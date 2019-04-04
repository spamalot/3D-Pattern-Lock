using UnityEngine;
using UnityEngine.EventSystems;

public class TouchFollower : MonoBehaviour {

    public DragController dragController;

    void Update()
    {
        GetComponent<RectTransform>().anchoredPosition = Util.PixelToCanvas(dragController.posn);
    }

}
