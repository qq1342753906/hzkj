using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:54338271-8a82-40ab-8f9b-baa2cdb659f4
	public partial class UIDownTimePanel
	{
		public const string Name = "UIDownTimePanel";
		
		[SerializeField]
		public UnityEngine.UI.Text countdownText;
		[SerializeField]
		public UnityEngine.UI.Image gamePanel;
		[SerializeField]
		public UnityEngine.UI.Image DownTimeSprite;
		[SerializeField]
		public UnityEngine.UI.Image 温馨提示;
		[SerializeField]
		public UnityEngine.UI.Button 进入游戏;
		
		private UIDownTimePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			countdownText = null;
			gamePanel = null;
			DownTimeSprite = null;
			温馨提示 = null;
			进入游戏 = null;
			
			mData = null;
		}
		
		public UIDownTimePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIDownTimePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIDownTimePanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
