using UnityEngine;

public class DisplayFPS : MonoBehaviour
{
  private float deltaTime = 0.0f;

  private void Start()
  {
    w = Screen.width; h = Screen.height;
    style = new GUIStyle();

    rect = new Rect(0, 0, w, h * 2 / 100);
    style.alignment = TextAnchor.UpperLeft;
    style.fontSize = h * 2 / 100;
    style.normal.textColor = new Color(0.0f, 1.0f, 0.0f, 1.0f);  // ����������ɫΪ��ɫ
  }
  void Update()
  {
    // ���㾭����ʱ��
    deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
  }
  GUIStyle style;
  Rect rect;
  int w, h;

  void OnGUI()
  {



    float fps = 1.0f / deltaTime;
    string text = string.Format("{0:0.} FPS ,spped{1}", fps, DataMgr.��ǰ��Ƶ�����ٶ�);
    GUI.Label(rect, text, style);
  }
}
