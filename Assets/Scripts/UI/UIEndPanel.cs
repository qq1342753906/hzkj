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
		public string �ɼ�, ����, ʱ��, ��·��;
        public bool �ɼ��쳣;
	}
	public partial class UIEndPanel : UIPanel, ICloseable
    {
        protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIEndPanelData ?? new UIEndPanelData();
			// please add init code here
			�ɼ�Value.text = mData.�ɼ�;
            ����Value.text = mData.����;
            ʱ��Value.text = mData.ʱ��;
            ��·��Value.text = mData.��·��;
            �����û�Value.text ="����"+ ����(int.Parse(mData.�ɼ�))+ "���û�����";
            if (mData.�ɼ��쳣)
            {
                �ɼ��쳣.gameObject.SetActive(true);
            }
            else
            {
                �ɼ��쳣.gameObject.SetActive(false);
            }
            imageDisplay.gameObject.SetActive(false);
            StartCoroutine(PlaySlideshow());
            ����Btn.onClick.AddListener(() => { ����(); CloseSelf(); });
            �鿴�嵥.onClick.AddListener(() => { UIKit.OpenPanel<UIRankingVersionPanel>();CloseSelf(); });
          //  TypeEventSystem.Global.Register<�������»���״̬>(�������»���״̬).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        void ����()
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
				����();
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


        private string filePath; // �ļ�·��
        private List<int> leaderboardData; // ���а������б�

        private string ����(int newScore)
        {
            filePath = Path.Combine(Application.persistentDataPath, "leaderboard.json");

            if (File.Exists(filePath))
            {
                // ������а��ļ����ڣ����ȡ����
                string json = File.ReadAllText(filePath);
                leaderboardData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(json);
            }
            else
            {
                // ������а��ļ������ڣ��򴴽��µ����ݲ�����
                leaderboardData = new List<int>();
                SaveLeaderboardData();
            }


            int rank = GetRankForScore(newScore);

            Debug.Log("��ķ����������е�λ��: " + rank);

            // ��������ڰٷֱ�λ��
            float percentage = (float)(leaderboardData.Count-rank) / leaderboardData.Count * 100;
            Debug.Log("��ķ����������еİٷֱ�λ��: " + percentage.ToString("0.00") + "%");
            return percentage.ToString("0.00") + "%";
        }

        private int GetRankForScore(int score)
        {
            // ���µķ������������б��У�������������
            leaderboardData.Add(score);
            leaderboardData.Sort((a, b) => b.CompareTo(a));
            foreach(var v in leaderboardData)
            {
                Debug.Log("����֮�����:" + v);
            }
            // �ҵ��·�����������б��е�����λ��
            int rank = leaderboardData.IndexOf(score);

            SaveLeaderboardData(); // ������º������

            return rank;
        }

        private void SaveLeaderboardData()
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(leaderboardData);
            File.WriteAllText(filePath, json);
        }
        public List<Sprite> images; // ���ͼƬ
        public float slideDuration = 5f; // ͼƬ�л����ʱ��

        private int currentIndex = 0; // ��ǰ��ʾͼƬ������
        void �������»���״̬(�������»���״̬ s)
        {
            if (DataMgr.���ƻ���ʱ���� > 1)
            {
                DataMgr.ResertTime();
                ThreadToMain.ToMain(1, () => {
                    if(imageDisplay.gameObject.activeSelf)
                    ����();
                });
            }

        }

        private IEnumerator PlaySlideshow()
        {
            yield return new WaitForSeconds(60);
            imageDisplay.gameObject.SetActive(true);
            // ��ʼ����ʾ��һ��ͼƬ
            if (images.Count > 0)
            {
                imageDisplay.sprite = images[currentIndex];
                imageDisplay.DOFade(1, 0.5f);
            }
            while (true)
            {
                // �ӳ�Ƭ��
                yield return new WaitForSeconds(slideDuration);

                // �л�����һ��ͼƬ
                currentIndex = (currentIndex + 1) % images.Count;
                imageDisplay.sprite = images[currentIndex];
                imageDisplay.DOFade(1, 0.5f);
                // ʹ��DOTweenʵ�ֽ����л�Ч��
                //imageDisplay.DOFade(0, 0.5f).OnComplete(() =>
                //{

                //});
            }
        }
    }
}
