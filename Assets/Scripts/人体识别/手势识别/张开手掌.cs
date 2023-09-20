using Mediapipe;
using Mediapipe.Unity;
using Mediapipe.Unity.HandTracking;

using QFramework;

using System.Collections.Generic;

using UnityEngine;
/*
 * 0 - wrist（腕部）
1 - thumb_cmc (first metacarpophalangeal joint)（拇指第一掌指关节）
2 - thumb_mcp (first metacarpophalangeal joint)（拇指第一掌指关节）
3 - thumb_ip (first interphalangeal joint)（拇指第一指间关节）
4 - thumb_tip（拇指指尖）
5 - index_finger_mcp (second metacarpophalangeal joint)（食指第二掌指关节）
6 - index_finger_pip (second proximal interphalangeal joint)（食指第二近侧指间关节）
7 - index_finger_dip (second distal interphalangeal joint)（食指第二远侧指间关节）
8 - index_finger_tip（食指指尖）
9 - middle_finger_mcp (third metacarpophalangeal joint)（中指第三掌指关节）
10 - middle_finger_pip (third proximal interphalangeal joint)（中指第三近侧指间关节）
11 - middle_finger_dip (third distal interphalangeal joint)（中指第三远侧指间关节）
12 - middle_finger_tip（中指指尖）
13 - ring_finger_mcp (fourth metacarpophalangeal joint)（无名指第四掌指关节）
14 - ring_finger_pip (fourth proximal interphalangeal joint)（无名指第四近侧指间关节）
15 - ring_finger_dip (fourth distal interphalangeal joint)（无名指第四远侧指间关节）
16 - ring_finger_tip（无名指指尖）
17 - pinky_finger_mcp (fifth metacarpophalangeal joint)（小指第五掌指关节）
18 - pinky_finger_pip (fifth proximal interphalangeal joint)（小指第五近侧指间关节）
19 - pinky_finger_dip (fifth distal interphalangeal joint)（小指第五远侧指间关节）
20 - pinky_finger_tip（小指指尖）
21 - palm (palm base, between wrist and thumb)（掌心，腕部和拇指之间）
22 - index_finger_2nd_joint（食指第二指节）
23 - index_finger_3rd_joint（食指第三指节）
24 - middle_finger_2nd_joint（中指第二指节）
25 - middle_finger_3rd_joint（中指第三指节）
26 - ring_finger_2nd_joint（无名指第二指节）
27 - ring_finger_3rd_joint（无名指第三指节）
28 - pinky_finger_2nd_joint（小指第二指节）
29 - pinky_finger_3rd_joint（小指第三指节）
30 - wrist_bottom（腕部底部）
31 - thumb_2nd_joint（拇指第二指节）
*/
public class 张开手掌
{
  [SerializeField] private HandTrackingSolution 手部追踪解决方案;
  [SerializeField] private float 手掌张开阈值 = 0.08f;
  [SerializeField] private float 向上阈值 = 0.0f;
  [SerializeField] private float 手指间距阈值 = 0.01f; // 调整适当的距离阈值
                                                 // Update is called once per frame
  public void OnHandLandmarksOutput(object sender, OutputEventArgs<List<NormalizedLandmarkList>> eventArgs)
  {
    //if (!DataMgr.人体是否在中间)
    //{
    //    return;
    //}
    List<NormalizedLandmarkList> 手部关键点 = eventArgs.value;
    if (手部关键点 == null)
    {
      return;
    }

    bool 手掌完全张开 = 是否手掌完全张开(手部关键点);
    bool 手指尖向上 = 是否手指尖向上(手部关键点);

    Debug.Log("手掌完全张开:" + 手掌完全张开 + "手指尖向上" + 手指尖向上);
    if (手掌完全张开 && 手指尖向上)
    {
      DataMgr.手掌张开延迟时间 = 0;
      DataMgr.手掌张开状态 = true;
      Debug.Log("手掌完全张开:，指尖向上且正面面对摄像头");
      if (!DataMgr.打开手掌张开特效界面)
      {
        if (DataMgr.手掌页面关闭时间 > 1)
        {
          ThreadToMain.ToMain(1, () =>
          {

          });
        }

      }
      // 在这里添加处理手势的逻辑
    }
  }

  // 判断手掌是否完全张开
  private bool 是否手掌完全张开(List<NormalizedLandmarkList> 手部关键点List)
  {
    foreach (var v in 手部关键点List)
    {
      if (判断当前的手掌是否张开(v))
      {
        return true;
      }

    }
    return false;
  }

