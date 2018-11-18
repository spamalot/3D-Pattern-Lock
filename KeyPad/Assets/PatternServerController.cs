using System.Collections.Generic;
using UnityEngine;

public class PatternServerController : TechniqueServerController {

    public override void OnBeginDrag() {
        enteredNumbers.Clear();
    }

    public override void OnDrag(Vector2 pos) {
        var cellOrNull = NearestGridCell(pos);

        if (cellOrNull == null) {
            return;
        }

        var cell = (int)cellOrNull;

        if (!enteredNumbers.Contains(cell)) {
            enteredNumbers.Add(cell);
        }

        InvokeOnEnteredNumbersChanged(enteredNumbers.ToArray());
        InvokeOnCursorPositionchanged(Util.PixelToCanvas(pos));
    }


    /*
     * 0 1 2
     * 3 4 5
     * 6 7 8
     */

    int? NearestGridCell(Vector2 pos) {

        for (int i = 0; i < 9; i++) {
            if (Vector2.Distance(pos, PatternSharedData.Points[i]) < 50) {
                return i;
            }
        }

        return null;
    }

    void Update() {
        
    }

    /*
     void OnEnd()
    {
        Commit();
    }
    */

}
