using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:7c12609e-93a0-4064-8527-f9c825aa2346
	public partial class TestUIGameStartPanel
	{
		public const string Name = "TestUIGameStartPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button BtnCoinUpgrade;
		[SerializeField]
		public UnityEngine.UI.Button BtnStartGame;
		[SerializeField]
		public CoinUpgradePanel CoinUpgradePanel;
		
		private TestUIGameStartPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			BtnCoinUpgrade = null;
			BtnStartGame = null;
			CoinUpgradePanel = null;
			
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
