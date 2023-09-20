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
        public RectTransform sbg;       // 进度条背景
        public RectTransform sbgValue;  // 进度条进度
        [Range(0, 1)]
        private float currentValue = 0; // 当前进度值（范围：0 - 1）
        float totalTime = 60; // 总倒计时时间（秒）

        private float remainingTime; // 剩余倒计时时间（秒）
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
        public Sprite 红色底框;
        public float 真人异常检测时间;
        bool 真人人体识别=true;
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
            AudioKit.PlayMusic("resources://音效/背景音效");
        }
        public 昆虫 生成阶段;
        private void initData()
        {
            isCountingDown = true;
            Time.timeScale = 0.5f;
            DataMgr.当前阶段 = 1;
            PlayerData = Newtonsoft.Json.JsonConvert.DeserializeObject<AIData>(Resources.Load<TextAsset>("DataText/playerCfig").text);
            生成阶段 = Newtonsoft.Json.JsonConvert.DeserializeObject<昆虫>(Resources.Load<TextAsset>("DataText/阶段").text);
            playerL = PlayerL.GetComponent<Player>();
            playerLRect = PlayerL.GetComponent<RectTransform>();
            playerR = PlayerR.GetComponent<Player>();
            playerRRect = PlayerR.GetComponent<RectTransform>();
            playerL.依赖注入(PlayerData);
            playerR.依赖注入(PlayerData);
            rect = GameObject.FindObjectOfType<AutoFit>().GetComponent<RectTransform>().rect;
            remainingTime = totalTime;
            UpdateCountdownText();
        }

        private void initEvent()
        {
            TypeEventSystem.Global.Register<姿势识别数据解析>(玩家移动).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void initObj()
        {
            checkStatus.gameObject.SetActive(false);
            warmImg.gameObject.SetActive(false);
            wariIcon.gameObject.SetActive(false);
            连切.gameObject.SetActive(false);
            Nice.gameObject.SetActive(false);
            Perfect.gameObject.SetActive(false);
        }

        float agoPosX;
        [Range(0,-500)]
        public float RangeY = -250f;
        private void 玩家移动(姿势识别数据解析 zs)
        {
            if (!真人人体识别)
            {
                return;
            }
            Debug.Log("玩家移动解析:");
            ThreadToMain.ToMain(1, () =>
            {
                var landmark = zs.nl.Landmark;
                var v1 = ImageCoordinate.ImageNormalizedToPoint(rect, landmark[0].X, landmark[0].Y, 0, RotationAngle.Rotation0, false);
                var v2 = ImageCoordinate.ImageNormalizedToPoint(rect, landmark[19].X, landmark[19].Y, 0, RotationAngle.Rotation0, false);
                var v3 = ImageCoordinate.ImageNormalizedToPoint(rect, landmark[20].X, landmark[20].Y, 0, RotationAngle.Rotation0, false);
                移动(v2,v3);
            }
            );
        }
        public Player playerL,playerR;
        public RectTransform playerLRect, playerRRect;
        private void 移动(Vector3 posL,Vector3 posR)
        {
            Debug.Log("移动了");
            if (playerL == null)
            {
                TypeEventSystem.Global.UnRegister<姿势识别数据解析>(玩家移动);
                Debug.LogError("这边发现了bug，未知原因导致");
                return;
            }
            playerL.Move(new Vector3(Mathf.Clamp(-posL.x, -960, 960), Mathf.Clamp(posL.y, -540, 540), 0));
            playerR.Move(new Vector3(Mathf.Clamp(-posR.x, -960, 960), Mathf.Clamp(posR.y, -540, 540), 0));
        }

        //设置当前阶段的总分数
        public void 设置当前等级总分数(int level)
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
        public bool 可以移动;
        public float countdownDuration = 10f; // 倒计时的持续时间（秒）
        private float currentTime = 0f;
        private bool isCountingDown = false;
        private void FixedUpdate()
        {
            if (真人异常检测时间 < 10)
            {
                if (DataMgr.没有检测到人)
                {
                    真人异常检测时间 += Time.deltaTime;
                    if (真人异常检测时间 > 1.0f)
                    {
                        checkStatus.gameObject.SetActive(true);
                        checkStatus.text = "检测不到真人";
                        if (真人异常检测时间 > 10)
                        {
                            触发异常警告();
                        }
                    }

                }
                else
                {
                    checkStatus.gameObject.SetActive(false);
                    真人异常检测时间 = 0;
                }
            }
            if (!真人人体识别)
            {
                移动(CanvasToScreenCoordinateConverter.Instance.屏幕转Canvas位置(Input.mousePosition) - new Vector3(960, 540, 0), -CanvasToScreenCoordinateConverter.Instance.屏幕转Canvas位置(Input.mousePosition) - new Vector3(960, 540, 0));
            }
            if (isCountingDown)
            {
                currentTime += Time.unscaledDeltaTime; // 使用Time.unscaledDeltaTime来减少时间
                if (currentTime > 1)
                {
                    UpdateTimer();
                    currentTime = currentTime% 1;
                }

            }
        }
        void 触发异常警告()
        {
            warmImg.gameObject.SetActive(true);
            checkStatus.gameObject.SetActive(true);
            warmImg.gameObject.SetActive(true);
            checkStatus.text = "真人异常检测";
        }


        private void UpdateTimer()
        {
            if (remainingTime > 0)
            {
                remainingTime -= 1.0f;
                if (remainingTime == 10)
                {
                    TimeImage.sprite = 红色底框;
                }
                UpdateCountdownText();
            }
            else
            {
                Debug.Log("倒计时结束");
                isCountingDown = false;
                float 距离 = 0;

                for (int ab = 0; ab < 路径计算.Count - 1; ab++)
                {
                    距离 += Vector3.Distance(路径计算[ab], 路径计算[ab + 1]);
                }
                UIKit.ClosePanel<UIAIPanel>();
                UIKit.OpenPanel<UIEndPanel>(
                    new UIEndPanelData()
                    {
                        成绩 = NowScore.ToString(),
                        时间 = "1:00",
                        评价 = 获取等级(),
                        卡路里 =((int) (距离 / 500)).ToString(),
                        成绩异常= checkStatus.text == "真人异常检测"
                    }); ;

                //TextDisplayer.DisplayText("总共移动了多少距离:" ,距离);
                CloseSelf();
            }
        }
        public string 获取等级()
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
        public List<Vector3> 路径计算 = new List<Vector3>();
        private void UpdateCountdownText()
        {
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            Debug.Log("更新");
            TImeText.text = string.Format("{0}:{1:D2}", minutes, seconds);
            路径计算.Add(playerLRect.anchoredPosition);
        }
        Vector3 oTo = Vector3.one * 1.2f;
        public int 等级=0;
        public void SetScore(int score)
        {
            NowScore = score;
            //TextDisplayer.DisplayText("当前的分数:" , NowScore);
            //ScoreText.DOText(NowScore.ToString(),0.05f);
            // 使用DOTween创建一个弹簧效果的动画
            ScoreText.DOText(NowScore.ToString(), 0.05f);
            ScoreText.transform.DOScale(oTo, 0.05f).SetLoops(2, LoopType.Yoyo).OnComplete(() => { ScoreText.transform.DOScale(Vector3.one, 0.05f); });
            if (等级 < 5)
                if (score > 生成阶段.分数[等级])
                {
                    等级++;
                    TypeEventSystem.Global.Send<更新等级>(new 更新等级 { 当前等级 = 等级 - 1 });
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
