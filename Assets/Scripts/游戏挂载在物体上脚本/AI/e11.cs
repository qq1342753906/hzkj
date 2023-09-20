using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class e11 : AIBaseData
{
    public override void Move(Vector3 pos)
    {
        Debug.Log("执行了移动:" + pos);
    }

    public override void 依赖注入(AIData aiData)
    {
        this.aiData = aiData;
    }
}
