using Mediapipe;
using Mediapipe.Unity.PoseTracking;

using QFramework;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

using UnityEditor;

using UnityEngine;

public class 姿势识别手掌滑动检测
{

  public 姿势识别手掌滑动检测()
  {
    TypeEventSystem.Global.Register<姿势识别数据解析>(姿势识别数据解析);
    TypeEventSystem.Global.Register<姿势手掌轨迹重置>(姿势手掌轨迹重置);
  }
  void 姿势识别数据解析(姿势识别数据解析 e)
  {
    //0 - 鼻子
    //1 - 左眼（内）
    //2 - 左眼
    //3 - 左眼（外）
    //4 - 右眼（内）
    //5 - 右眼
    //6 - 右眼（外）
    //7 - 左耳
    //8 - 右耳
    //9 - 嘴（左）
    //10 - 嘴（右）
    //11 - 左肩
    //12 - 右肩
    //13 - 左肘
    //14 - 右肘
    //15 - 左手腕
    //16 - 右手腕
    //17 - 左小指
    //18 - 右小指
    //19 - 左索引
    //20 - 右索引
    //21 - 左拇指
    //22 - 右手拇指
    //23 - 左髋
    //24 - 右髋
    //25 - 左膝
    //26 - 右膝
    //27 - 左脚踝
    //28 - 右脚踝
    //29 - 左脚跟
    //30 - 右脚跟
    //31 - 左脚索引
    //32 - 右脚索引

    var poseLandmarkList = e.nl;
    DataMgr.鼻子X = poseLandmarkList.Landmark[0].X;
    ////TextDisplayer.DisplayText("人体是否在中间:", DataMgr.人体是否在中间);
    ////TextDisplayer.DisplayText("左小指:", poseLandmarkList.Landmark[17].X);
    ////TextDisplayer.DisplayText("左拇指:", poseLandmarkList.Landmark[21].X);
    if (!DataMgr.人体是否在中间)
    {

      return;
    }
    /*
    //0 - 鼻子
    NormalizedLandmark 鼻子 = poseLandmarkList.Landmark[0];
    //1 - 左眼(内侧)
    NormalizedLandmark 左眼内侧 = poseLandmarkList.Landmark[1];
    //2 - 左眼
    NormalizedLandmark 左眼 = poseLandmarkList.Landmark[2];
    //3 - 左眼(外侧)
    NormalizedLandmark 左眼外侧 = poseLandmarkList.Landmark[3];
    //4 - 右眼(内侧)
    NormalizedLandmark 右眼内侧 = poseLandmarkList.Landmark[4];
    //5 - 右眼
    NormalizedLandmark 右眼 = poseLandmarkList.Landmark[5];
    //6 - 右眼(外侧)
    NormalizedLandmark 右眼外侧 = poseLandmarkList.Landmark[6];
    //7 - 左耳
    NormalizedLandmark 左耳 = poseLandmarkList.Landmark[7];
    //8 - 右耳
    NormalizedLandmark 右耳 = poseLandmarkList.Landmark[8];
    //9 - 嘴巴(左侧)
    NormalizedLandmark 嘴巴左侧 = poseLandmarkList.Landmark[9];
    //10 - 嘴巴(右侧)
    NormalizedLandmark 嘴巴右侧 = poseLandmarkList.Landmark[10];

    //13 - 左肘
    NormalizedLandmark 左肘 = poseLandmarkList.Landmark[13];
    //14 - 右肘
    NormalizedLandmark 右肘 = poseLandmarkList.Landmark[14];

    //17 - 左小指
    NormalizedLandmark 左小指 = poseLandmarkList.Landmark[17];
    //18 - 右小指
    NormalizedLandmark 右小指 = poseLandmarkList.Landmark[18];
    //19 - 左食指
    NormalizedLandmark 左食指 = poseLandmarkList.Landmark[19];
    //20 - 右食指
    NormalizedLandmark 右食指 = poseLandmarkList.Landmark[20];
    //21 - 左大拇指
    NormalizedLandmark 左大拇指 = poseLandmarkList.Landmark[21];
    //22 - 右大拇指
    NormalizedLandmark 右大拇指 = poseLandmarkList.Landmark[22];
    //23 - 左臀
    NormalizedLandmark 左臀 = poseLandmarkList.Landmark[23];
    //24 - 右臀
    NormalizedLandmark 右臀 = poseLandmarkList.Landmark[24];
    //25 - 左膝盖
    NormalizedLandmark 左膝盖 = poseLandmarkList.Landmark[25];
    //26 - 右膝盖
    NormalizedLandmark 右膝盖 = poseLandmarkList.Landmark[26];
    //27 - 左脚踝
    NormalizedLandmark 左脚踝 = poseLandmarkList.Landmark[27];
    //28 - 右脚踝
    NormalizedLandmark 右脚踝 = poseLandmarkList.Landmark[28];
    //29 - 左脚跟
    NormalizedLandmark 左脚跟 = poseLandmarkList.Landmark[29];
    //30 - 右脚跟
    NormalizedLandmark 右脚跟 = poseLandmarkList.Landmark[30];
    //31 - 左脚趾
    NormalizedLandmark 左脚趾 = poseLandmarkList.Landmark[31];
    //32 - 右脚趾
    NormalizedLandmark 右脚趾 = poseLandmarkList.Landmark[32];
    */
    TypeEventSystem.Global.Send<直接获取姿势数据>(new 直接获取姿势数据() { nl = poseLandmarkList });
    检测手掌左右滑动(poseLandmarkList);
    检测手掌向下滑动(poseLandmarkList);
  }

