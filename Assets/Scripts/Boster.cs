using DG.Tweening;

using QFramework;
using QFramework.Example;

using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Boster : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {

        ResKit.Init();
        //if (GameObject.FindObjectOfType<MediapipeInit>() == null)
        //{
        UIKit.OpenPanel<UIAnnotatableScreenPanel>();
        UIKit.OpenPanel<UIMousePanel>(UILevel.CanvasPanel);
        UIKit.OpenPanel<UIMainPanel>();
        //GameObject.Instantiate(ResLoader.Allocate().LoadSync<GameObject>("Annotatable Screen"));
        //}
        InitObj();
        InitData();
        InitEvent();

    }


    private void InitEvent()
    {

    }

    public void InitData()
    {
        Debug.Log("当前打开的场景:" + SceneManager.sceneCount);
    }

    private static void InitObj()
    {
        Transform ht = new GameObject("Helper").transform;
        new GameObject("ThreadToMain").AddComponent<ThreadToMain>().transform.parent = ht;
        new GameObject("DataUpdate").AddComponent<DataUpdate>().transform.parent = ht;
        new GameObject("TextDisplayer").AddComponent<TextDisplayer>().transform.parent = ht;
        ht.AddComponent<DontDestroyOnLoadScript>();

        //if(GameObject.FindObjectOfType<MediapipeInit>() == null){
        //    ResLoader.Allocate();
        //}
    }

    private void OnApplicationQuit()
    {

    }
}
