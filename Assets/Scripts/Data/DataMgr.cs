
using QFramework;
using QFramework.Example;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class DataMgr : MonoBehaviour
{
    public static bool �Ƿ�������ͷ = true;

    internal static void ResertTime()
    {
        ��ȭѡ���� = 0;
        ���ƻ���ʱ���� = 0;
        TypeEventSystem.Global.Send<�������ƹ켣����>();
    }
    public static float ��ȭѡ����;
    public static float ���ƻ���ʱ����;
    public static float �������� = 0.5f;

    public static bool �����ſ�״̬;
    public static float ����X;
    public static bool �����Ƿ����м�
    {
        get
        {
            return ����X > 0.4 && ����X < 0.6;
        }
    }

    private static bool ���Ƴ����۾�֮������۾�˽��;
    public static float ���Ƴ����۾�֮������۾�ʱ��;
    internal static float �����ſ��ӳ�ʱ��;
    public static bool Ƭͷ�Ƿ��ڲ���;
    internal static bool �������ſ���Ч���� = false;
    internal static float ����ҳ��ر�ʱ��;
    internal static float ��ǰ��Ƶ�����ٶ�;

    public static bool ���Ƴ����۾�֮������۾�
    {
        get
        {
            return ���Ƴ����۾�֮������۾�˽��;
        }
        set
        {
            ���Ƴ����۾�֮������۾�˽�� = value;
            if (value)
            {
                ���Ƴ����۾�֮������۾�ʱ�� = 0;
            }
        }
    }
    private static ResLoader load;
    internal static float ������Y��Χ=400;
    internal static float AI����Ƶ�� = 0.5f;
    internal static int ��ǰ�׶�= 1;
    internal static bool û�м�⵽��;
    private static float cxyxsd=Time.timeScale;
    internal static float ���������ٶ�
    {
        get
        {
            return cxyxsd;
        }
        set
        {
            cxyxsd = value;
            Time.timeScale = cxyxsd;
            TypeEventSystem.Global.Send<���³��������ٶ�>(new ���³��������ٶ� { ��ǰ�����ٶ� = cxyxsd });
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