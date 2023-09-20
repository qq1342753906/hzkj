using DG.Tweening;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace QFramework.Example
{
    public class UIEndPanelData : UIPanelData
	{
		public string 成绩, 评价, 时间, 卡路里;
        public bool 成绩异常;
	}
	public partial class UIEndPanel : UIPanel, ICloseable
    {
        protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIEndPanelData ?? new UIEndPanelData();
			// please add init code here
			成绩Value.text = mData.成绩;
            评价Value.text = mData.评价;
            时间Value.text = mData.时间;
            卡路里Value.text = mData.卡路里;
            超过用户Value.text ="超过"+ 排名(int.Parse(mData.成绩))+ "的用户排名";
            if (mData.成绩异常)
            {
                成绩异常.gameObject.SetActive(true);
            }
            else
            {
                成绩异常.gameObject.SetActive(false);
            }
            imageDisplay.gameObject.SetActive(false);
            StartCoroutine(PlaySlideshow());
            重试Btn.onClick.AddListener(() => { 重试(); CloseSelf(); });
            查看板单.onClick.AddListener(() => { UIKit.OpenPanel<UIRankingVersionPanel>();CloseSelf(); });
          //  TypeEventSystem.Global.Register<手掌上下滑动状态>(手掌上下滑动状态).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        void 重试()
		{
			UIKit.OpenPanel<UIPlayerPanel>();
            if (UIKit.GetPanel<UIMousePanel>() != null)
            {
                UIKit.ClosePanel<UIMousePanel>();
            }
            UIKit.OpenPanel<UIAIPanel>();
		}
        private void Update()
        {
			if (Input.GetKeyUp(KeyCode.J))
			{
				重试();
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
		}

        public UIPanel CloseWithAnimation()
        {
            throw new NotImplementedException();
        }

        public bool isDefaultClose()
        {
			return true;
        }

        public UIPanel OpenAnimationPanel()
        {
            throw new NotImplementedException();
        }

        public bool isDefaultOpen()
        {
			return true;
        }


        private string filePath; // 文件路径
        private List<int> leaderboardData; // 排行榜数据列表

        private string 排名(int newScore)
        {
            filePath = Path.Combine(Application.persistentDataPath, "leaderboard.json");

            if (File.Exists(filePath))
            {
                // 如果排行榜文件存在，则读取数据
                string json = File.ReadAllText(filePath);
                leaderboardData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(json);
            }
            else
            {
                // 如果排行榜文件不存在，则创建新的数据并保存
                leaderboardData = new List<int>();
                SaveLeaderboardData();
            }


            int rank = GetRankForScore(newScore);

            Debug.Log("你的分数在排名中的位置: " + rank);

            // 输出分数在百分比位置
            float percentage = (float)(leaderboardData.Count-rank) / leaderboardData.Count * 100;
            Debug.Log("你的分数在排名中的百分比位置: " + percentage.ToString("0.00") + "%");
            return percentage.ToString("0.00") + "%";
        }

        private int GetRankForScore(int score)
        {
            // 将新的分数插入数据列表中，并按降序排序
            leaderboardData.Add(score);
            leaderboardData.Sort((a, b) => b.CompareTo(a));
            foreach(var v in leaderboardData)
            {
                Debug.Log("排序之后输出:" + v);
            }
            // 找到新分数在排序后列表中的索引位置
            int rank = leaderboardData.IndexOf(score);

            SaveLeaderboardData(); // 保存更新后的数据

            return rank;
        }

        private void SaveLeaderboardData()
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(leaderboardData);
            File.WriteAllText(filePath, json);
        }
        public List<Sprite> images; // 存放图片
        public float slideDuration = 5f; // 图片切换间隔时间

        private int currentIndex = 0; // 当前显示图片的索引
        void 手掌上下滑动状态(手掌上下滑动状态 s)
        {
            if (DataMgr.手掌滑动时间间隔 > 1)
            {
                DataMgr.ResertTime();
                ThreadToMain.ToMain(1, () => {
                    if(imageDisplay.gameObject.activeSelf)
                    重试();
                });
            }

        }

        private IEnumerator PlaySlideshow()
        {
            yield return new WaitForSeconds(60);
            imageDisplay.gameObject.SetActive(true);
            // 初始化显示第一张图片
            if (images.Count > 0)
            {
                imageDisplay.sprite = images[currentIndex];
                imageDisplay.DOFade(1, 0.5f);
            }
            while (true)
            {
                // 延迟片刻
                yield return new WaitForSeconds(slideDuration);

                // 切换到下一张图片
                currentIndex = (currentIndex + 1) % images.Count;
                imageDisplay.sprite = images[currentIndex];
                imageDisplay.DOFade(1, 0.5f);
                // 使用DOTween实现渐变切换效果
                //imageDisplay.DOFade(0, 0.5f).OnComplete(() =>
                //{

                //});
            }
        }
    }
}
