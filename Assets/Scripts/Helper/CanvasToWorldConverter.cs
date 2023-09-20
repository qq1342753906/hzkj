using QFramework;

using UnityEngine;
using UnityEngine.UI;

public class CanvasToScreenCoordinateConverter : MonoSingleton<CanvasToScreenCoordinateConverter>
{
    public Canvas canvas;
    public RectTransform targetRectTransform; // Ŀ��UIԪ�ص�RectTransform
    Vector2 canvasSize, screenSize;
    private void Awake()
    {
        canvas = GameObject.Find("UIRoot").GetComponent<Canvas>();
        targetRectTransform = GetComponent<RectTransform>();
        // ��ȡCanvas�Ĵ�С��������Ϊ��λ��
        canvasSize = canvas.GetComponent<RectTransform>().sizeDelta;

        // ��ȡ��Ļ�Ĵ�С��������Ϊ��λ��
        screenSize = new Vector2(Screen.width, Screen.height);
        //TextDisplayer.DisplayText("canvasSize:", canvasSize);
        //TextDisplayer.DisplayText("screenSize:", screenSize);
    }

    public Vector3 ��ĻתCanvasλ��(Vector2 screenPosition)
    {
        // ���������ϵ������Ļλ��ת��ΪCanvasλ��
        Vector3 canvasPosition = new Vector3(
            (screenPosition.x / screenSize.x) * canvasSize.x,
            (screenPosition.y / screenSize.y) * canvasSize.y,
            0
        );
        return canvasPosition;
    }
    public Vector3 ����תCanvasλ��(Vector2 screenPosition)
    {
        // ���������ϵ������Ļλ��ת��ΪCanvasλ��
        Vector3 canvasPosition = new Vector3(Mathf.Clamp(screenPosition.x * 180, -960, 960), Mathf.Clamp(screenPosition.y * 180, -540, 540), 0);
        return canvasPosition;
    }
    public Vector3 Canvasת��Ļλ��(Vector3 canvasPosition)
    {

        // ���������ϵ����Canvasλ��ת��Ϊ��Ļλ��
        Vector2 screenPosition = new Vector2(
            (canvasPosition.x / canvasSize.x) * screenSize.x,
            (canvasPosition.y / canvasSize.y) * screenSize.y
        );
        return screenPosition;
    }
}
