using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:2161d877-3d77-449e-99a5-6e0be156e72b
	public partial class UIDetectionBodyPanel
	{
		public const string Name = "UIDetectionBodyPanel";
		
		[SerializeField]
		public UnityEngine.UI.Image TipsPanel;
		[SerializeField]
		public UnityEngine.UI.Image StartGameImg;
		
		private UIDetectionBodyPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			TipsPanel = null;
			StartGameImg = null;
			
			mData = null;
		}
		
		public UIDetectionBodyPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIDetectionBodyPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIDetectionBodyPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
