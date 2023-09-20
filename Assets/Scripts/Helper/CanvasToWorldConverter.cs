using QFramework;

using UnityEngine;
using UnityEngine.UI;

public class CanvasToScreenCoordinateConverter : MonoSingleton<CanvasToScreenCoordinateConverter>
{
    public Canvas canvas;
    public RectTransform targetRectTransform; // 目标UI元素的RectTransform
    Vector2 canvasSize, screenSize;
    private void Awake()
    {
        canvas = GameObject.Find("UIRoot").GetComponent<Canvas>();
        targetRectTransform = GetComponent<RectTransform>();
        // 获取Canvas的大小（以像素为单位）
        canvasSize = canvas.GetComponent<RectTransform>().sizeDelta;

        // 获取屏幕的大小（以像素为单位）
        screenSize = new Vector2(Screen.width, Screen.height);
        //TextDisplayer.DisplayText("canvasSize:", canvasSize);
        //TextDisplayer.DisplayText("screenSize:", screenSize);
    }

    public Vector3 屏幕转Canvas位置(Vector2 screenPosition)
    {
        // 计算比例关系，将屏幕位置转换为Canvas位置
        Vector3 canvasPosition = new Vector3(
            (screenPosition.x / screenSize.x) * canvasSize.x,
            (screenPosition.y / screenSize.y) * canvasSize.y,
            0
        );
        return canvasPosition;
    }
    public Vector3 比例转Canvas位置(Vector2 screenPosition)
    {
        // 计算比例关系，将屏幕位置转换为Canvas位置
        Vector3 canvasPosition = new Vector3(Mathf.Clamp(screenPosition.x * 180, -960, 960), Mathf.Clamp(screenPosition.y * 180, -540, 540), 0);
        return canvasPosition;
    }
    public Vector3 Canvas转屏幕位置(Vector3 canvasPosition)
    {

        // 计算比例关系，将Canvas位置转换为屏幕位置
        Vector2 screenPosition = new Vector2(
            (canvasPosition.x / canvasSize.x) * screenSize.x,
            (canvasPosition.y / canvasSize.y) * screenSize.y
        );
        return screenPosition;
    }
}
