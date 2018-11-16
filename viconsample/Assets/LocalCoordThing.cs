using UnityEngine;

public class LocalCoordThing : MonoBehaviour {

    public float phoneWidth = 0.8f;
    public float pixelWidth = 480;
    public float aspect = 800f / 480f;

    public float close = 0.2f;
    public float middle = 0.5f;
    public float far = 1f;

    void Update () {
        var unitScale = 1.0f / phoneWidth;
        var p = transform.localPosition;

        var xyz = new Vector3((p.x * unitScale + 0.5f) * pixelWidth, (p.y * unitScale + 0.5f * aspect) * pixelWidth, -p.z);
        var xy = new Vector2(xyz.x, xyz.y);
        string nearness;
        if (xyz.z >= 0 && xyz.z < close) {
            nearness = "close";
        } else if (xyz.z >= close && xyz.z < middle) {
            nearness = "middle";
        } else if (xyz.z >= middle && xyz.z < far) {
            nearness = "far";
        } else {
            nearness = "inactive";
        }
        Debug.Log(xy+" " + nearness);
    }
}
