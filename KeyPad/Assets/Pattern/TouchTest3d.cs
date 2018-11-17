
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TouchTest3d : PinTechnique {

	/*public float leftOffset = 114;
	public float topOffset = 332;
	public float spacing = 127;*/
    public GameObject pointsPattern;

    public DragController dragController;
    public UnityEngine.UI.Extensions.UILineRenderer lr;

    private List<Vector2> pts = new List<Vector2>();
    private List<Vector2> linePts = new List<Vector2>();

    public Canvas canvas;

    /*
     * 0 1 2
     * 3 4 5
     * 6 7 8
     */

    int? NearestGridCell(Vector2 pos) {

        for (int i = 0; i < 27; i++) {
            if (Vector2.Distance(pos, pts[i]) < 20) {
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

        /*pts.Clear();
        foreach (RectTransform child in pointsPattern.GetComponent<RectTransform>())
        {
            Vector2 foo;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(),
                                                                    Util.CanvasToPixel(child.position), null, out foo);
                                pts.Add(foo);
        }

        linePts.Clear();
        linePts.Add(Util.PixelToCanvas(pts[0]));
        linePts.Add(Util.PixelToCanvas(pts[1]));
        linePts.Add(Util.PixelToCanvas(pts[3]));
        linePts.Add(Util.PixelToCanvas(pts[4]));
        lr.Points = linePts.ToArray();*/

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
        /*foreach (RectTransform child in pointsPattern.GetComponent<RectTransform>())
        {
            pts.Add(Util.CanvasToPixel(child.anchoredPosition));
        }*/

           float leftOffset = 50;
            float topOffset = 438;
            float spacing = 120;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                //var k = 0;
                    pts.Add(new Vector2(leftOffset + spacing * j + k*spacing, topOffset + spacing * i - k*64));
                }
            }
        }

        Debug.Log(string.Join(",",pts.Select(x => x.ToString())));
  foreach (var p in pts) {
            Debug.Log(p);
        }



 dragController.OnPressed += OnBegin;
        dragController.OnReleased += OnEnd;
    }
	

}
