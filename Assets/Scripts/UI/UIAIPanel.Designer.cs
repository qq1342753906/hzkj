using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:d923a852-881a-463f-a801-0e0634e8d2da
	public partial class UIAIPanel
	{
		public const string Name = "UIAIPanel";
		
		
		private UIAIPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			
			mData = null;
		}
		
		public UIAIPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIAIPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIAIPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
