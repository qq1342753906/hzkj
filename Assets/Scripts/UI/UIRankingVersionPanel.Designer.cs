using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:f6495709-5a96-4b03-8f62-11706d03310c
	public partial class UIRankingVersionPanel
	{
		public const string Name = "UIRankingVersionPanel";
		
		[SerializeField]
		public UnityEngine.UI.Image BG;
		[SerializeField]
		public UnityEngine.UI.Text nameText;
		[SerializeField]
		public UnityEngine.UI.Button 重试按钮;
		[SerializeField]
		public UnityEngine.UI.Image imageDisplay;
		
		private UIRankingVersionPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			BG = null;
			nameText = null;
			重试按钮 = null;
			imageDisplay = null;
			
			mData = null;
		}
		
		public UIRankingVersionPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIRankingVersionPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIRankingVersionPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
