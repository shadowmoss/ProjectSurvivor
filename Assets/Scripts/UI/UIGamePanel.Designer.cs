using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:44599810-6b39-496f-9279-3317c016252c
	public partial class UIGamePanel
	{
		public const string Name = "UIGamePanel";
		
		[SerializeField]
		public UnityEngine.UI.Text ExpText;
		[SerializeField]
		public UnityEngine.UI.Text LevelText;
		[SerializeField]
		public UnityEngine.UI.Button BtnUpgrade;
		[SerializeField]
		public UnityEngine.UI.Text TimeText;
		[SerializeField]
		public UnityEngine.UI.Text EnemyCountText;
		
		private UIGamePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			ExpText = null;
			LevelText = null;
			BtnUpgrade = null;
			TimeText = null;
			EnemyCountText = null;
			
			mData = null;
		}
		
		public UIGamePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGamePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGamePanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
