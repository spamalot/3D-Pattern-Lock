using System.Collections.Generic;
using UnityEngine;

public static class PatternSharedData {

    public static float leftOffset = 114;
    public static float topOffset = 332;
    public static float spacing = 127;

    private static List<Vector2> pointCache = null;


    public static List<Vector2> Points {
        get {
            if (pointCache == null) {
                pointCache = new List<Vector2>();
                for (int i = 0; i < 3; i++) {
                    for (int j = 0; j < 3; j++) {
                        pointCache.Add(new Vector2(leftOffset + spacing * j, topOffset + spacing * i));
                    }
                }
            }
            return pointCache;
        }
    }


}