  // 存储右手腕位置的轨迹
  private List<float> 右手腕位置轨迹 = new List<float>();
  private List<float> 左手腕位置轨迹 = new List<float>();
  public void 姿势手掌轨迹重置(姿势手掌轨迹重置 a)
  {
    右手腕位置轨迹.Clear();
    左手腕位置轨迹.Clear();
  }
  // 轨迹长度
  private const int 轨迹长度 = 20;

  手掌左右滑动状态 手掌左右滑动状态 = new 手掌左右滑动状态();
  private float 手掌上下滑动检测时间;
  private bool 手掌超过眼睛;


  // 判断手掌滑动的方向
  private void 检测手掌左右滑动(NormalizedLandmarkList poseLandmarkList)
  {
    //Debug.Log("这边检测到了左右滑动状态，一直在检测");
    //11 - 左肩膀
    NormalizedLandmark 左肩膀 = poseLandmarkList.Landmark[11];
    //12 - 右肩膀
    NormalizedLandmark 右肩膀 = poseLandmarkList.Landmark[12];

    //15 - 左手腕
    NormalizedLandmark 左手腕 = poseLandmarkList.Landmark[15];
    //16 - 右手腕
    NormalizedLandmark 右手腕 = poseLandmarkList.Landmark[16];
    float 鼻子 = poseLandmarkList.Landmark[0].X;

    // 添加到右手腕位置的轨迹列表
    右手腕位置轨迹.Add(右手腕.X);
    左手腕位置轨迹.Add(左手腕.X);

    // 限制轨迹列表的长度，避免过长
    if (右手腕位置轨迹.Count > 轨迹长度)
    {
      右手腕位置轨迹.RemoveAt(0);
    }
    // 限制轨迹列表的长度，避免过长
    if (左手腕位置轨迹.Count > 轨迹长度)
    {
      左手腕位置轨迹.RemoveAt(0);
    }
    ////TextDisplayer.DisplayText("右手腕位置轨迹长度:", 右手腕位置轨迹.Count);
    // 判断手腕的滑动方向
    if (右手腕位置轨迹.Count >= 轨迹长度)
    {
      ////TextDisplayer.DisplayText("右手腕是否在屏幕外:", 是否在屏幕外(右手腕));

      if (!是否在屏幕外(右手腕))
      {
        // 判断右手腕位置是否小于右肩膀位置
        bool 右手腕小于肩膀 = false;
        foreach (var 轨迹点 in 右手腕位置轨迹)
        {
          if (轨迹点 < 右肩膀.X)
          {
            右手腕小于肩膀 = true;
            break;
          }
        }
        // 获取最新的右手腕位置
        float 最新右手腕位置;
        try
        {
          最新右手腕位置 = 右手腕位置轨迹[右手腕位置轨迹.Count - 1];
        }
        catch (Exception e)
        {
          Debug.LogError(右手腕位置轨迹.Count + "长度" + e);
          return;
        }
        ////TextDisplayer.DisplayText("最新右手腕位置:", 最新右手腕位置);

        #region 右手腕判断
        // 判断是否满足手腕小于肩膀的条件，并且最新的右手腕位置大于右肩膀位置
        //判断右手腕来控制向左
        if (右手腕小于肩膀 && 最新右手腕位置 > 右肩膀.X)
        {
          // 判断右手腕位置是否大于鼻肩距离的阈值
          if (最新右手腕位置 > 鼻子)
          {
            手掌左右滑动状态.HandType = 1;
            TypeEventSystem.Global.Send<手掌左右滑动状态>(手掌左右滑动状态);
            ////TextDisplayer.DisplayText("右手腕滑动状态:", "手掌向左滑动");
            //向左滑动触发
            // 处理右手掌向左滑动超过阈值的逻辑
            return;
          }
        }
        //判断右手腕来控制向右
        else
        {
          bool 右手腕轨迹有大于肩膀 = false;
          foreach (var 轨迹点 in 右手腕位置轨迹)
          {
            if (轨迹点 > 右肩膀.X)
            {
              右手腕轨迹有大于肩膀 = true;
              break;
            }
          }
          if (右手腕轨迹有大于肩膀 && 最新右手腕位置 < 右肩膀.X)
          {
            // 判断右手腕位置与右肩的阀值是否大于双肩肩距离的阈值
            float 右手腕位置与右肩的阀值 = Math.Abs(右手腕.X - 右肩膀.X);
            float 双肩距离阀值 = Math.Abs(右肩膀.X - 左肩膀.X);
            if (右手腕位置与右肩的阀值 > 双肩距离阀值)
            {
              手掌左右滑动状态.HandType = 2;
              TypeEventSystem.Global.Send<手掌左右滑动状态>(手掌左右滑动状态);
              Debug.Log("手掌向右滑动");
              ////TextDisplayer.DisplayText("右手腕滑动状态:", "手掌向右滑动");
              //向右滑动触发
              // 处理右手掌向左滑动超过阈值的逻辑
              return;
            }
          }
        }
        #endregion
      }


    }

    if (是否在屏幕外(左手腕))
    {
      return;
    }


    // 判断手腕的滑动方向
    if (左手腕位置轨迹.Count >= 轨迹长度)
    {

      // 判断右手腕位置是否小于右肩膀位置
      bool 左手腕轨迹在肩膀左侧 = false;
      foreach (var 轨迹点 in 左手腕位置轨迹)
      {
        if (轨迹点 > 左肩膀.X)
        {
          左手腕轨迹在肩膀左侧 = true;
          break;
        }
      }
      ////TextDisplayer.DisplayText("左手腕轨迹在肩膀左侧:", 左手腕轨迹在肩膀左侧);
      // 获取最新的右手腕位置
      float 最新左手腕位置 = 左手腕位置轨迹[左手腕位置轨迹.Count - 1];
      #region 左手腕判断
      // 判断是否满足手腕小于肩膀的条件，并且最新的右手腕位置大于右肩膀位置
      //判断左手腕来控制向右
      if (左手腕轨迹在肩膀左侧 && 最新左手腕位置 < 左肩膀.X)
      {
        // 判断右手腕位置是否大于鼻肩距离的阈值
        if (最新左手腕位置 < 鼻子)
        {
          手掌左右滑动状态.HandType = 2;
          TypeEventSystem.Global.Send<手掌左右滑动状态>(手掌左右滑动状态);
          Debug.Log("左手掌手掌向右滑动");
          ////TextDisplayer.DisplayText("左手掌手掌向右滑动:", 手掌左右滑动状态);

          //向左滑动触发
          // 处理右手掌向左滑动超过阈值的逻辑
          return;
        }
      }
      //判断左手腕来控制向左
      else
      {
        bool 左手腕轨迹有在肩膀右侧 = false;
        foreach (var 轨迹点 in 左手腕位置轨迹)
        {
          if (轨迹点 < 左肩膀.X)
          {
            左手腕轨迹有在肩膀右侧 = true;
            break;
          }
        }
        if (左手腕轨迹有在肩膀右侧 && 最新左手腕位置 > 左肩膀.X)
        {
          // 判断左手腕位置与左肩的阀值是否大于双肩肩距离的阈值
          float 左手腕位置与左肩的阀值 = Math.Abs(左手腕.X - 左肩膀.X);
          float 双肩距离阀值 = Math.Abs(左肩膀.X - 右肩膀.X);
          if (左手腕位置与左肩的阀值 > 双肩距离阀值)
          {
            手掌左右滑动状态.HandType = 1;
            TypeEventSystem.Global.Send<手掌左右滑动状态>(手掌左右滑动状态);
            Debug.Log("左手掌手掌向左滑动");
            ////TextDisplayer.DisplayText("左手掌手掌向左滑动:", 手掌左右滑动状态);
            //向右滑动触发
            // 处理右手掌向左滑动超过阈值的逻辑
            return;
          }
        }
      }
      #endregion

    }
  }


