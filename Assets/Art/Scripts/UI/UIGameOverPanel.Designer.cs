using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	// Generate Id:dd1bd386-9119-4972-b8a5-308adf327963
	public partial class UIGameOverPanel
	{
		public const string Name = "UIGameOverPanel";
		
		
		private UIGameOverPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			
			mData = null;
		}
		
		public UIGameOverPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGameOverPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGameOverPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
