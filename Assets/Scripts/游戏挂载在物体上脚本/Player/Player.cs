using Ara;

using DG.Tweening;
using QFramework;
using QFramework.Example;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int ��ǰ����;
    public int scoreValue = 10;
    /// <summary>
    /// ��Ч�ͷ���Ԥ����
    /// </summary>
    public GameObject ��������,ը����ը;
    public UISpriteAnimation usa;
    public List<float> scaleList = new List<float>() { 0.67f, 0.82f, 1 };
    public UIPlayerPanel upp;
    public List<int> levelAllScoreList = new List<int>() { 100, 250, 450 };

    private void Awake()
    {
        playerRect = GetComponent<RectTransform>();
    }
    public AIData playerData;
    public Image ������ʾ, Nice, Perfect;
    private float ����ʱ���ж�=0.1f;
    public float ��ǰ����ʱ��;
    public int ��ǰ���д���;
    private bool ��ǰ������ʾ=true;
    // Start is called before the first frame update
    void Start()
    {
        upp = UIKit.GetPanel<UIPlayerPanel>();
        upp.���õ�ǰ�ȼ��ܷ���(playerData.Level);
        usa = GetComponent<UISpriteAnimation>();
        Debug.LogError(playerData.Level);
        transform.DOScale(scaleList[playerData.Level-1], 0.1f);
        ������ʾ = upp.����;
        Nice = upp.Nice;
        Perfect = upp.Perfect;
        scoreEffectPrefab = upp.scoreEffectPrefab;
        transform.GetComponentInChildren<AraTrail>().time = 1f;
        TypeEventSystem.Global.Register<���³��������ٶ�>((b) =>
        {
            transform.GetComponentInChildren<AraTrail>().time = 1f * b.��ǰ�����ٶ�;
        }).UnRegisterWhenGameObjectDestroyed(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        ��ǰ����ʱ�� += Time.deltaTime;
        if (Input.GetKeyUp(KeyCode.M))
        {
            GetComponent<Image>().DOFade(0, 0.15f).SetLoops(4, LoopType.Yoyo).OnComplete(() => {
                GetComponent<BoxCollider>().enabled = true; 
            });
        }
        ////TextDisplayer.DisplayText("��ҵȼ�:", playerData.Level);
    }
    private void ������ʾ����()
    {
        ��ǰ������ʾ = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("AI"))
        {
            BaseObjData fb = other.GetComponent<BaseObjData>();
            if (playerData.Level >= fb.aiData.Level)
            {
                if (��ǰ����ʱ�� < ����ʱ���ж�)
                {
                    if (��ǰ������ʾ)
                    {
                        ��ǰ������ʾ = false;
                        ����������Ч();
                    }

                    ��ǰ���д���++;
                }
                else
                {
                    ��ǰ���д��� = 0;
                }
                ��ǰ����ʱ�� = 0;
                fb.Die(��������.gameObject);
                if (��ǰ���д��� == 1)
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
                    ScoreEffectManager.Instance.ͼƬչʾЧ��(Perfect, playerRect.anchoredPosition);
                }
                if (playerData.Score == 25)
                {
                    ScoreEffectManager.Instance.ͼƬչʾЧ��(Nice, playerRect.anchoredPosition);
                }
            }
            else
            {
                playerData.Score -= fb.aiData.Attack;
                //�ܵ��˺�
                upp.SetScore(playerData.Score);
                Debug.Log("�յ��˺�:" + fb.aiData.name);
                fb.Attack(scoreEffectPrefab.gameObject,ը����ը.gameObject);
            }

        }
    }
    public Image scoreEffectPrefab;
    private void ����������Ч()
    {
        ScoreEffectManager.Instance.ͼƬչʾЧ��(������ʾ, playerRect.anchoredPosition, ������ʾ����);
    }

    public float MoveSpeed=10;
    RectTransform playerRect;
    public void Move(Vector3 pos)
    {
        playerRect.anchoredPosition = pos;// Vector3.MoveTowards(playerRect.anchoredPosition, pos, playerData.Speed);
    }

    public void ����ע��(AIData fishData)
    {
        this.playerData = fishData;
    }
}
