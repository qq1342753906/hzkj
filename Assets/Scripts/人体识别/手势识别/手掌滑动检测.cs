using System;
using System.Collections.Generic;
using UnityEngine;
using Mediapipe;
using Mediapipe.Unity;
using QFramework;

public class 手掌滑动检测
{
  private float 上一帧手掌X坐标 = 0.0f;
  private float 上一帧手掌Y坐标 = 0.0f;
  private DateTime 上一帧时间;

  private const double 滑动时间阈值 = 0.1; // 滑动时间阈值，单位：秒
  private const float 滑动距离阈值 = 0.05f; // 滑动距离阈值，根据实际情况调整

  public void OnHandLandmarksOutput(object sender, OutputEventArgs<List<NormalizedLandmarkList>> eventArgs)
  {
    List<NormalizedLandmarkList> handLandmarks = eventArgs.value;
    if (handLandmarks != null)
    {
      判断并输出滑动方向(handLandmarks);
    }
  }

  private void 判断并输出滑动方向(List<NormalizedLandmarkList> 手部关键点列表)
  {
    if (判断手掌滑动方向(手部关键点列表, out bool 向左滑动, out bool 向右滑动, out bool 向下滑动, out bool 向上滑动))
    {
      if (向左滑动)
      {
        Debug.Log($"向左滑动了");
      }
      else if (向右滑动)
      {
        Debug.Log($"向右滑动了");
      }
      else if (向下滑动)
      {
        Debug.Log($"向下滑动了");

      }
      else if (向上滑动)
      {
        Debug.Log($"向上滑动了");
      }
      else
      {
        Debug.Log($"没有滑动");
      }
    }
  }
  手掌左右滑动状态 手掌左右滑动状态 = new 手掌左右滑动状态();
  手掌上下滑动状态 手掌上下滑动状态 = new 手掌上下滑动状态();
  private bool 判断手掌滑动方向(List<NormalizedLandmarkList> 手部关键点列表, out bool 向左滑动, out bool 向右滑动, out bool 向上滑动, out bool 向下滑动)
  {
    向左滑动 = false;
    向右滑动 = false;
    向上滑动 = false;
    向下滑动 = false;
    foreach (var 手部关键点 in 手部关键点列表)
    {
      if (手部关键点.Landmark.Count >= 21)
      {
        NormalizedLandmark 手掌基准点 = 手部关键点.Landmark[0];
        DateTime 当前时间 = DateTime.Now;

        if ((当前时间 - 上一帧时间).TotalSeconds >= 滑动时间阈值)
        {
          float 手掌横向移动距离 = Mathf.Abs(手掌基准点.X - 上一帧手掌X坐标);
          float 手掌纵向移动距离 = Mathf.Abs(手掌基准点.Y - 上一帧手掌Y坐标);

          if (手掌横向移动距离 >= 滑动距离阈值 || 手掌纵向移动距离 >= 滑动距离阈值)
          {
            if (手掌基准点.X > 上一帧手掌X坐标 && 手掌横向移动距离 >= 滑动距离阈值)
            {
              向右滑动 = true;
              向左滑动 = false;
              向下滑动 = false;
              向上滑动 = false;
              手掌左右滑动状态.HandType = 2;
              TypeEventSystem.Global.Send<手掌左右滑动状态>(手掌左右滑动状态);
            }
            else if (手掌基准点.X < 上一帧手掌X坐标 && 手掌横向移动距离 >= 滑动距离阈值)
            {
              向右滑动 = false;
              向左滑动 = true;
              向下滑动 = false;
              向上滑动 = false;
              手掌左右滑动状态.HandType = 1;
              TypeEventSystem.Global.Send<手掌左右滑动状态>(手掌左右滑动状态);
            }
            else if (手掌基准点.Y > 上一帧手掌Y坐标)
            {
              //向下滑动
              手掌上下滑动状态.HandType = 3;
              向右滑动 = false;
              向左滑动 = false;
              向下滑动 = true;
              向上滑动 = false;
              TypeEventSystem.Global.Send<手掌上下滑动状态>(手掌上下滑动状态);
            }
            else if (手掌基准点.Y < 上一帧手掌Y坐标)
            {
              //向上滑动
              手掌上下滑动状态.HandType = 4;
              向右滑动 = false;
              向左滑动 = false;
              向下滑动 = false;
              向上滑动 = true;
              TypeEventSystem.Global.Send<手掌上下滑动状态>(手掌上下滑动状态);
            }

          }

          上一帧手掌X坐标 = 手掌基准点.X;
          上一帧手掌Y坐标 = 手掌基准点.Y;
          上一帧时间 = 当前时间;
        }
      }
    }

    return 向左滑动 || 向右滑动 || 向上滑动 || 向下滑动;
  }
}