  /// <summary>
  /// 检测手掌是否向下滑动
  /// </summary>
  /// <param name="手部关键点">手部关键点数据</param>
  /// <param name="滑动阈值">手掌向下滑动的阈值</param>
  /// <param name="向下阈值">手掌向下的额外阈值</param>
  /// <returns>是否检测到手掌向下滑动</returns>
  public bool 检测手掌向下滑动(NormalizedLandmarkList 手部关键点)
  {
    var 右手腕 = 手部关键点.Landmark[16];
    var 右眼 = 手部关键点.Landmark[2]; // 用于检测向下滑动的参考点
    var 右肘 = 手部关键点.Landmark[14]; // 用于检测向下滑动的参考点
                                 ////TextDisplayer.DisplayText("右手腕Y数据:", 右手腕.Y);
                                 ////TextDisplayer.DisplayText("右眼Y数据:", 右眼.Y);
                                 ////TextDisplayer.DisplayText("右肘数据:", 右肘.Y);
    if (DataMgr.手掌超过眼睛之后低于眼睛)
    {
      if (右手腕.Y > 右肘.Y)
      {
        DataMgr.手掌超过眼睛之后低于眼睛 = false;
        手掌超过眼睛 = false;
        // 检测到手掌下滑
        TypeEventSystem.Global.Send<手掌上下滑动状态>(new 手掌上下滑动状态() { HandType = 3 });

        return true;
      }
    }
    if (手掌超过眼睛)
    {
      if (右手腕.Y > 右眼.Y)
      {
        Debug.Log("执行这边");
        DataMgr.手掌超过眼睛之后低于眼睛 = true;
        手掌超过眼睛 = false;
      }
    }

    if (!手掌超过眼睛)
    {
      if (右手腕.Y < 右眼.Y)
      {
        手掌超过眼睛 = true;
      }
      else
      {
        手掌超过眼睛 = false;
      }
    }

    return false;

  }

  public bool 是否在屏幕外(NormalizedLandmark jd)
  {
    if (jd.X > 1 || jd.Y > 1 || jd.X < 0 || jd.Y < 0)
    {
      return true;
    }
    else
    {
      return false;
    }
  }
}
