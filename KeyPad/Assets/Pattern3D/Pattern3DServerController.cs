using System.Collections.Generic;
using UnityEngine;

public class Pattern3DServerController : TechniqueServerController {

    public LocalCoordThing localCoordThing;

    // FIXME TODO eventually auto clear on finish rather than this
    public override void OnBeginDrag() {
        enteredNumbers.Clear();
    }


    int? NearestGridCell3D(Vector3 pos) {

        for (int i = 0; i < 27; i++) {
            if (Vector3.Distance(pos, Pattern3DSharedData.Points3D[i].actualPt) < 50) {
                return i;
            }
        }

        return null;
    }

    // FIXME: get rid of this when localCoordThingFixed
    void Start() {
        localCoordThing = GameObject.Find("ThumbTest").GetComponent<LocalCoordThing>();
    }

    void Update() {

        //
        // Calculate cursor position
        //
        var viconPos3D = localCoordThing.posn;
        var viconPos2D = new Vector2(viconPos3D.x, viconPos3D.y);
        var depthOffsetScalar = (viconPos3D.z - Pattern3DSharedData.minScreenZ) / Pattern3DSharedData.spacing;
        var depthOffsetVector = depthOffsetScalar * Pattern3DSharedData.spacingProjDepth * new Vector2(1, -1);
        var viconPosProj = viconPos2D + depthOffsetVector;

        // FIXME TODO need new event for cursor size/color
        //textThing.text = pk.ToString();


        //
        // Calculate pin
        //
        var cellOrNull = NearestGridCell3D(localCoordThing.posn);

        if (cellOrNull == null) {
            return;
        }

        var gc = (int)cellOrNull;

        if (!enteredNumbers.Contains(gc)) {
            enteredNumbers.Add(gc);

        }

        //
        // Send to client
        //
        InvokeOnEnteredNumbersChanged(enteredNumbers.ToArray());
        InvokeOnCursorPositionchanged(Util.PixelToCanvas(viconPosProj));
    }


}
