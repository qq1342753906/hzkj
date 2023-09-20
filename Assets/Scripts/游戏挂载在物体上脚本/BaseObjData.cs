using QFramework;

using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

public enum ��ɫ����
{
    player,
    ai
}
public abstract class BaseObjData : MonoBehaviour
{
    public ��ɫ���� fishType;
    public int Speed;
    public int Level;
    public AIData aiData;
    public abstract void Move(Vector3 pos);
    /// <summary>
    /// ����ע������
    /// </summary>
    public abstract void ����ע��(AIData aiData);
    public virtual void Die(GameObject obj)
    {
        Vector3 pos = GetComponent<RectTransform>().anchoredPosition3D;
        // ����������Ч
        GameObject scoreP = Instantiate(obj.gameObject, pos, Quaternion.identity);
        scoreP.transform.parent = transform.parent;
        scoreP.gameObject.SetActive(true);
        scoreP.transform.localScale = Vector3.one*1.2f;
        scoreP.GetComponent<RectTransform>().anchoredPosition3D = pos;
        // ��ʾ������Ч
        //scoreP.GetComponent<ScoreEffectManager>().ShowScoreEffect(obj.GetComponent<BaseObjData>().playerData.Score, pos);
        AudioKit.PlaySound("Resources://��Ч/���汬ը��Ч"); 
        Destroy(gameObject);
    }
    public virtual void Attack(GameObject obj,GameObject obj2)
    {
        Vector3 pos = GetComponent<RectTransform>().anchoredPosition3D;
        // ����������Ч
        GameObject scoreP = Instantiate(obj.gameObject, pos, Quaternion.identity);
        scoreP.transform.parent = transform.parent;
        scoreP.gameObject.SetActive(true);
        scoreP.transform.localScale = Vector3.one*0.5f;
        scoreP.GetComponent<RectTransform>().anchoredPosition3D = pos;

        // ����������Ч
        GameObject scoreP2 = Instantiate(obj2.gameObject, pos, Quaternion.identity);
        scoreP2.transform.parent = transform.parent;
        scoreP2.gameObject.SetActive(true);
        scoreP2.transform.localScale = Vector3.one * 200;
        scoreP2.GetComponent<RectTransform>().anchoredPosition3D = pos;
        AudioKit.PlaySound("Resources://��Ч/ը����ը��Ч");

        // ��ʾ������Ч
        scoreP.GetComponent<ScoreEffectManager>().ShowScoreEffect(AIManager.Instance.��ȡAI����("ը��").Score, pos);
        Destroy(gameObject);
    }
}
