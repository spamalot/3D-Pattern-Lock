
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TouchTest3d : TechniqueClientController {

    /*public float leftOffset = 114;
	public float topOffset = 332;
	public float spacing = 127;*/

    /*
     * 0 1 2
     * 3 4 5
     * 6 7 8
     */

        /*

    public GameObject pointsPattern;

    public UnityEngine.UI.Extensions.UILineRenderer lr;
    public RectTransform cursor;
    public UnityEngine.UI.Text textThing;

    public LocalCoordThing localCoordThing;

    private class Thing {
        public Vector2 pt;
        public Vector3 actualPt;
    }

    private List<Thing> pts = new List<Thing>();
    private List<Vector2> linePts = new List<Vector2>();


    int? NearestGridCell(Vector3 pos) {

        for (int i = 0; i < 27; i++) {
            if (Vector3.Distance(pos, pts[i].actualPt) < 50) {
                return i;
            }
        }

        return null;
    }

    void OnBegin()
    {
        linePts.Clear();
        numsSoFar.Clear();
    }

    void Update()
    {

        var pp = localCoordThing.posn;
        var ppxy = new Vector2(pp.x, pp.y);
        var pk = (pp.z - 200f) / 120f;
        textThing.text = pk.ToString();
        ppxy += new Vector2(pk*40, -pk*40);
        cursor.anchoredPosition = Util.PixelToCanvas(ppxy);


        var gcx = NearestGridCell(localCoordThing.posn);

        if (gcx == null) {
            return;
        }

        var gc = (int)gcx;

        if (!numsSoFar.Contains(gc)) {
            numsSoFar.Add(gc);
            var foo = Util.PixelToCanvas(pts[gc].pt);
            linePts.Add(foo);
        }



        lr.Points = linePts.ToArray();
    }

    void OnEnd()
    {
        Commit();
    }

    protected override void Start () {
        base.Start();

        float leftOffset = 50;
        float topOffset = 438;
        float spacing = 120;


        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                for (int k = 0; k < 3; k++) {
                    pts.Add(
                        new Thing {
                            pt = new Vector2(leftOffset + spacing * j + k*40, topOffset + spacing * i - k*40),
                            actualPt = new Vector3(leftOffset + spacing * j, topOffset + spacing * i, 200+ k * spacing) });
                }
            }
        }

        Debug.Log(string.Join(",",pts.Select(x => x.ToString())));
        foreach (var p in pts) {
            Debug.Log(p);
        }


    // TODO TODO!!!! end pin after four numbers reached

    }
    */
	

}
