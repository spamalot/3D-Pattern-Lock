using UnityEngine;

public class PatternServerController : TechniqueServerController {

    public override void OnBeginDrag() {
        EnteredNumbers.Clear();
    }

    public override void OnDrag(Vector2 pos) {
        InvokeOnCursorPositionChanged(Util.PixelToCanvas(pos));

        var cellOrNull = NearestGridCell(pos);

        if (cellOrNull == null) {
            return;
        }

        var cell = (int)cellOrNull;

        if (!EnteredNumbers.Contains(cell)) {
            EnteredNumbers.Add(cell);
        }

        InvokeOnEnteredNumbersChanged();
    }


    /*
     * 0 1 2
     * 3 4 5
     * 6 7 8
     */

    int? NearestGridCell(Vector2 pos) {

        for (int i = 0; i < 9; i++) {
            if (Vector2.Distance(pos, PatternSharedData.Points[i]) < PatternSharedData.spacing * 0.4f) {
                return i;
            }
        }

        return null;
    }

    /*
     void OnEnd()
    {
        Commit();
    }
    */

}
