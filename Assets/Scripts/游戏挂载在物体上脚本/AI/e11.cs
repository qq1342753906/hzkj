using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class e11 : AIBaseData
{
    public override void Move(Vector3 pos)
    {
        Debug.Log("ִ�����ƶ�:" + pos);
    }

    public override void ����ע��(AIData aiData)
    {
        this.aiData = aiData;
    }
}
