using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:e8f1c7b1-0ce3-4494-b376-80d6688dacb4
	public partial class TestUIGameStartPanel
	{
		public const string Name = "TestUIGameStartPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button BtnCoinUpgrade;
		[SerializeField]
		public RectTransform CoinUpgradePanel;
		[SerializeField]
		public UnityEngine.UI.Image BG;
		[SerializeField]
		public UnityEngine.UI.Button BtnCoinPercentUpgrade;
		[SerializeField]
		public UnityEngine.UI.Button BtnExpPercentUpgrade;
		[SerializeField]
		public UnityEngine.UI.Button BtnClose;
		[SerializeField]
		public UnityEngine.UI.Text CoinText;
		[SerializeField]
		public UnityEngine.UI.Button BtnStartGame;
		
		private TestUIGameStartPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			BtnCoinUpgrade = null;
			CoinUpgradePanel = null;
			BG = null;
			BtnCoinPercentUpgrade = null;
			BtnExpPercentUpgrade = null;
			BtnClose = null;
			CoinText = null;
			BtnStartGame = null;
			
			mData = null;
		}
		
		public TestUIGameStartPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		TestUIGameStartPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new TestUIGameStartPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
