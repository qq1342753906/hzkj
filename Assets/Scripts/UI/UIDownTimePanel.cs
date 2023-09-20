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
            温馨提示.gameObject.SetActive(false);
            温馨提示.transform.localScale = Vector3.zero;
            // please add init code here
            DOVirtual.DelayedCall(2, () =>
            {
                温馨提示.gameObject.SetActive(true);
                温馨提示.transform.DOScale(1, 0.5f);
            });
            进入游戏.onClick.AddListener(() => {
                gamePanel.GetComponent<RectTransform>().DOAnchorPos3DY(-1200, 0.5f);
                // 延迟3秒后执行的操作
                温馨提示.GetComponent<RectTransform>().DOAnchorPos3DY(-1200, 0.5f).OnComplete(() => { StartCoroutine(StartCountdown()); });
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

        public Vector3 centerPosition; // 屏幕中心位置
        public int countdownDuration = 3; // 倒计时总时长
        private float textScale = 18.0f; // 初始缩放比例
        private float timecount;

        private IEnumerator StartCountdown()
        {
            int currentTime = countdownDuration;
            DownTimeSprite.gameObject.SetActive(true);
            AudioKit.PlaySound("Resources://音效/321开始");
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
