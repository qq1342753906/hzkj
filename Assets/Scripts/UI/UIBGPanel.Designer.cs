using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:0140162e-f93a-4f78-8cba-442814e076fb
	public partial class UIBGPanel
	{
		public const string Name = "UIBGPanel";
		
		[SerializeField]
		public UnityEngine.UI.Image pp;
		
		private UIBGPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			pp = null;
			
			mData = null;
		}
		
		public UIBGPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIBGPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIBGPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
