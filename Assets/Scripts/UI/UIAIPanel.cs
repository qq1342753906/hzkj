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
        private bool �Ƿ������;
        public Player player;
        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIAIPanelData ?? new UIAIPanelData();
            // please add init code here
            TypeEventSystem.Global.Register<����ʱ��>((bl) => {
                if (bl.fl == true)
                {
                    ����AIʱ�� = -0.6f;

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
                    Invoke("����ҡǮ��", 0.4f);
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            int spwant1, spwant2;
            spwant1 = UnityEngine.Random.Range(10, 50);
            spwant2 = UnityEngine.Random.Range(10, 50);
            Invoke("����ը��", spwant1);
            Invoke("����ը��", spwant2);
            player = UIKit.GetPanel<UIPlayerPanel>().playerL;
            TypeEventSystem.Global.Register<���µȼ�>(���µȼ�).UnRegisterWhenGameObjectDestroyed(gameObject);
            ���ɽ׶� = Newtonsoft.Json.JsonConvert.DeserializeObject<����>(Resources.Load<TextAsset>("DataText/�׶�").text);

        }
        public ���� ���ɽ׶�;
        private void ���µȼ�(���µȼ� �ȼ�)
        {
            DataMgr.AI����Ƶ�� = ���ɽ׶�.�����ٶ�[�ȼ�.��ǰ�ȼ�];
            DataMgr.���������ٶ� = ���ɽ׶�.�ƶ��ٶ�[�ȼ�.��ǰ�ȼ�];
        }
        private void ����ը��()
        {
            ����AI("ը��");
        }
        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.C))
            {
                UIKit.OpenPanel<UITestDataPanel>();

            }
        }
        float ����AIʱ��;
        int AIը��;
        private void FixedUpdate()
        {
            if (����AIʱ�� > DataMgr.AI����Ƶ��)
            {
                ����AIʱ�� = 0;
                ����AI();
                AIը��++;
                if(DataMgr.AI����Ƶ��<0.3f)
                if (AIը�� % 10 == 1)
                {
                    AIը�� = AIը�� % 10;
                    ����AI("ը��");
                }
            }
            ����AIʱ�� += Time.unscaledDeltaTime;
        }
        private void ����AI(string name =null)
        {
            GameObject aiObj;
            if (string.IsNullOrEmpty(name))
            {
                aiObj = AIManager.Instance.����AI����();

            }
            else
            {
                aiObj = AIManager.Instance.ָ������AI����(name);
            }
            aiObj.transform.parent = transform;
            aiObj.transform.localScale = Vector3.one;

            int rota = UnityEngine.Random.Range(-960, 960);
            if (rota == 1100)
            {
                aiObj.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 180, 0);
            }
            aiObj.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(rota, -700, 0);
            //����ai���յ�
            if (aiObj.name.Equals("ը��(Clone)"))
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
            ���µȼ�(new ���µȼ� { ��ǰ�ȼ�=0});
        }
    }
}
