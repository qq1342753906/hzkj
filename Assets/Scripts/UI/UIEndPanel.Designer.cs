using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:c3b59354-1214-4d1a-9b1e-6148ddc92d6f
	public partial class UIEndPanel
	{
		public const string Name = "UIEndPanel";
		
		[SerializeField]
		public UnityEngine.UI.Text 成绩Value;
		[SerializeField]
		public UnityEngine.UI.Text 评价Value;
		[SerializeField]
		public UnityEngine.UI.Text 时间Value;
		[SerializeField]
		public UnityEngine.UI.Text 卡路里Value;
		[SerializeField]
		public UnityEngine.UI.Button 重试Btn;
		[SerializeField]
		public UnityEngine.UI.Text 超过用户Value;
		[SerializeField]
		public UnityEngine.UI.Image 成绩异常;
		[SerializeField]
		public UnityEngine.UI.Toggle 重试Toggle;
		[SerializeField]
		public UnityEngine.UI.Toggle 查看榜单Toggle;
		[SerializeField]
		public UnityEngine.UI.Image imageDisplay;
		[SerializeField]
		public UnityEngine.UI.Button 查看板单;
		
		private UIEndPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			成绩Value = null;
			评价Value = null;
			时间Value = null;
			卡路里Value = null;
			重试Btn = null;
			超过用户Value = null;
			成绩异常 = null;
			重试Toggle = null;
			查看榜单Toggle = null;
			imageDisplay = null;
			查看板单 = null;
			
			mData = null;
		}
		
		public UIEndPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIEndPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIEndPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
