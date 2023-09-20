using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mediapipe.Unity.HandTracking;
using Mediapipe.Unity;
using Mediapipe;
using System;

public class HandSpeedDetector
{
  public HandSpeedDetector()
  {
  }

  // 手部关键点回调函数，绘制手部关键点注释
  //public static void OnHandLandmarksOutput(object sender, OutputEventArgs<List<NormalizedLandmarkList>> eventArgs)
  //{
  //    List<NormalizedLandmarkList> handLandmarks = eventArgs.value;
  //    Debug.Log(handLandmarks==null);
  //}
  // 用于存储先前的手部关键点和时间
  private List<NormalizedLandmarkList> previousLandmarks = new List<NormalizedLandmarkList>();
  private List<float> previousTimes = new List<float>();

  #region 速度
  //// 手部关键点输出的回调函数
  //public void OnHandLandmarksOutput(object sender, OutputEventArgs<List<NormalizedLandmarkList>> eventArgs)
  //{
  //    List<NormalizedLandmarkList> handLandmarks = eventArgs.value;
  //    if (handLandmarks == null)
  //    {
  //        return;
  //    }
  //    if (IsFistGesture(handLandmarks))
  //    {
  //        Debug.Log($"Hand 1 is in fist gesture.");
  //    }
  //    else
  //    {
  //        Debug.Log($"Hand 2 is not in fist gesture.");
  //    }
  //    // 遍历检测到的所有手部
  //    for (int i = 0; i < handLandmarks.Count; i++)
  //    {

  //        if (handLandmarks[i].Landmark.Count > 0)
  //        {
  //            Vector3 currentPosition = new Vector3(handLandmarks[i].Landmark[0].X, handLandmarks[i].Landmark[0].Y, handLandmarks[i].Landmark[0].Z);
  //            float currentTime = DateTime.Now.Millisecond;

  //            if (previousLandmarks.Count > i && previousLandmarks[i] != null)
  //            {
  //                Vector3 positionDelta = currentPosition - GetPreviousPosition(i);
  //                float timeDelta = currentTime - GetPreviousTime(i);

  //                Vector3 velocity = positionDelta / timeDelta;

  //                Debug.Log($"Hand {i + 1} Speed: {(velocity*100).magnitude}");
  //            }

  //            UpdatePreviousLandmarks(i, handLandmarks[i]);
  //            UpdatePreviousTime(i, currentTime);
  //        }
  //    }
  //}

  //// 获取先前的位置
  //private Vector3 GetPreviousPosition(int index)
  //{
  //    return new Vector3(previousLandmarks[index].Landmark[0].X, previousLandmarks[index].Landmark[0].Y, previousLandmarks[index].Landmark[0].Z);
  //}

  //// 获取先前的时间
  //private float GetPreviousTime(int index)
  //{
  //    return previousTimes[index];
  //}

  //// 更新先前的手部关键点
  //private void UpdatePreviousLandmarks(int index, NormalizedLandmarkList landmarks)
  //{
  //    if (previousLandmarks.Count > index)
  //    {
  //        previousLandmarks[index] = landmarks;
  //    }
  //    else
  //    {
  //        previousLandmarks.Add(landmarks);
  //    }
  //}

  //// 更新先前的时间
  //private void UpdatePreviousTime(int index, float time)
  //{
  //    if (previousTimes.Count > index)
  //    {
  //        previousTimes[index] = time;
  //    }
  //    else
  //    {
  //        previousTimes.Add(time);
  //    }
  //}

  //// 判断手是否握拳的方法
  //private bool IsFistGesture(List<NormalizedLandmarkList> handLandmarks2)
  //{
  //    // 根据手部关键点数据判断是否握拳
  //    // 这里可以根据手部关节角度、手指弯曲度等信息来判断是否握拳
  //    // 以下示例代码仅用于演示，具体的判断逻辑可能需要更复杂的处理
  //    bool isFist = false;
  //    foreach(var handLandmarks in handLandmarks2) 
  //    // 假设只判断大拇指和食指的位置
  //    if (handLandmarks.Landmark.Count >= 2)
  //    {
  //        Vector3 thumbPosition = new Vector3(handLandmarks.Landmark[0].X, handLandmarks.Landmark[0].Y, handLandmarks.Landmark[0].Z);
  //        Vector3 indexFingerPosition = new Vector3(handLandmarks.Landmark[8].X, handLandmarks.Landmark[8].Y, handLandmarks.Landmark[8].Z);

  //        // 假设当大拇指和食指距离很近时判定为握拳
  //        float distanceThreshold = 0.05f; // 距离阈值可根据实际情况调整
  //        float distance = Vector3.Distance(thumbPosition, indexFingerPosition);

  //        if (distance < distanceThreshold)
  //        {
  //            isFist = true;
  //        }
  //    }

  //    return isFist;
  //}

  #endregion
  public void OnHandLandmarksOutput(object sender, OutputEventArgs<List<NormalizedLandmarkList>> eventArgs)
  {
    List<NormalizedLandmarkList> handLandmarks = eventArgs.value;
    if (handLandmarks != null)
    {
      Debug.Log("handLandmarks长度是:" + handLandmarks.Count);
      if (判断握拳手势(handLandmarks))
      {
        Debug.Log($"Hand 1 is in complex gesture.");
      }
      else
      {
        Debug.Log($"Hand 2 is not in complex gesture.");
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

        float 大拇指指根距离 = Vector2.Distance(new Vector2(手掌基准点.X, 手掌基准点.Y), new Vector2(手部关键点.Landmark[2].X, 手部关键点.Landmark[2].Y));
        float 食指指根距离 = Vector2.Distance(new Vector2(手掌基准点.X, 手掌基准点.Y), new Vector2(手部关键点.Landmark[6].X, 手部关键点.Landmark[6].Y));
        float 中指指根距离 = Vector2.Distance(new Vector2(手掌基准点.X, 手掌基准点.Y), new Vector2(手部关键点.Landmark[10].X, 手部关键点.Landmark[10].Y));
        float 无名指指根距离 = Vector2.Distance(new Vector2(手掌基准点.X, 手掌基准点.Y), new Vector2(手部关键点.Landmark[14].X, 手部关键点.Landmark[14].Y));
        float 小指指根距离 = Vector2.Distance(new Vector2(手掌基准点.X, 手掌基准点.Y), new Vector2(手部关键点.Landmark[18].X, 手部关键点.Landmark[18].Y));

        // 判断手指指尖和手指指根哪个更接近手掌
        bool 大拇指更近 = 大拇指指尖距离 < 大拇指指根距离;
        bool 食指更近 = 食指指尖距离 < 食指指根距离;
        bool 中指更近 = 中指指尖距离 < 中指指根距离;
        bool 无名指更近 = 无名指指尖距离 < 无名指指根距离;
        bool 小指更近 = 小指指尖距离 < 小指指根距离;

        // 判断是否握拳
        int 更近的手指数量 = 0;

        if (大拇指更近) 更近的手指数量++;
        if (食指更近) 更近的手指数量++;
        if (中指更近) 更近的手指数量++;
        if (无名指更近) 更近的手指数量++;
        if (小指更近) 更近的手指数量++;

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
