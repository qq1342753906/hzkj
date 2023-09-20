using Ara;

using DG.Tweening;
using QFramework;
using QFramework.Example;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int 当前分数;
    public int scoreValue = 10;
    /// <summary>
    /// 特效和分数预制体
    /// </summary>
    public GameObject 昆虫死亡,炸弹爆炸;
    public UISpriteAnimation usa;
    public List<float> scaleList = new List<float>() { 0.67f, 0.82f, 1 };
    public UIPlayerPanel upp;
    public List<int> levelAllScoreList = new List<int>() { 100, 250, 450 };

    private void Awake()
    {
        playerRect = GetComponent<RectTransform>();
    }
    public AIData playerData;
    public Image 连切提示, Nice, Perfect;
    private float 连切时间判断=0.1f;
    public float 当前连切时间;
    public int 当前连切次数;
    private bool 当前连切显示=true;
    // Start is called before the first frame update
    void Start()
    {
        upp = UIKit.GetPanel<UIPlayerPanel>();
        upp.设置当前等级总分数(playerData.Level);
        usa = GetComponent<UISpriteAnimation>();
        Debug.LogError(playerData.Level);
        transform.DOScale(scaleList[playerData.Level-1], 0.1f);
        连切提示 = upp.连切;
        Nice = upp.Nice;
        Perfect = upp.Perfect;
        scoreEffectPrefab = upp.scoreEffectPrefab;
        transform.GetComponentInChildren<AraTrail>().time = 1f;
        TypeEventSystem.Global.Register<更新程序运行速度>((b) =>
        {
            transform.GetComponentInChildren<AraTrail>().time = 1f * b.当前运行速度;
        }).UnRegisterWhenGameObjectDestroyed(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        当前连切时间 += Time.deltaTime;
        if (Input.GetKeyUp(KeyCode.M))
        {
            GetComponent<Image>().DOFade(0, 0.15f).SetLoops(4, LoopType.Yoyo).OnComplete(() => {
                GetComponent<BoxCollider>().enabled = true; 
            });
        }
        ////TextDisplayer.DisplayText("玩家等级:", playerData.Level);
    }
    private void 连切提示重置()
    {
        当前连切显示 = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("AI"))
        {
            BaseObjData fb = other.GetComponent<BaseObjData>();
            if (playerData.Level >= fb.aiData.Level)
            {
                if (当前连切时间 < 连切时间判断)
                {
                    if (当前连切显示)
                    {
                        当前连切显示 = false;
                        触发连接特效();
                    }

                    当前连切次数++;
                }
                else
                {
                    当前连切次数 = 0;
                }
                当前连切时间 = 0;
                fb.Die(昆虫死亡.gameObject);
                if (当前连切次数 == 1)
                {
                    playerData.Score += fb.aiData.Score;
                }
                else
                {
                    playerData.Score += fb.aiData.Score;
                }
                upp.SetScore(playerData.Score);
                if (playerData.Score == 15)
                {
                    ScoreEffectManager.Instance.图片展示效果(Perfect, playerRect.anchoredPosition);
                }
                if (playerData.Score == 25)
                {
                    ScoreEffectManager.Instance.图片展示效果(Nice, playerRect.anchoredPosition);
                }
            }
            else
            {
                playerData.Score -= fb.aiData.Attack;
                //受到伤害
                upp.SetScore(playerData.Score);
                Debug.Log("收到伤害:" + fb.aiData.name);
                fb.Attack(scoreEffectPrefab.gameObject,炸弹爆炸.gameObject);
            }

        }
    }
    public Image scoreEffectPrefab;
    private void 触发连接特效()
    {
        ScoreEffectManager.Instance.图片展示效果(连切提示, playerRect.anchoredPosition, 连切提示重置);
    }

    public float MoveSpeed=10;
    RectTransform playerRect;
    public void Move(Vector3 pos)
    {
        playerRect.anchoredPosition = pos;// Vector3.MoveTowards(playerRect.anchoredPosition, pos, playerData.Speed);
    }

    public void 依赖注入(AIData fishData)
    {
        this.playerData = fishData;
    }
}
