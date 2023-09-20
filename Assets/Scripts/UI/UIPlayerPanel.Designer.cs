using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:c501246e-d9f6-4bba-b4ee-25ef21495faf
	public partial class UIPlayerPanel
	{
		public const string Name = "UIPlayerPanel";
		
		[SerializeField]
		public UnityEngine.UI.Image PlayerL;
		[SerializeField]
		public RectTransform 升级;
		[SerializeField]
		public UnityEngine.UI.Image scoreEffectPrefab;
		[SerializeField]
		public UnityEngine.UI.Image TimeImage;
		[SerializeField]
		public UnityEngine.UI.Text TImeText;
		[SerializeField]
		public UnityEngine.UI.Text ScoreText;
		[SerializeField]
		public UnityEngine.UI.Image warmImg;
		[SerializeField]
		public UnityEngine.UI.Image SilederValue;
		[SerializeField]
		public UnityEngine.UI.Text checkStatus;
		[SerializeField]
		public UnityEngine.UI.Image wariIcon;
		[SerializeField]
		public UnityEngine.UI.Image PlayerR;
		[SerializeField]
		public UnityEngine.UI.Image 连切;
		[SerializeField]
		public UnityEngine.UI.Image Nice;
		[SerializeField]
		public UnityEngine.UI.Image Perfect;
		
		private UIPlayerPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			PlayerL = null;
			升级 = null;
			scoreEffectPrefab = null;
			TimeImage = null;
			TImeText = null;
			ScoreText = null;
			warmImg = null;
			SilederValue = null;
			checkStatus = null;
			wariIcon = null;
			PlayerR = null;
			连切 = null;
			Nice = null;
			Perfect = null;
			
			mData = null;
		}
		
		public UIPlayerPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIPlayerPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIPlayerPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
