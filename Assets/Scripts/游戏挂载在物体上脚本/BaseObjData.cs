using QFramework;

using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

public enum 角色类型
{
    player,
    ai
}
public abstract class BaseObjData : MonoBehaviour
{
    public 角色类型 fishType;
    public int Speed;
    public int Level;
    public AIData aiData;
    public abstract void Move(Vector3 pos);
    /// <summary>
    /// 依赖注入属性
    /// </summary>
    public abstract void 依赖注入(AIData aiData);
    public virtual void Die(GameObject obj)
    {
        Vector3 pos = GetComponent<RectTransform>().anchoredPosition3D;
        // 触发死亡特效
        GameObject scoreP = Instantiate(obj.gameObject, pos, Quaternion.identity);
        scoreP.transform.parent = transform.parent;
        scoreP.gameObject.SetActive(true);
        scoreP.transform.localScale = Vector3.one*1.2f;
        scoreP.GetComponent<RectTransform>().anchoredPosition3D = pos;
        // 显示分数特效
        //scoreP.GetComponent<ScoreEffectManager>().ShowScoreEffect(obj.GetComponent<BaseObjData>().playerData.Score, pos);
        AudioKit.PlaySound("Resources://音效/昆虫爆炸音效"); 
        Destroy(gameObject);
    }
    public virtual void Attack(GameObject obj,GameObject obj2)
    {
        Vector3 pos = GetComponent<RectTransform>().anchoredPosition3D;
        // 触发死亡特效
        GameObject scoreP = Instantiate(obj.gameObject, pos, Quaternion.identity);
        scoreP.transform.parent = transform.parent;
        scoreP.gameObject.SetActive(true);
        scoreP.transform.localScale = Vector3.one*0.5f;
        scoreP.GetComponent<RectTransform>().anchoredPosition3D = pos;

        // 触发死亡特效
        GameObject scoreP2 = Instantiate(obj2.gameObject, pos, Quaternion.identity);
        scoreP2.transform.parent = transform.parent;
        scoreP2.gameObject.SetActive(true);
        scoreP2.transform.localScale = Vector3.one * 200;
        scoreP2.GetComponent<RectTransform>().anchoredPosition3D = pos;
        AudioKit.PlaySound("Resources://音效/炸弹爆炸音效");

        // 显示分数特效
        scoreP.GetComponent<ScoreEffectManager>().ShowScoreEffect(AIManager.Instance.获取AI数据("炸弹").Score, pos);
        Destroy(gameObject);
    }
}
