using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:99d86c16-4f7f-4eb8-95c9-9cf226741228
	public partial class UIAnnotatableScreenPanel
	{
		public const string Name = "UIAnnotatableScreenPanel";
		
		
		private UIAnnotatableScreenPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			
			mData = null;
		}
		
		public UIAnnotatableScreenPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIAnnotatableScreenPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIAnnotatableScreenPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
