using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:840f4fa0-c476-4dba-bbac-45dc6a9b6eaf
	public partial class UIMousePanel
	{
		public const string Name = "UIMousePanel";
		
		[SerializeField]
		public UnityEngine.UI.Image 鼠标;
		[SerializeField]
		public UnityEngine.UI.Image 鼠标检测轮廓;
		
		private UIMousePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			鼠标 = null;
			鼠标检测轮廓 = null;
			
			mData = null;
		}
		
		public UIMousePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIMousePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIMousePanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
