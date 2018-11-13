using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchTest : MonoBehaviour {

	public float leftOffset = 114;
	public float topOffset = 332;
	public float spacing = 127;

	public Camera c;
	public TouchFollower touchFollower;
    public UnityEngine.UI.Extensions.UILineRenderer lr;

    private List<Vector2> pts = new List<Vector2>();

    private List<Vector2> linePts = new List<Vector2>();
    private List<int> usedCells = new List<int>();

    /*
     * 0 1 2
     * 3 4 5
     * 6 7 8
     */

    int? NearestGridCell(Vector2 pos) {

        pos = new Vector2(pos.x, -pos.y);// - new Vector2(leftOffset, topOffset);


        for (int i = 0; i < 9; i++) {
            //Debug.Log(pos+" " +pts[i]);
            //return null;
            if (Vector2.Distance(pos, pts[i]) < 50) {
                return i;//Debug.Log(i);
            }
        }

        return null;

    }



	void Start () {
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                pts.Add(new Vector2(leftOffset + spacing * j, topOffset + spacing * i));
            }
        }
        foreach (var p in pts) {
            Debug.Log(p);
        }
	}
	
	void Update () {
        //if (touchFollower.posn.x > leftOffset-30)
        var mousePressed = Input.GetMouseButton(0);



        if (mousePressed) {
            if (Input.GetMouseButtonDown(0)) {
                linePts.Clear();
                usedCells.Clear();
            }

            var gcx = NearestGridCell(touchFollower.posn);

            if (gcx == null) {
                return;
            }

            var gc = (int)gcx;

            if (!usedCells.Contains(gc)) {
                usedCells.Add(gc);
                var foo = new Vector2(pts[gc].x, -pts[gc].y) + new Vector2(-240, 400);
                linePts.Add(foo);
            }

            lr.Points = linePts.ToArray();
        }

	}
}
