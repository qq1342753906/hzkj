using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:98856cf9-4212-4b2e-ac4b-883c2ab19eb7
	public partial class Boster
	{
		public const string Name = "Boster";
		
		
		private BosterData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			
			mData = null;
		}
		
		public BosterData Data
		{
			get
			{
				return mData;
			}
		}
		
		BosterData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new BosterData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
