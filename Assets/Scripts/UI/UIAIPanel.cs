using UnityEngine;
using UnityEngine.UI;
using QFramework;
using DG.Tweening;
using System;

namespace QFramework.Example
{
	public class UIAIPanelData : UIPanelData
	{
	}
	public partial class UIAIPanel : UIPanel
	{
        private bool 是否产生鱼;
        public Player player;
        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIAIPanelData ?? new UIAIPanelData();
            // please add init code here
            TypeEventSystem.Global.Register<福利时间>((bl) => {
                if (bl.fl == true)
                {
                    产生AI时间 = -0.6f;

                    foreach (var v in GameObject.FindObjectsOfType<AIBaseData>())
                    {
                        v.transform.DOKill();
                        if (v.GetComponent<BoxCollider>() != null)
                        {
                            v.GetComponent<BoxCollider>().enabled = false;

                        }
                        v.GetComponent<Image>().DOFade(0, 1f);
                        AIBaseData aif = v;
                        aif.GetComponent<RectTransform>().DOAnchorPos3D(v.endPos, 0.4f).OnComplete(() => { Destroy(aif.gameObject); });
                    }
                    Invoke("产生摇钱树", 0.4f);
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            int spwant1, spwant2;
            spwant1 = UnityEngine.Random.Range(10, 50);
            spwant2 = UnityEngine.Random.Range(10, 50);
            Invoke("产生炸弹", spwant1);
            Invoke("产生炸弹", spwant2);
            player = UIKit.GetPanel<UIPlayerPanel>().playerL;
            TypeEventSystem.Global.Register<更新等级>(更新等级).UnRegisterWhenGameObjectDestroyed(gameObject);
            生成阶段 = Newtonsoft.Json.JsonConvert.DeserializeObject<昆虫>(Resources.Load<TextAsset>("DataText/阶段").text);

        }
        public 昆虫 生成阶段;
        private void 更新等级(更新等级 等级)
        {
            DataMgr.AI产生频率 = 生成阶段.生成速度[等级.当前等级];
            DataMgr.程序运行速度 = 生成阶段.移动速度[等级.当前等级];
        }
        private void 产生炸弹()
        {
            产生AI("炸弹");
        }
        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.C))
            {
                UIKit.OpenPanel<UITestDataPanel>();

            }
        }
        float 产生AI时间;
        int AI炸弹;
        private void FixedUpdate()
        {
            if (产生AI时间 > DataMgr.AI产生频率)
            {
                产生AI时间 = 0;
                产生AI();
                AI炸弹++;
                if(DataMgr.AI产生频率<0.3f)
                if (AI炸弹 % 10 == 1)
                {
                    AI炸弹 = AI炸弹 % 10;
                    产生AI("炸弹");
                }
            }
            产生AI时间 += Time.unscaledDeltaTime;
        }
        private void 产生AI(string name =null)
        {
            GameObject aiObj;
            if (string.IsNullOrEmpty(name))
            {
                aiObj = AIManager.Instance.产生AI物体();

            }
            else
            {
                aiObj = AIManager.Instance.指定产生AI物体(name);
            }
            aiObj.transform.parent = transform;
            aiObj.transform.localScale = Vector3.one;

            int rota = UnityEngine.Random.Range(-960, 960);
            if (rota == 1100)
            {
                aiObj.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 180, 0);
            }
            aiObj.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(rota, -700, 0);
            //设置ai鱼终点
            if (aiObj.name.Equals("炸弹(Clone)"))
            {
                aiObj.GetComponent<RectTransform>().DOAnchorPos3DY(Screen.height/2.0f, 2).SetEase(Ease.OutCirc).OnComplete(() => {
                   var  myTween2 = aiObj.GetComponent<RectTransform>().DOAnchorPos3DY(-Screen.height / 2.0f, 2).SetEase(Ease.InCirc).OnComplete(()=>Destroy(aiObj)).SetUpdate(true);
                }).SetUpdate(true);
                //aiObj.GetComponent<Rigidbody>().AddForce(new Vector3(0,15,0), ForceMode.Impulse);
            }
            else
            {
                aiObj.GetComponent<AIBaseData>().AddFaoce();
            }
        }
        protected override void OnOpen(IUIData uiData = null)
        {
        }

        protected override void OnShow()
        {
        }

        protected override void OnHide()
        {
        }

        protected override void OnClose()
        {
            更新等级(new 更新等级 { 当前等级=0});
        }
    }
}
