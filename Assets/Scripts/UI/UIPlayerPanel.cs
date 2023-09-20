using DG.Tweening;
using Mediapipe.Unity;
using Mediapipe.Unity.CoordinateSystem;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Example
{
    public class UIPlayerPanelData : UIPanelData
    {
    }
    public partial class UIPlayerPanel : UIPanel,ICloseable
    {
        public RectTransform sbg;       // ����������
        public RectTransform sbgValue;  // ����������
        [Range(0, 1)]
        private float currentValue = 0; // ��ǰ����ֵ����Χ��0 - 1��
        float totalTime = 60; // �ܵ���ʱʱ�䣨�룩

        private float remainingTime; // ʣ�൹��ʱʱ�䣨�룩
        private int nowscore;
        public int NowScore
        {
            get
            {
                return nowscore;
            }
            set
            {
                nowscore = value;
            }
        }
        public float NowLevelAllScore;
        private List<float> levelAllScoreList = new List<float>() { 100.00f, 250.00f, 570.00f };
        Rect rect;
        public List<Sprite> silderBgList = new List<Sprite>();
        public Sprite ��ɫ�׿�;
        public float �����쳣���ʱ��;
        bool ��������ʶ��=true;
        public AIData PlayerData;
        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIPlayerPanelData ?? new UIPlayerPanelData();
            // please add init code here
            initData();
            initEvent();
            initObj();
            PlayMusic();
        }
        void PlayMusic()
        {
            AudioKit.PlayMusic("resources://��Ч/������Ч");
        }
        public ���� ���ɽ׶�;
        private void initData()
        {
            isCountingDown = true;
            Time.timeScale = 0.5f;
            DataMgr.��ǰ�׶� = 1;
            PlayerData = Newtonsoft.Json.JsonConvert.DeserializeObject<AIData>(Resources.Load<TextAsset>("DataText/playerCfig").text);
            ���ɽ׶� = Newtonsoft.Json.JsonConvert.DeserializeObject<����>(Resources.Load<TextAsset>("DataText/�׶�").text);
            playerL = PlayerL.GetComponent<Player>();
            playerLRect = PlayerL.GetComponent<RectTransform>();
            playerR = PlayerR.GetComponent<Player>();
            playerRRect = PlayerR.GetComponent<RectTransform>();
            playerL.����ע��(PlayerData);
            playerR.����ע��(PlayerData);
            rect = GameObject.FindObjectOfType<AutoFit>().GetComponent<RectTransform>().rect;
            remainingTime = totalTime;
            UpdateCountdownText();
        }

        private void initEvent()
        {
            TypeEventSystem.Global.Register<����ʶ�����ݽ���>(����ƶ�).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void initObj()
        {
            checkStatus.gameObject.SetActive(false);
            warmImg.gameObject.SetActive(false);
            wariIcon.gameObject.SetActive(false);
            ����.gameObject.SetActive(false);
            Nice.gameObject.SetActive(false);
            Perfect.gameObject.SetActive(false);
        }

        float agoPosX;
        [Range(0,-500)]
        public float RangeY = -250f;
        private void ����ƶ�(����ʶ�����ݽ��� zs)
        {
            if (!��������ʶ��)
            {
                return;
            }
            Debug.Log("����ƶ�����:");
            ThreadToMain.ToMain(1, () =>
            {
                var landmark = zs.nl.Landmark;
                var v1 = ImageCoordinate.ImageNormalizedToPoint(rect, landmark[0].X, landmark[0].Y, 0, RotationAngle.Rotation0, false);
                var v2 = ImageCoordinate.ImageNormalizedToPoint(rect, landmark[19].X, landmark[19].Y, 0, RotationAngle.Rotation0, false);
                var v3 = ImageCoordinate.ImageNormalizedToPoint(rect, landmark[20].X, landmark[20].Y, 0, RotationAngle.Rotation0, false);
                �ƶ�(v2,v3);
            }
            );
        }
        public Player playerL,playerR;
        public RectTransform playerLRect, playerRRect;
        private void �ƶ�(Vector3 posL,Vector3 posR)
        {
            Debug.Log("�ƶ���");
            if (playerL == null)
            {
                TypeEventSystem.Global.UnRegister<����ʶ�����ݽ���>(����ƶ�);
                Debug.LogError("��߷�����bug��δ֪ԭ����");
                return;
            }
            playerL.Move(new Vector3(Mathf.Clamp(-posL.x, -960, 960), Mathf.Clamp(posL.y, -540, 540), 0));
            playerR.Move(new Vector3(Mathf.Clamp(-posR.x, -960, 960), Mathf.Clamp(posR.y, -540, 540), 0));
        }

        //���õ�ǰ�׶ε��ܷ���
        public void ���õ�ǰ�ȼ��ܷ���(int level)
        {
            NowLevelAllScore = levelAllScoreList[level-1];
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
            Time.timeScale = 1f;
            AudioKit.StopMusic();
            UIKit.OpenPanel<UIMousePanel>();
        }
        public bool �����ƶ�;
        public float countdownDuration = 10f; // ����ʱ�ĳ���ʱ�䣨�룩
        private float currentTime = 0f;
        private bool isCountingDown = false;
        private void FixedUpdate()
        {
            if (�����쳣���ʱ�� < 10)
            {
                if (DataMgr.û�м�⵽��)
                {
                    �����쳣���ʱ�� += Time.deltaTime;
                    if (�����쳣���ʱ�� > 1.0f)
                    {
                        checkStatus.gameObject.SetActive(true);
                        checkStatus.text = "��ⲻ������";
                        if (�����쳣���ʱ�� > 10)
                        {
                            �����쳣����();
                        }
                    }

                }
                else
                {
                    checkStatus.gameObject.SetActive(false);
                    �����쳣���ʱ�� = 0;
                }
            }
            if (!��������ʶ��)
            {
                �ƶ�(CanvasToScreenCoordinateConverter.Instance.��ĻתCanvasλ��(Input.mousePosition) - new Vector3(960, 540, 0), -CanvasToScreenCoordinateConverter.Instance.��ĻתCanvasλ��(Input.mousePosition) - new Vector3(960, 540, 0));
            }
            if (isCountingDown)
            {
                currentTime += Time.unscaledDeltaTime; // ʹ��Time.unscaledDeltaTime������ʱ��
                if (currentTime > 1)
                {
                    UpdateTimer();
                    currentTime = currentTime% 1;
                }

            }
        }
        void �����쳣����()
        {
            warmImg.gameObject.SetActive(true);
            checkStatus.gameObject.SetActive(true);
            warmImg.gameObject.SetActive(true);
            checkStatus.text = "�����쳣���";
        }


        private void UpdateTimer()
        {
            if (remainingTime > 0)
            {
                remainingTime -= 1.0f;
                if (remainingTime == 10)
                {
                    TimeImage.sprite = ��ɫ�׿�;
                }
                UpdateCountdownText();
            }
            else
            {
                Debug.Log("����ʱ����");
                isCountingDown = false;
                float ���� = 0;

                for (int ab = 0; ab < ·������.Count - 1; ab++)
                {
                    ���� += Vector3.Distance(·������[ab], ·������[ab + 1]);
                }
                UIKit.ClosePanel<UIAIPanel>();
                UIKit.OpenPanel<UIEndPanel>(
                    new UIEndPanelData()
                    {
                        �ɼ� = NowScore.ToString(),
                        ʱ�� = "1:00",
                        ���� = ��ȡ�ȼ�(),
                        ��·�� =((int) (���� / 500)).ToString(),
                        �ɼ��쳣= checkStatus.text == "�����쳣���"
                    }); ;

                //TextDisplayer.DisplayText("�ܹ��ƶ��˶��پ���:" ,����);
                CloseSelf();
            }
        }
        public string ��ȡ�ȼ�()
        {
            if (nowscore < 6)
            {
                return "D";
            }
            if (nowscore < 25)
            {
                return "D+";
            }
            if (nowscore < 26)
            {
                return "C";
            }
            if (nowscore < 55)
            {
                return "C+";
            }
            if (nowscore < 56)
            {
                return "B";
            }
            if (nowscore < 80)
            {
                return "B+";
            }
            if (nowscore < 81)
            {
                return "A";
            }
            if (nowscore < 95)
            {
                return "A+";
            }
            if (nowscore < 96)
            {
                return "S";
            }
            return "S+";
            
        }
        public List<Vector3> ·������ = new List<Vector3>();
        private void UpdateCountdownText()
        {
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            Debug.Log("����");
            TImeText.text = string.Format("{0}:{1:D2}", minutes, seconds);
            ·������.Add(playerLRect.anchoredPosition);
        }
        Vector3 oTo = Vector3.one * 1.2f;
        public int �ȼ�=0;
        public void SetScore(int score)
        {
            NowScore = score;
            //TextDisplayer.DisplayText("��ǰ�ķ���:" , NowScore);
            //ScoreText.DOText(NowScore.ToString(),0.05f);
            // ʹ��DOTween����һ������Ч���Ķ���
            ScoreText.DOText(NowScore.ToString(), 0.05f);
            ScoreText.transform.DOScale(oTo, 0.05f).SetLoops(2, LoopType.Yoyo).OnComplete(() => { ScoreText.transform.DOScale(Vector3.one, 0.05f); });
            if (�ȼ� < 5)
                if (score > ���ɽ׶�.����[�ȼ�])
                {
                    �ȼ�++;
                    TypeEventSystem.Global.Send<���µȼ�>(new ���µȼ� { ��ǰ�ȼ� = �ȼ� - 1 });
                }
        }

        public UIPanel CloseWithAnimation()
        {
            throw new System.NotImplementedException();
        }

        public bool isDefaultClose()
        {
            return true;
        }

        public UIPanel OpenAnimationPanel()
        {
            throw new System.NotImplementedException();
        }

        public bool isDefaultOpen()
        {
            return true;
        }
    }
}