  private bool 判断当前的手掌是否张开(NormalizedLandmarkList 手部关键点)
  {
    // 判断手指头、中指指尖Y最小，大拇指指尖最大
    float 大拇指指尖Y = 手部关键点.Landmark[4].Y;
    //float 食指指尖Y = 手部关键点.Landmark[8].Y;
    //float 中指指尖Y = 手部关键点.Landmark[12].Y;
    //float 无名指指尖Y = 手部关键点.Landmark[16].Y;
    //float 小拇指指尖Y = 手部关键点.Landmark[20].Y;
    //float 食指指尖Y = 手部关键点.Landmark[6].Y;
    //float 中指指尖Y = 手部关键点.Landmark[10].Y;
    //float 无名指指尖Y = 手部关键点.Landmark[14].Y;
    //float 小拇指指尖Y = 手部关键点.Landmark[18].Y;
    float 食指指尖Y = 手部关键点.Landmark[7].Y;
    float 中指指尖Y = 手部关键点.Landmark[11].Y;
    float 无名指指尖Y = 手部关键点.Landmark[15].Y;
    float 小拇指指尖Y = 手部关键点.Landmark[19].Y;
    ////TextDisplayer.DisplayText("大拇指指尖Y", 大拇指指尖Y);
    ////TextDisplayer.DisplayText("食指指尖Y", 食指指尖Y);
    ////TextDisplayer.DisplayText("中指指尖Y", 中指指尖Y);
    ////TextDisplayer.DisplayText("无名指指尖Y", 无名指指尖Y);
    ////TextDisplayer.DisplayText("小拇指指尖Y", 小拇指指尖Y);
    if (大拇指指尖Y > 食指指尖Y && 大拇指指尖Y > 中指指尖Y && 大拇指指尖Y > 无名指指尖Y && 大拇指指尖Y > 小拇指指尖Y)
    {
      if (中指指尖Y < 食指指尖Y || 中指指尖Y < 无名指指尖Y || 中指指尖Y < 小拇指指尖Y)
      {
        Debug.Log("手掌完全张开:1");
      }
      else
      {
        ////TextDisplayer.DisplayText("未手掌完全张开:",1);

        return false;
      }
    }
    //NormalizedLandmark 大拇指指尖 = 手部关键点.Landmark[4];
    //NormalizedLandmark 食指指尖 = 手部关键点.Landmark[8];
    //NormalizedLandmark 中指指尖 = 手部关键点.Landmark[12];
    //NormalizedLandmark 无名指指尖 = 手部关键点.Landmark[16];
    //NormalizedLandmark 小指指尖 = 手部关键点.Landmark[20];
    // 判断大拇指的X是否大于其他所有手指头的X
    float 大拇指指尖X = 手部关键点.Landmark[4].X;
    for (int i = 8; i <= 20; i += 4)
    {
      if (i == 20) continue;  // 不判断小拇指
      if (大拇指指尖X >= 手部关键点.Landmark[i].X)
      {
        ////TextDisplayer.DisplayText("未手掌完全张开:", 2);

        return false;
      }
    }

    // 判断小拇指的X是否小于其他所有手指头的X
    float 小拇指指尖X = 手部关键点.Landmark[20].X;
    for (int i = 4; i <= 16; i += 4)
    {
      if (i == 20) continue;  // 不判断小拇指
      if (小拇指指尖X <= 手部关键点.Landmark[i].X)
      {
        ////TextDisplayer.DisplayText("未手掌完全张开:", 3);


        return false;
      }
    }


    // 判断大拇指与食指的距离要大于其他手指之间的距离
    float 大拇指食指距离 = Vector2.Distance(new Vector2(大拇指指尖X, 大拇指指尖Y),
                                          new Vector2(手部关键点.Landmark[8].X, 手部关键点.Landmark[8].Y));
    List<float> 手指距离列表 = new List<float>();
    for (int i = 8; i < 16; i += 4)
    {
      float 手指距离 = Vector2.Distance(new Vector2(手部关键点.Landmark[i].X, 手部关键点.Landmark[i].Y),
                                        new Vector2(手部关键点.Landmark[i + 4].X, 手部关键点.Landmark[i + 4].Y));
      手指距离列表.Add(手指距离);
    }

    foreach (float 距离 in 手指距离列表)
    {
      if (距离 >= 大拇指食指距离)
      {
        ////TextDisplayer.DisplayText("未手掌完全张开:", 4);


        return false;
      }
    }

    foreach (var v in 手指距离列表)
    {
      if (v < 手指间距阈值)
      {
        ////TextDisplayer.DisplayText("未手掌完全张开:", 5);


        return false;
      }
    }
    ////TextDisplayer.DisplayText("未手掌完全张开:", "完全张开");


    return true;
  }

  // 判断手掌指尖是否向上
  private bool 是否手指尖向上(List<NormalizedLandmarkList> 手部关键点List)
  {
    foreach (var 手部关键点 in 手部关键点List)
    {
      // 判断中指指尖Y是不是大于其他中指关节的Y
      float 中指指尖Y = 手部关键点.Landmark[12].Y;
      if (中指指尖Y >= 手部关键点.Landmark[9].Y ||
          中指指尖Y >= 手部关键点.Landmark[10].Y ||
          中指指尖Y >= 手部关键点.Landmark[11].Y)
      {
        ////TextDisplayer.DisplayText("是否手指尖向上:", false);
        return false;
      }
    }
    ////TextDisplayer.DisplayText("是否手指尖向上:", true);



    return true; // 这里返回true表示指尖向上，根据实际需求修改
  }
}
