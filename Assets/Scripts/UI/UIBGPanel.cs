using UnityEngine;
using UnityEngine.UI;
using QFramework;
using DG.Tweening;
using System.Collections.Generic;

namespace QFramework.Example
{
	public class UIBGPanelData : UIPanelData
	{
	}
	public partial class UIBGPanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIBGPanelData ?? new UIBGPanelData();
			// please add init code here
		}
		public Sprite p2;
		public List<Sprite> ls = new List<Sprite>();
		private float 产生环境速度;
        private void FixedUpdate()
        {
			if (产生环境速度 > 1f)
			{
				GameObject p = GameObject.Instantiate(pp.gameObject);
				p.transform.parent = transform;
				float scale = Random.Range(0.4f, 1.2f);
                p.GetComponent<Image>().sprite = ls[Random.Range(0, 4)];
                p.GetComponent<Image>().SetNativeSize();
                p.transform.localScale = new Vector3(scale, scale, scale);
				p.transform.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(Random.Range(-900, 900), -600, 0);
				p.transform.GetComponent<RectTransform>().DOAnchorPos3DY(600, 9).OnComplete(()=>Destroy(p.gameObject));
				p.transform.GetComponent<RectTransform>().DORotate(Vector3.forward*50, 9);

                产生环境速度 = 0;
            }
			产生环境速度 += Time.deltaTime;
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
	}
}
