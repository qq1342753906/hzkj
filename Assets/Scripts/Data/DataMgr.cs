
using QFramework;
using QFramework.Example;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class DataMgr : MonoBehaviour
{
    public static bool 是否有摄像头 = true;

    internal static void ResertTime()
    {
        握拳选择间隔 = 0;
        手掌滑动时间间隔 = 0;
        TypeEventSystem.Global.Send<姿势手掌轨迹重置>();
    }
    public static float 握拳选择间隔;
    public static float 手掌滑动时间间隔;
    public static float 背景音乐 = 0.5f;

    public static bool 手掌张开状态;
    public static float 鼻子X;
    public static bool 人体是否在中间
    {
        get
        {
            return 鼻子X > 0.4 && 鼻子X < 0.6;
        }
    }

    private static bool 手掌超过眼睛之后低于眼睛私有;
    public static float 手掌超过眼睛之后低于眼睛时间;
    internal static float 手掌张开延迟时间;
    public static bool 片头是否在播放;
    internal static bool 打开手掌张开特效界面 = false;
    internal static float 手掌页面关闭时间;
    internal static float 当前视频播放速度;

    public static bool 手掌超过眼睛之后低于眼睛
    {
        get
        {
            return 手掌超过眼睛之后低于眼睛私有;
        }
        set
        {
            手掌超过眼睛之后低于眼睛私有 = value;
            if (value)
            {
                手掌超过眼睛之后低于眼睛时间 = 0;
            }
        }
    }
    private static ResLoader load;
    internal static float 鱼生成Y范围=400;
    internal static float AI产生频率 = 0.5f;
    internal static int 当前阶段= 1;
    internal static bool 没有检测到人;
    private static float cxyxsd=Time.timeScale;
    internal static float 程序运行速度
    {
        get
        {
            return cxyxsd;
        }
        set
        {
            cxyxsd = value;
            Time.timeScale = cxyxsd;
            TypeEventSystem.Global.Send<更新程序运行速度>(new 更新程序运行速度 { 当前运行速度 = cxyxsd });
        }
    }

    public static ResLoader Load
    {
        get
        {
            if (load == null)
            {
                load = ResLoader.Allocate();
            }
            return load;
        }
    }
}