using QFramework.Example;
using QFramework;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataUpdate : MonoBehaviour
{
  void FixedUpdate()
  {
    DataMgr.握拳选择间隔 += Time.deltaTime;
    DataMgr.手掌滑动时间间隔 += Time.deltaTime;
    DataMgr.手掌超过眼睛之后低于眼睛时间 += Time.deltaTime;
    if (DataMgr.手掌超过眼睛之后低于眼睛时间 > 0.3f)
    {
      DataMgr.手掌超过眼睛之后低于眼睛 = false;
    }
    ////TextDisplayer.DisplayText("延迟时间:",DataMgr.手掌张开延迟时间);
    DataMgr.手掌张开延迟时间 += Time.deltaTime;
    if (DataMgr.手掌张开延迟时间 > 0.3)
    {
      DataMgr.手掌张开状态 = false;
      if (DataMgr.打开手掌张开特效界面)
      {
        //这边可能考虑自行张开手掌界面

      }
    }
    DataMgr.手掌页面关闭时间 += Time.deltaTime;
    ////TextDisplayer.DisplayText("蓝牙连接状态:", DataMgr.蓝牙连接状态);
  }

}
