
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TouchTest3d : TechniqueClientController {

    public UnityEngine.UI.Extensions.UILineRenderer lr;
    public RectTransform cursor;

    public Sprite closeCursor;
    public Sprite middleCursor;
    public Sprite farCursor;
    public Sprite inactiveCursor;

    void Update()
    {
        var linePts = new List<Vector2>();
        foreach (var cell in EnteredNumbers) {
            var point = Util.PixelToCanvas(Pattern3DSharedData.Points3D[cell].pt);
            linePts.Add(point);
        }
        lr.Points = linePts.ToArray();
        cursor.anchoredPosition = CursorPosition;

        Sprite cursorSprite;
        switch (CursorDepth) {
            case TechniqueServerController.Depth.Close: cursorSprite = closeCursor; break;
            case TechniqueServerController.Depth.Middle: cursorSprite = middleCursor; break;
            case TechniqueServerController.Depth.Far: cursorSprite = farCursor; break;
            default: cursorSprite = inactiveCursor; break;
        }
        cursor.GetComponent<UnityEngine.UI.Image>().sprite = cursorSprite;

    }


   /* void OnEnd()
    {
        Commit();
    }*/


    // TODO TODO!!!! end pin after four numbers reached

}
