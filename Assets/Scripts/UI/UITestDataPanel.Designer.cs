using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:6f11782e-1fc3-44da-9fc2-83a2564744a4
	public partial class UITestDataPanel
	{
		public const string Name = "UITestDataPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button Close;
		[SerializeField]
		public UnityEngine.UI.Slider Y区间进度条;
		[SerializeField]
		public UnityEngine.UI.Slider 鱼出现频率进度条;
		[SerializeField]
		public UnityEngine.UI.InputField Y区间输入框;
		[SerializeField]
		public UnityEngine.UI.InputField 鱼出现频率输入框;
		[SerializeField]
		public UnityEngine.UI.Text WaringText;
		
		private UITestDataPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			Close = null;
			Y区间进度条 = null;
			鱼出现频率进度条 = null;
			Y区间输入框 = null;
			鱼出现频率输入框 = null;
			WaringText = null;
			
			mData = null;
		}
		
		public UITestDataPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UITestDataPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UITestDataPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
