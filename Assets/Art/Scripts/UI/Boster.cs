using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	public class BosterData : UIPanelData
	{
	}
	public partial class Boster : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as BosterData ?? new BosterData();
			// please add init code here
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
