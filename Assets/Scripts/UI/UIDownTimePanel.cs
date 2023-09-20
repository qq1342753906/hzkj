using UnityEngine;
using UnityEngine.UI;
using QFramework;
using DG.Tweening;
using Google.Protobuf.WellKnownTypes;
using System.Collections;
using System.Collections.Generic;

namespace QFramework.Example
{
    public class UIDownTimePanelData : UIPanelData
    {
    }
    public partial class UIDownTimePanel : UIPanel
    {
        public List<Sprite> SpriteList = new List<Sprite>();
        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIDownTimePanelData ?? new UIDownTimePanelData();
            countdownText.gameObject.SetActive(false);
            DownTimeSprite.gameObject.SetActive(false);
            ��ܰ��ʾ.gameObject.SetActive(false);
            ��ܰ��ʾ.transform.localScale = Vector3.zero;
            // please add init code here
            DOVirtual.DelayedCall(2, () =>
            {
                ��ܰ��ʾ.gameObject.SetActive(true);
                ��ܰ��ʾ.transform.DOScale(1, 0.5f);
            });
            ������Ϸ.onClick.AddListener(() => {
                gamePanel.GetComponent<RectTransform>().DOAnchorPos3DY(-1200, 0.5f);
                // �ӳ�3���ִ�еĲ���
                ��ܰ��ʾ.GetComponent<RectTransform>().DOAnchorPos3DY(-1200, 0.5f).OnComplete(() => { StartCoroutine(StartCountdown()); });
            });
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

        public Vector3 centerPosition; // ��Ļ����λ��
        public int countdownDuration = 3; // ����ʱ��ʱ��
        private float textScale = 18.0f; // ��ʼ���ű���
        private float timecount;

        private IEnumerator StartCountdown()
        {
            int currentTime = countdownDuration;
            DownTimeSprite.gameObject.SetActive(true);
            AudioKit.PlaySound("Resources://��Ч/321��ʼ");
            while (currentTime >= 0)
            {
                //            Text countdownText1 = Instantiate(countdownText.gameObject).GetComponent<Text>();
                //countdownText1.transform.parent = transform;
                //countdownText1.gameObject.SetActive(true);
                //            countdownText1.text = currentTime.ToString("F0");
                //            DownTimeSprite.sprite = SpriteList[currentTime];
                DownTimeSprite.sprite = SpriteList[currentTime];
                if (currentTime == 0)
                {
                    DownTimeSprite.transform.localScale = Vector3.zero;
                    DownTimeSprite.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutElastic);
                }
                DownTimeSprite.SetNativeSize();
                //countdownText1.transform.localScale = Vector3.zero;
                //countdownText1.transform.position = Vector3.zero;


                //countdownText1.transform.DOScale(textScale, 1.0f).SetEase(Ease.OutBack);
                //countdownText1.DOFade(0, 1.0f).OnComplete(() => Destroy(countdownText1.gameObject));

                yield return new WaitForSeconds(1.0f);
                currentTime--;
                if (currentTime < 0)
                {
                    UIKit.OpenPanel<UIBGPanel>();
                    UIKit.OpenPanel<UIPlayerPanel>();
                    UIKit.OpenPanel<UIAIPanel>();
                    UIKit.ClosePanel<UIMousePanel>();
                    CloseSelf();
                }
            }
        }
    }
}
