using UnityEngine;

public class LocalCoordThing : MonoBehaviour {

    public float phoneWidth = 0.8f;
    public float pixelWidth = 480;
    public float aspect = 800f / 480f;

    public float close = 0.2f;
    public float middle = 0.5f;
    public float far = 1f;

    //public enum Distance { Close, Middle, Far, Inactive };

    //public Distance distance;
    //public Vector2 posn;

    public Vector3 posn;

    void Update () {
        var unitScale = 1.0f / phoneWidth;
        var p = transform.localPosition;

        var xyz = new Vector3((p.x * unitScale + 0.5f) * pixelWidth, aspect* pixelWidth - (p.y * unitScale + 0.5f * aspect) * pixelWidth, -p.z);
        //posn = new Vector2(xyz.x, xyz.y);
        /*if (xyz.z >= 0 && xyz.z < close) {
            distance = Distance.Close;
        } else if (xyz.z >= close && xyz.z < middle) {
            distance = Distance.Middle;
        } else if (xyz.z >= middle && xyz.z < far) {
            distance = Distance.Far;
        } else {
            distance = Distance.Inactive;
        }*/
        posn = new Vector3(xyz.x, xyz.y, xyz.z * pixelWidth * unitScale);
       // Debug.Log(posn);// +" " + distance);
    }
}
