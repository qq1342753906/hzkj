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
            TypeEventSystem.Global.Register<姿势识别数据解析>(姿势识别是否在中间).UnRegisterWhenGameObjectDestroyed(gameObject);
            StartGameImg.gameObject.SetActive(false);
            TipsPanel.gameObject.SetActive(false);
            AudioKit.PlaySound("Resources://音效/温馨小提示_改");
        }
        private bool 是否检测到人体;
        public float 没有检测到人体时长;
        private float 检测到人体时长;
        private void 姿势识别是否在中间(姿势识别数据解析 zs)
        {
            var landmark = zs.nl.Landmark;
            //Debug.Log("姿势识别:");
            //判断是否在中间
            if (landmark[0].X > 0.45f && landmark[0].X < 0.55)
            {
                //判断鼻子是否在屏幕上方
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
                            if (检测到人体时长 == 0)
                            {
                                检测到人体时长 = Time.time;
                            }
                            
                            没有检测到人体时长 = 0;
                            //if (检测到人体时长 > 1.4f)
                            //{
                            //    //开始游戏，打开玩家界面
                            //    GOinGame();
                            //}
                            //else
                            if (检测到人体时长-Time.time < -0.5f && !StartGameImg.gameObject.activeSelf)
                            {
                                StartGameImg.gameObject.SetActive(true);
                                AudioKit.PlaySound("Resources://音效/解锁音效");
                                StartGameImg.DOFillAmount(1, 1).OnComplete(()=> { GOinGame(); });
                            }
                            List<Vector3> lv = new List<Vector3>();
                             鼻子 = ImageCoordinate.ImageNormalizedToPoint(GameObject.FindObjectOfType<AutoFit>().GetComponent<RectTransform>().rect, landmark[0].X, landmark[0].Y, 0, RotationAngle.Rotation0, false);
                             右跨 = ImageCoordinate.ImageNormalizedToPoint(GameObject.FindObjectOfType<AutoFit>().GetComponent<RectTransform>().rect, landmark[23].X, landmark[23].Y, 0, RotationAngle.Rotation0, false);
                             右肩 = ImageCoordinate.ImageNormalizedToPoint(GameObject.FindObjectOfType<AutoFit>().GetComponent<RectTransform>().rect, landmark[11].X, landmark[11].Y, 0, RotationAngle.Rotation0, false);
                             左肩 = ImageCoordinate.ImageNormalizedToPoint(GameObject.FindObjectOfType<AutoFit>().GetComponent<RectTransform>().rect, landmark[12].X, landmark[12].Y, 0, RotationAngle.Rotation0, false);
                             左脚趾 = ImageCoordinate.ImageNormalizedToPoint(GameObject.FindObjectOfType<AutoFit>().GetComponent<RectTransform>().rect, landmark[32].X, landmark[32].Y, 0, RotationAngle.Rotation0, false);
                             右脚趾 = ImageCoordinate.ImageNormalizedToPoint(GameObject.FindObjectOfType<AutoFit>().GetComponent<RectTransform>().rect, landmark[31].X, landmark[31].Y, 0, RotationAngle.Rotation0, false);
                            lv.Add(鼻子);
                            lv.Add(右跨);
                            lv.Add(右肩);
                            lv.Add(左肩);
                            //TextDisplayer.DisplayText("鼻子:", 鼻子);
                            //TextDisplayer.DisplayText("v2:",右跨);
                            //TextDisplayer.DisplayText("v3:",右肩);
                            //TextDisplayer.DisplayText("v4:",左肩);

                            TipsPanel.gameObject.SetActive(true);

                            TipsPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Abs(右肩.x - 左肩.x), 鼻子.y - (右脚趾.y + 左脚趾.y)/2.0f + (鼻子.y - 左肩.y));
                            //TextDisplayer.DisplayText("TipsPanel大小:", TipsPanel.GetComponent<RectTransform>().sizeDelta);

                            Vector2 screenCenter = new Vector2(UnityEngine.Screen.width / 2f, UnityEngine.Screen.height / 2f);
                            Vector2 imageCenter = new Vector2(screenCenter.x, screenCenter.y - (TipsPanel.GetComponent<RectTransform>().sizeDelta.y / 2f));
                            TipsPanel.GetComponent<RectTransform>().anchoredPosition = new Vector3(-鼻子.x, (鼻子.y + 右脚趾.y + (鼻子.y - 左肩.y)/2.0f) /2.0f, 0);
                            //TipsPanel.GetComponent<RectTransform>().anchoredPosition = imageCenter;
                            ////TextDisplayer.DisplayText("TipsPanel位置:", TipsPanel.GetComponent<RectTransform>().anchoredPosition);

                        });
                    }
                    else
                    {
                        重置人体检测();
                    }
                }
                else
                {
                    重置人体检测();
                }
            }
            else
            {
                    重置人体检测();

            }
        }

    


        private void 重置人体检测()
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
                是否检测到人体 = false;
                检测到人体时长 = 0;
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
        Vector3 鼻子; 
        Vector3 右跨; 
        Vector3 右肩; 
        Vector3 左肩;
        Vector3 左脚趾;
        Vector3 右脚趾;
        public RectTransform rectangle;


        private void Update()
        {

            if (Input.GetKeyUp(KeyCode.K))
            {
                GOinGame();
            }
            if (DataMgr.没有检测到人)
            {
                没有检测到人体时长 += Time.deltaTime;
                if (没有检测到人体时长 > 1)
                {
                    重置人体检测();
                }
            }
        }
    }
}
