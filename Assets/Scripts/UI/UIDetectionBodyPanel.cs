using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Mediapipe.Unity;
using Mediapipe.Unity.CoordinateSystem;
using System.Collections.Generic;
using DG.Tweening;

namespace QFramework.Example
{
    public class UIDetectionBodyPanelData : UIPanelData
    {
    }
    public partial class UIDetectionBodyPanel : UIPanel
    {
        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIDetectionBodyPanelData ?? new UIDetectionBodyPanelData();
            // please add init code here
            TypeEventSystem.Global.Register<����ʶ�����ݽ���>(����ʶ���Ƿ����м�).UnRegisterWhenGameObjectDestroyed(gameObject);
            StartGameImg.gameObject.SetActive(false);
            TipsPanel.gameObject.SetActive(false);
            AudioKit.PlaySound("Resources://��Ч/��ܰС��ʾ_��");
        }
        private bool �Ƿ��⵽����;
        public float û�м�⵽����ʱ��;
        private float ��⵽����ʱ��;
        private void ����ʶ���Ƿ����м�(����ʶ�����ݽ��� zs)
        {
            var landmark = zs.nl.Landmark;
            //Debug.Log("����ʶ��:");
            //�ж��Ƿ����м�
            if (landmark[0].X > 0.45f && landmark[0].X < 0.55)
            {
                //�жϱ����Ƿ�����Ļ�Ϸ�
                if (landmark[0].Y < 0.35f)
                {
                    if (landmark[27].Y < 1 && landmark[28].Y < 1)
                    {
                        ThreadToMain.ToMain(1, () =>
                        {
                            if (TipsPanel == null)
                            {
                                return;
                            }
                            if (��⵽����ʱ�� == 0)
                            {
                                ��⵽����ʱ�� = Time.time;
                            }
                            
                            û�м�⵽����ʱ�� = 0;
                            //if (��⵽����ʱ�� > 1.4f)
                            //{
                            //    //��ʼ��Ϸ������ҽ���
                            //    GOinGame();
                            //}
                            //else
                            if (��⵽����ʱ��-Time.time < -0.5f && !StartGameImg.gameObject.activeSelf)
                            {
                                StartGameImg.gameObject.SetActive(true);
                                AudioKit.PlaySound("Resources://��Ч/������Ч");
                                StartGameImg.DOFillAmount(1, 1).OnComplete(()=> { GOinGame(); });
                            }
                            List<Vector3> lv = new List<Vector3>();
                             ���� = ImageCoordinate.ImageNormalizedToPoint(GameObject.FindObjectOfType<AutoFit>().GetComponent<RectTransform>().rect, landmark[0].X, landmark[0].Y, 0, RotationAngle.Rotation0, false);
                             �ҿ� = ImageCoordinate.ImageNormalizedToPoint(GameObject.FindObjectOfType<AutoFit>().GetComponent<RectTransform>().rect, landmark[23].X, landmark[23].Y, 0, RotationAngle.Rotation0, false);
                             �Ҽ� = ImageCoordinate.ImageNormalizedToPoint(GameObject.FindObjectOfType<AutoFit>().GetComponent<RectTransform>().rect, landmark[11].X, landmark[11].Y, 0, RotationAngle.Rotation0, false);
                             ��� = ImageCoordinate.ImageNormalizedToPoint(GameObject.FindObjectOfType<AutoFit>().GetComponent<RectTransform>().rect, landmark[12].X, landmark[12].Y, 0, RotationAngle.Rotation0, false);
                             ���ֺ = ImageCoordinate.ImageNormalizedToPoint(GameObject.FindObjectOfType<AutoFit>().GetComponent<RectTransform>().rect, landmark[32].X, landmark[32].Y, 0, RotationAngle.Rotation0, false);
                             �ҽ�ֺ = ImageCoordinate.ImageNormalizedToPoint(GameObject.FindObjectOfType<AutoFit>().GetComponent<RectTransform>().rect, landmark[31].X, landmark[31].Y, 0, RotationAngle.Rotation0, false);
                            lv.Add(����);
                            lv.Add(�ҿ�);
                            lv.Add(�Ҽ�);
                            lv.Add(���);
                            //TextDisplayer.DisplayText("����:", ����);
                            //TextDisplayer.DisplayText("v2:",�ҿ�);
                            //TextDisplayer.DisplayText("v3:",�Ҽ�);
                            //TextDisplayer.DisplayText("v4:",���);

                            TipsPanel.gameObject.SetActive(true);

                            TipsPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Abs(�Ҽ�.x - ���.x), ����.y - (�ҽ�ֺ.y + ���ֺ.y)/2.0f + (����.y - ���.y));
                            //TextDisplayer.DisplayText("TipsPanel��С:", TipsPanel.GetComponent<RectTransform>().sizeDelta);

                            Vector2 screenCenter = new Vector2(UnityEngine.Screen.width / 2f, UnityEngine.Screen.height / 2f);
                            Vector2 imageCenter = new Vector2(screenCenter.x, screenCenter.y - (TipsPanel.GetComponent<RectTransform>().sizeDelta.y / 2f));
                            TipsPanel.GetComponent<RectTransform>().anchoredPosition = new Vector3(-����.x, (����.y + �ҽ�ֺ.y + (����.y - ���.y)/2.0f) /2.0f, 0);
                            //TipsPanel.GetComponent<RectTransform>().anchoredPosition = imageCenter;
                            ////TextDisplayer.DisplayText("TipsPanelλ��:", TipsPanel.GetComponent<RectTransform>().anchoredPosition);

                        });
                    }
                    else
                    {
                        ����������();
                    }
                }
                else
                {
                    ����������();
                }
            }
            else
            {
                    ����������();

            }
        }

    


        private void ����������()
        {
            ThreadToMain.ToMain(1, () =>
            {
                if (TipsPanel != null)
                    if (TipsPanel.gameObject.activeSelf)
                    {
                        TipsPanel.color = Color.white;
                        TipsPanel.gameObject.SetActive(false);
                        StartGameImg.gameObject.gameObject.SetActive(false);
                        DOTween.Kill(StartGameImg);
                        StartGameImg.fillAmount = 0;

                    }
                �Ƿ��⵽���� = false;
                ��⵽����ʱ�� = 0;
            });
        }

        private void GOinGame()
        {
            UIKit.OpenPanel<UIDownTimePanel>();
            //         UIKit.OpenPanel<UIBGPanel>();
            //         UIKit.OpenPanel<UIPlayerPanel>();
            //UIKit.OpenPanel<UIAIFishPanel>();
            CloseSelf();
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
        }
        public Color lineColor = Color.red;
        public float lineWidth = 0.1f;
        Vector3 ����; 
        Vector3 �ҿ�; 
        Vector3 �Ҽ�; 
        Vector3 ���;
        Vector3 ���ֺ;
        Vector3 �ҽ�ֺ;
        public RectTransform rectangle;


        private void Update()
        {

            if (Input.GetKeyUp(KeyCode.K))
            {
                GOinGame();
            }
            if (DataMgr.û�м�⵽��)
            {
                û�м�⵽����ʱ�� += Time.deltaTime;
                if (û�м�⵽����ʱ�� > 1)
                {
                    ����������();
                }
            }
        }
    }
}
