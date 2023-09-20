using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(图片滚动效果))]
public class 图片滚动效果Editor : Editor
{
  public override void OnInspectorGUI()
  {
    图片滚动效果 script = (图片滚动效果)target;

    script.手动设置缩放因子 = EditorGUILayout.Toggle("手动设置缩放因子", script.手动设置缩放因子);

    if (script.手动设置缩放因子)
    {
      script.缩放因子 = EditorGUILayout.FloatField("缩放因子", script.缩放因子);
    }

    DrawDefaultInspector(); // 为其他的公共变量绘制默认的Inspector
  }
}
