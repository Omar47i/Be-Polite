using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIUtil : MonoBehaviour {

    private RectTransform CanvasRect;

    void Start()
    {
        CanvasRect = GetComponent<RectTransform>();
    }

    public Vector2 WorldToCanvasPoint(Camera cam, Vector3 WorldObject)
    {
        Vector2 ViewportPosition = cam.WorldToViewportPoint(WorldObject);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

        return WorldObject_ScreenPosition;
    }
}
