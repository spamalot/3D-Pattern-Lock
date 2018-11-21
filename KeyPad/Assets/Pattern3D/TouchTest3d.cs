
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TouchTest3d : TechniqueClientController {

    public UnityEngine.UI.Extensions.UILineRenderer lr;
    public RectTransform cursor;

    // FIXME: TODO
    public UnityEngine.UI.Text textThing;

    void Update()
    {
        var linePts = new List<Vector2>();
        foreach (var cell in EnteredNumbers) {
            var point = Util.PixelToCanvas(Pattern3DSharedData.Points3D[cell].pt);
            linePts.Add(point);
        }
        lr.Points = linePts.ToArray();
        cursor.anchoredPosition = CursorPosition;
    }


   /* void OnEnd()
    {
        Commit();
    }*/


    // TODO TODO!!!! end pin after four numbers reached

}
