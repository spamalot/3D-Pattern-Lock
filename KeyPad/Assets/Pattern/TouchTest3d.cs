
using System.Collections.Generic;
using UnityEngine;

public class TouchTest3d : PinTechnique {

	public float leftOffset = 114;
	public float topOffset = 332;
	public float spacing = 127;
    public GameObject pointsPattern;

    public DragController dragController;
    public UnityEngine.UI.Extensions.UILineRenderer lr;

    private List<Vector2> pts = new List<Vector2>();
    private List<Vector2> linePts = new List<Vector2>();

    /*
     * 0 1 2
     * 3 4 5
     * 6 7 8
     */

    int? NearestGridCell(Vector2 pos) {

        for (int i = 0; i < 27; i++) {
            if (Vector2.Distance(pos, pts[i]) < 30) {
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
        var gcx = NearestGridCell(dragController.posn);

        if (gcx == null)
        {
            return;
        }

        var gc = (int)gcx;

        if (!numsSoFar.Contains(gc))
        {
            numsSoFar.Add(gc);
            var foo = Util.PixelToCanvas(pts[gc]);
            linePts.Add(foo);
        }

        lr.Points = linePts.ToArray();
    }

    void OnEnd()
    {
        Commit();
    }

    void Start () {
        foreach (Transform child in pointsPattern.transform)
        {
            pts.Add(new Vector2(child.position.x, child.position.y));
        }


        foreach (var p in pts) {
            Debug.Log(p);
        }

        dragController.OnPressed += OnBegin;
        dragController.OnReleased += OnEnd;
    }
	

}
