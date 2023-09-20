using Mediapipe.Unity;
using Mediapipe;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using QFramework.Example;

public class 握拳 : MonoBehaviour
{
  /// <summary>
  /// 传入手掌数据
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="eventArgs"></param>
  public void OnHandLandmarksOutput(object sender, OutputEventArgs<List<NormalizedLandmarkList>> eventArgs)
  {
    if (!DataMgr.手掌张开状态)
    {
      // De.Log("手掌未处于张开状态，不检测握拳操作");
      return;
    }
    //if (!DataMgr.人体是否在中间)
    //{
    //    return;
    //}
    List<NormalizedLandmarkList> handLandmarks = eventArgs.value;
    if (handLandmarks != null)
    {
      // De.Log("handLandmarks长度是:" + handLandmarks.Count);
      if (判断握拳手势(handLandmarks))
      {
        Debug.LogWarning("握拳了");
        ////TextDisplayer.DisplayText("当前是否握拳了:", true);
        TypeEventSystem.Global.Send<握拳选择>();
        ThreadToMain.ToMain(1, () =>
        {

        });

      }
      else
      {
        ////TextDisplayer.DisplayText("当前是否握拳了:", false);
      }
    }
    // 遍历检测到的所有手部
    //for (int i = 0; i < handLandmarks.Count; i++)
    //{

    //}
  }

  private bool 判断握拳手势(List<NormalizedLandmarkList> 手部关键点列表)
  {
    bool 是否握拳 = false;

    foreach (var 手部关键点 in 手部关键点列表)
    {
      if (手部关键点.Landmark.Count >= 21)
      {
        // 获取手指关键点和手掌基准点
        NormalizedLandmark 大拇指指尖 = 手部关键点.Landmark[4];
        NormalizedLandmark 食指指尖 = 手部关键点.Landmark[8];
        NormalizedLandmark 中指指尖 = 手部关键点.Landmark[12];
        NormalizedLandmark 无名指指尖 = 手部关键点.Landmark[16];
        NormalizedLandmark 小指指尖 = 手部关键点.Landmark[20];
        NormalizedLandmark 手掌基准点 = 手部关键点.Landmark[0];

        // 计算手指指尖和手指指根与手掌基准点的距离
        float 大拇指指尖距离 = Vector2.Distance(new Vector2(手掌基准点.X, 手掌基准点.Y), new Vector2(大拇指指尖.X, 大拇指指尖.Y));
        float 食指指尖距离 = Vector2.Distance(new Vector2(手掌基准点.X, 手掌基准点.Y), new Vector2(食指指尖.X, 食指指尖.Y));
        float 中指指尖距离 = Vector2.Distance(new Vector2(手掌基准点.X, 手掌基准点.Y), new Vector2(中指指尖.X, 中指指尖.Y));
        float 无名指指尖距离 = Vector2.Distance(new Vector2(手掌基准点.X, 手掌基准点.Y), new Vector2(无名指指尖.X, 无名指指尖.Y));
        float 小指指尖距离 = Vector2.Distance(new Vector2(手掌基准点.X, 手掌基准点.Y), new Vector2(小指指尖.X, 小指指尖.Y));
        List<float> f = new List<float>();
        f.Add(手部关键点.Landmark[5].Y);
        f.Add(手部关键点.Landmark[9].Y);
        f.Add(手部关键点.Landmark[13].Y);
        f.Add(手部关键点.Landmark[17].Y);
        // 判断是否握拳
        int 更近的手指数量 = 0;
        // 判断手指指尖和手指指根哪个更接近手掌
        //De.Log("大拇指指尖.X" + 大拇指指尖.X + 手部关键点.Landmark[2].X);
        bool 大拇指更近 = 大拇指指尖.X > 手部关键点.Landmark[2].X;
        for (int n = 8; n <= 20;)
        {
          bool ss = true;
          foreach (var v in f)
          {
            if (手部关键点.Landmark[n].Y < v)
            {
              Debug.LogWarning("这边不行:" + n + ":" + 手部关键点.Landmark[n].Y + "其他的手势关节点Y" + v);
              ////TextDisplayer.DisplayText("握拳手指不能识别的手指序号:", n);
              ss = false;
              break;
            }
          }
          if (ss)
          {
            更近的手指数量++;
          }
          n += 4;
        }

        if (大拇指更近) 更近的手指数量++;
        // De.Log("更近的手指数量" + 更近的手指数量);
        ////TextDisplayer.DisplayText("握拳手指识别数量:", 更近的手指数量);
        if (更近的手指数量 >= 3)
        {
          是否握拳 = true;
          break;
        }
      }
    }

    return 是否握拳;
  }
}
