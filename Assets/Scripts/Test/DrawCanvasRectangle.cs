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
        // ����Ļ�����ת��ΪCanvas�ڵľֲ�����
        Vector2 localPointA = WorldToCanvasPosition(pointA);
        Vector2 localPointB = WorldToCanvasPosition(pointB);
        Vector2 localPointC = WorldToCanvasPosition(pointC);

        // ���þ��ε�λ�úʹ�С
        rectTransform.position = localPointA;
        rectTransform.sizeDelta = new Vector2(Vector2.Distance(localPointB, localPointC), Vector2.Distance(localPointA, localPointB));

        // �����εĳ������Ϊ����Ļ��B����C����������һ��
        Vector2 directionBC = (localPointC - localPointB).normalized;
        float angle = Mathf.Atan2(directionBC.y, directionBC.x) * Mathf.Rad2Deg;
        rectTransform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // ����Ļ����ת��ΪCanvas�ڵľֲ�����
    private Vector2 WorldToCanvasPosition(Vector3 worldPosition)
    {
        Vector2 viewportPosition = Camera.main.WorldToViewportPoint(worldPosition);
        return new Vector2(viewportPosition.x * canvasRect.sizeDelta.x, viewportPosition.y * canvasRect.sizeDelta.y);
    }
}
