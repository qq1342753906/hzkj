using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	public class UIMainPanelData : UIPanelData
	{
	}
	public partial class UIMainPanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIMainPanelData ?? new UIMainPanelData();
			// please add init code here
			��ʼ��Ϸ��ť.onClick.AddListener(() => { UIKit.OpenPanel<UIDetectionBodyPanel>();CloseSelf(); });
			�˳���Ϸ��ť.onClick.AddListener(() => { Application.Quit(); });
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
