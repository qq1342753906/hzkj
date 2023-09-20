using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

public class TextDisplayer : MonoBehaviour
{
    static Dictionary<string, object> 显示数据字典 = new Dictionary<string, object>();
    private static readonly object lockObject = new object();

    private static string message = string.Empty;
    private static DateTime displayStartTime;
    private static float displayDuration = 3f;
    private static float fadeDuration = 1f;
    // 静态方法，允许外部传递文本
    public static void DisplayText(string key, object text)
    {
        lock (lockObject)
        {
            addcl(key, text);
        }
    }
    public static void DisplayMessage(string newMessage)
    {
        message = newMessage;
        displayStartTime = DateTime.Now;
    }
    static void addcl(string key, object str)
    {
        if (显示数据字典.ContainsKey(key))
        {
            显示数据字典[key] = str;
        }
        else
        {
            显示数据字典.Add(key, str);
        }
    }
    int n = 0;
    private GUIStyle guiStyle = new GUIStyle(); // 创建GUIStyle对象
    private void Awake()
    {
        // 根据屏幕高度设置字体大小
        guiStyle.fontSize = (int)(Screen.height * 0.02f);
        guiStyle.normal.textColor = Color.red;
    }
    private void OnGUI()
    {
        n = 0;



        lock (lockObject)
        {
            foreach (var v in 显示数据字典)
            {
                GUI.Label(new Rect(200 + guiStyle.fontSize * 10, 10 + (n++ * (guiStyle.fontSize * 1.5f)), Screen.width - guiStyle.fontSize, Screen.height - guiStyle.fontSize), v.Key + ":" + v.Value.ToString(), guiStyle);
            }
        }
        if (!string.IsNullOrEmpty(message))
        {
            double elapsed = (DateTime.Now - displayStartTime).TotalSeconds;
            float alpha = 1f;

            if (elapsed < fadeDuration)
            {
                alpha = (float)elapsed / fadeDuration;
            }
            else if (elapsed > displayDuration - fadeDuration)
            {
                alpha = (float)((displayDuration - elapsed) / fadeDuration);
            }

            GUI.color = new Color(1, 1, 1, alpha);

            // 获取屏幕尺寸
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;

            // 创建一个样式来居中文本并设置字体大小
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.alignment = TextAnchor.UpperCenter;
            style.fontSize = Screen.height / 30;  // 设置字体大小为屏幕高度的10分之一

            // 计算文本的位置，使其显示在屏幕中心下方
            Rect rect = new Rect(0, screenHeight - 100, screenWidth, 100);  // 将矩形的宽度设置为屏幕的宽度
            // 显示文本
            GUI.Label(rect, message, style);

            if (elapsed > displayDuration)
            {
                message = string.Empty;
            }
        }
    }
}
