using UnityEngine;
using UnityEngine.UI;

public class DrawCanvasRectangle : MonoBehaviour
{
    public RectTransform canvasRect;
    public RectTransform rectTransform;

    public Vector3 pointA;
    public Vector3 pointB;
    public Vector3 pointC;

    private void Update()
    {
        // 将屏幕坐标点转换为Canvas内的局部坐标
        Vector2 localPointA = WorldToCanvasPosition(pointA);
        Vector2 localPointB = WorldToCanvasPosition(pointB);
        Vector2 localPointC = WorldToCanvasPosition(pointC);

        // 设置矩形的位置和大小
        rectTransform.position = localPointA;
        rectTransform.sizeDelta = new Vector2(Vector2.Distance(localPointB, localPointC), Vector2.Distance(localPointA, localPointB));

        // 将矩形的朝向调整为与屏幕点B到点C的向量方向一致
        Vector2 directionBC = (localPointC - localPointB).normalized;
        float angle = Mathf.Atan2(directionBC.y, directionBC.x) * Mathf.Rad2Deg;
        rectTransform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // 将屏幕坐标转换为Canvas内的局部坐标
    private Vector2 WorldToCanvasPosition(Vector3 worldPosition)
    {
        Vector2 viewportPosition = Camera.main.WorldToViewportPoint(worldPosition);
        return new Vector2(viewportPosition.x * canvasRect.sizeDelta.x, viewportPosition.y * canvasRect.sizeDelta.y);
    }
}
