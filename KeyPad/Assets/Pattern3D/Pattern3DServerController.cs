using System.Collections.Generic;
using UnityEngine;

public class Pattern3DServerController : TechniqueServerController {

    public LocalCoordThing localCoordThing;

    // FIXME TODO eventually auto clear on finish rather than this
    public override void OnBeginDrag() {
        enteredNumbers.Clear();
    }


    int? NearestGridCell3D(Vector3 pos) {

        // Split up x and y dims;
        // have z dims precisely on border, because selection should only
        // depend on xy.
        for (int i = 0; i < 27; i++) {
            var posxy = new Vector2(pos.x, pos.y);
            var actualPt = Pattern3DSharedData.Points3D[i].actualPt;
            var actualPtxy = new Vector2(actualPt.x, actualPt.y);
            if (Vector2.Distance(posxy, actualPt) < Pattern3DSharedData.spacing * 0.4f
                && Mathf.Abs(pos.z - actualPt.z) < Pattern3DSharedData.spacingZ * 0.499f) {
                return i;
            }
        }

        return null;
    }

    void Update() {

        //
        // Calculate cursor position
        //
        var viconPos3D = localCoordThing.posn;
        var viconPos2D = new Vector2(viconPos3D.x, viconPos3D.y);
        var depthOffsetScalar = (viconPos3D.z - Pattern3DSharedData.minScreenZ) / Pattern3DSharedData.spacingZ;
        var depthOffsetVector = depthOffsetScalar * Pattern3DSharedData.spacingProjDepth * new Vector2(1, -1);
        var viconPosProj = viconPos2D + depthOffsetVector;

        // FIXME TODO need new event for cursor size/color
        //textThing.text = pk.ToString();

        InvokeOnCursorPositionChanged(Util.PixelToCanvas(viconPosProj));

        // 0.5 offset to account for fact that depth offset marks
        // *boundary* of depths.
        var depthOffsetRounded = (int)(depthOffsetScalar + 0.5f);
        Depth depth;
        switch(depthOffsetRounded) {
            case 0: depth = Depth.Close; break;
            case 1: depth = Depth.Middle; break;
            case 2: depth = Depth.Far; break;
            default: depth = Depth.Inactive; break;
        }

        InvokeOnCursorDepthChanged(depth);

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

        InvokeOnEnteredNumbersChanged(enteredNumbers.ToArray());
        
    }


}
