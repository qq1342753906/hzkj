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
            Debug.LogError("����������ͣ��Ϸ");
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
    public AIData ��ȡAI����(string name)
    {
        if (AIDataDic.ContainsKey(name))
        {
            return AIDataDic[name];
        }
        return null;
    }
    public AIData ��ȡ����AI����(string name)
    {
        if (AIDataDic.ContainsKey(name))
        {
            return AIDataDic[name];
        }
        else
        {
            Debug.LogError("��ǰ�����ļ��޴�AI:" + name);
            return null;
        }
    }
    public AIData ����AI����(string name=null)
    {
        if (string.IsNullOrEmpty(name))
        {
            //�������AI
            int AI = new Random().Next(0, AIDataList.Count-1);
            return ��ȡ����AI����(AIDataList[AI].name);
        }
        else
        {
            //����ָ��AI
            return ��ȡ����AI����(name);
        }
    }
    public GameObject ����AI����()
    {
        return ����AI����(����AI����());
    }
    public GameObject ָ������AI����(string name)
    {
        return ����AI����(����AI����(name));
    }
    private GameObject ����AI����(AIData AI)
    {
        //ʹ��ͬ�����أ��첽�������ӳ������ԣ�������
        GameObject AIObj = GameObject.Instantiate(DataMgr.Load.LoadSync<GameObject>(AI.name));
        if (AIObj.GetComponent<BaseObjData>() != null)
        {
            AIObj.GetComponent<BaseObjData>().����ע��(AI);
        }
        else
        {
            string scriptTypeName = AI.name.Replace("(Clone)", ""); // Ҫת���Ľű���������
            // ͨ���������ƻ�ȡ����
            Debug.LogError("��ǰ��AI����:" + scriptTypeName);
            Type scriptType = Type.GetType(scriptTypeName);
            if (scriptType != null && typeof(MonoBehaviour).IsAssignableFrom(scriptType))
            {
                // ����ʵ��
                MonoBehaviour scriptInstance = Activator.CreateInstance(scriptType) as MonoBehaviour;

                // ��ʵ�����Ϊ���
                //if (scriptInstance != null)
                {
                    AIObj.AddComponent(scriptType);
                    if (AIObj.GetComponent<BaseObjData>() != null)
                    {
                        AIObj.GetComponent<BaseObjData>().����ע��(AI);
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
