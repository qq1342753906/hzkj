using DG.Tweening;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace QFramework.Example
{
    public class UIRankingVersionPanelData : UIPanelData
	{
	}
	public partial class UIRankingVersionPanel : UIPanel,ICloseable
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIRankingVersionPanelData ?? new UIRankingVersionPanelData();
			// please add init code here
			initData();
			initObj();
            AudioKit.PlaySound("Resources://��Ч/���а���Ч");
            TypeEventSystem.Global.Register<�������»���״̬>(�������»���״̬).UnRegisterWhenGameObjectDestroyed(gameObject);
            ���԰�ť.onClick.AddListener(() => ����());
        }
        void �������»���״̬(�������»���״̬ s)
        {
            if (DataMgr.���ƻ���ʱ���� > 1)
            {
                DataMgr.ResertTime();
                ThreadToMain.ToMain(1, () => {
                    ����();
                });
            }

        }
        void ����()
        {
            UIKit.ClosePanel<UIMousePanel>();
            UIKit.OpenPanel<UIPlayerPanel>();
            UIKit.OpenPanel<UIAIPanel>();
            CloseSelf();
        }
        private void initObj()
        {
            imageDisplay.gameObject.SetActive(false);
            nameText.gameObject.SetActive(false);
            imageDisplay.color = new Color(1, 1, 1, 0);
            // ����Э�̽��лõ�Ƭ����
            StartCoroutine(PlaySlideshow());
            for (int n = 0; n < 2; n++)
			{
				for(int nn = 0; nn < 6; nn++)
				{
                    GameObject obj = Instantiate(nameText.gameObject,BG.transform);
					obj.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-285 + n * 512, 85.4f + nn * -69);
                    GameObject obj2 = Instantiate(nameText.gameObject, BG.transform);
                    obj2.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-90 + n * 512, 85.4f + nn * -69);
					obj2.GetComponent<Text>().text = ����(n * 6 + nn);
                }
			}

        }
        List<int> leaderboardData = new List<int>();
        private string ����(int pos)
        {
			if (pos < leaderboardData.Count)
			{
                return leaderboardData[pos].ToString();
            }
			return "0";
        }
        private void initData()
        {
            var filePath = Path.Combine(Application.persistentDataPath, "leaderboard.json");

            if (File.Exists(filePath))
            {
                // ������а��ļ����ڣ����ȡ����
                string json = File.ReadAllText(filePath);
                leaderboardData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(json);
            }
            leaderboardData.Sort((a, b) => b.CompareTo(a));
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

        public object Clone()
        {
            throw new NotImplementedException();
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


        public List<Sprite> images; // ���ͼƬ
        public float slideDuration = 5f; // ͼƬ�л����ʱ��

        private int currentIndex = 0; // ��ǰ��ʾͼƬ������


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

        public GameObject agoObj;
        private IEnumerator PlaySlideshow2()
        {
            while (true)
            {
                //yield return new WaitForSeconds(slideDuration);
                GameObject obj = Instantiate(imageDisplay.gameObject);
                int nextIndex = (currentIndex + 1) % images.Count;
                obj.GetComponent<UnityEngine.UI.Image>().sprite= images[nextIndex];
                // Move image out of screen
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 1080);
                // Animate image sliding in from the top
                obj.GetComponent<RectTransform>().DOAnchorPosY(0, 0.5f);
                currentIndex = nextIndex;
                yield return new WaitForSeconds(5);
                obj.GetComponent<RectTransform>().DOAnchorPosY(-1080, 0.5f).OnComplete(()=> { Destroy(obj); });

            }
        }
    }
}
