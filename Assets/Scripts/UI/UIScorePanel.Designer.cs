using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:aa879c57-1217-4009-898d-0f289989c044
	public partial class UIScorePanel
	{
		public const string Name = "UIScorePanel";
		
		
		private UIScorePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			
			mData = null;
		}
		
		public UIScorePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIScorePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIScorePanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
