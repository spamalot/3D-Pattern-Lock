using System.Collections.Generic;
using UnityEngine;

public static class Pattern3DSharedData {

    public static float leftOffset = 50;
    public static float topOffset = 438;
    public static float spacing = 120;
    public static float spacingProjDepth = 40;
    public static float minScreenZ = 200;


    public class ProjPoint {
        public Vector2 pt;
        public Vector3 actualPt;
    }

    private static List<ProjPoint> pointCache3D = null;

    public static List<ProjPoint> Points3D {
        get {
            if (pointCache3D == null) {
                pointCache3D = new List<ProjPoint>();
                for (int i = 0; i < 3; i++) {
                    for (int j = 0; j < 3; j++) {
                        for (int k = 0; k < 3; k++) {
                            pointCache3D.Add(
                                new ProjPoint {
                                    pt = new Vector2(leftOffset + spacing * j + k * spacingProjDepth, topOffset + spacing * i - k * spacingProjDepth),
                                    actualPt = new Vector3(leftOffset + spacing * j, topOffset + spacing * i, minScreenZ + k * spacing)
                                });
                        }
                    }
                }
            }
            return pointCache3D;
        }
    }
}
