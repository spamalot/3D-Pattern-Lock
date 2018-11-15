using UnityEngine;

public static class Util {

    public static readonly Vector2 pixelDimensions = new Vector2(480, 800);

    /// <summary>
    /// Assumes canvas origin top left.
    /// </summary>
    /// <returns>The canvas position.</returns>
    /// <param name="in_">In.</param>
    public static Vector2 PixelToCanvas(Vector2 in_) {
        return new Vector2(in_.x, -in_.y);
    }

    /// <summary>
    /// Assumes canvas origin top left.
    /// </summary>
    /// <returns>The pixel position.</returns>
    /// <param name="in_">In.</param>
    public static Vector2 CanvasToPixel(Vector2 in_) {
        return new Vector2(in_.x, -in_.y);
    }

}
