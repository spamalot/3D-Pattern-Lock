
using System.Collections.Generic;
using UnityEngine;

public class TouchTest : TechniqueClientController {
    
    public UnityEngine.UI.Extensions.UILineRenderer lr;
    public RectTransform cursor;

    void Update()
    {
        var linePts = new List<Vector2>();
        foreach (var cell in EnteredNumbers) {
            var point = Util.PixelToCanvas(PatternSharedData.Points[cell]);
            linePts.Add(point);
        }
        lr.Points = linePts.ToArray();
        cursor.anchoredPosition = CursorPosition;
    }

    public override void ChangeFeedbackEnabled(bool enabled_) {
        lr.enabled = enabled_;
    }

}
