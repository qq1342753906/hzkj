using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ThreadToMain : MonoBehaviour
{
    static Action a;
    static Action<object> at;
    static object o, objago;

    public static void ToMain(object obj, Action ac)
    {
        o = obj;
        a += ac;
    }
    public static void ToMain<T>(object obj, Action<object> ac)
    {
        o = obj;
        at = ac;
    }
    private void Update()
    {
        if (o != objago)
        {
            if (a != null)
            {
                //Debug.Log("执行调用a方法:" + (a == null));
                a();
                a = null;
            }
            if (at != null)
            {
                at(o);
                at = null;
            }
        }
    }
}
