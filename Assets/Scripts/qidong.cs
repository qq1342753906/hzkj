using QFramework;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class qidong : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ResKit.Init();
        if (GameObject.FindObjectOfType<Boster>() == null)
        {
            Debug.Log("��������һ�Σ�������");
            Debug.LogError("��������һ�Σ�������");
            GameObject.Instantiate(ResLoader.Allocate().LoadSync<GameObject>("Boster"));
        }
        else
        {
            Debug.Log("Boster:"+GameObject.FindObjectOfType<Boster>().name);
            GameObject.FindObjectOfType<Boster>().InitData();
        }
    }
}
