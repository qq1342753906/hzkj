using QFramework;

using System;
using System.Collections.Generic;

using UnityEngine;

using Random = System.Random;

public class AIManager : MonoSingleton<AIManager>
{
    private void Awake()
    {
        AIDataList=Newtonsoft.Json.JsonConvert.DeserializeObject<List<AIData>>(Resources.Load<TextAsset>("DataText/AIDataJson").text
            );
        foreach(var v in AIDataList)
        {
            AIDataDic.Add(v.name, v);
        }
        if (AIDataDic.Count == 0)
        {
            Debug.LogError("发生错误，暂停游戏");
            Time.timeScale = 0;
        }
    }
    public bool ResetData()
    {
        AIDataList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AIData>>(Resources.Load<TextAsset>("DataText/AIDataJson").text
        );
        if (AIDataList == null || AIDataList.Count == 0)
        {
            return false;
        }
        AIDataDic.Clear();
        foreach (var v in AIDataList)
        {
            AIDataDic.Add(v.name, v);
        }
        return true;
    }
    List<AIData> AIDataList = new List<AIData>();
    Dictionary<string, AIData> AIDataDic = new Dictionary<string, AIData>();
    public AIData 获取AI数据(string name)
    {
        if (AIDataDic.ContainsKey(name))
        {
            return AIDataDic[name];
        }
        return null;
    }
    public AIData 获取具体AI数据(string name)
    {
        if (AIDataDic.ContainsKey(name))
        {
            return AIDataDic[name];
        }
        else
        {
            Debug.LogError("当前配置文件无此AI:" + name);
            return null;
        }
    }
    public AIData 创建AI数据(string name=null)
    {
        if (string.IsNullOrEmpty(name))
        {
            //随机产生AI
            int AI = new Random().Next(0, AIDataList.Count-1);
            return 获取具体AI数据(AIDataList[AI].name);
        }
        else
        {
            //产生指定AI
            return 获取具体AI数据(name);
        }
    }
    public GameObject 产生AI物体()
    {
        return 产生AI物体(创建AI数据());
    }
    public GameObject 指定产生AI物体(string name)
    {
        return 产生AI物体(创建AI数据(name));
    }
    private GameObject 产生AI物体(AIData AI)
    {
        //使用同步加载，异步加载增加程序复杂性，不建议
        GameObject AIObj = GameObject.Instantiate(DataMgr.Load.LoadSync<GameObject>(AI.name));
        if (AIObj.GetComponent<BaseObjData>() != null)
        {
            AIObj.GetComponent<BaseObjData>().依赖注入(AI);
        }
        else
        {
            string scriptTypeName = AI.name.Replace("(Clone)", ""); // 要转换的脚本类型名称
            // 通过类型名称获取类型
            Debug.LogError("当前胡AI名字:" + scriptTypeName);
            Type scriptType = Type.GetType(scriptTypeName);
            if (scriptType != null && typeof(MonoBehaviour).IsAssignableFrom(scriptType))
            {
                // 创建实例
                MonoBehaviour scriptInstance = Activator.CreateInstance(scriptType) as MonoBehaviour;

                // 将实例添加为组件
                //if (scriptInstance != null)
                {
                    AIObj.AddComponent(scriptType);
                    if (AIObj.GetComponent<BaseObjData>() != null)
                    {
                        AIObj.GetComponent<BaseObjData>().依赖注入(AI);
                    }
                }
                //else
                //{
                //    Debug.LogError("Failed to create instance of script type.");
                //}
            }
            else
            {
                Debug.LogError("Script type not found or is not a MonoBehaviour.");
            }
        }
        return AIObj;
    }
}
