using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:9aadd78e-19a8-4231-86b0-f9230c2eae0f
	public partial class UIMainPanel
	{
		public const string Name = "UIMainPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button 开始游戏按钮;
		[SerializeField]
		public UnityEngine.UI.Button 退出游戏按钮;
		
		private UIMainPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			开始游戏按钮 = null;
			退出游戏按钮 = null;
			
			mData = null;
		}
		
		public UIMainPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIMainPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIMainPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
