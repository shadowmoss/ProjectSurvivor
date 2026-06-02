/****************************************************************************
 * 2026.6 鲁童昕的MacBook Pro
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	public partial class CoinUpgradePanel
	{
		[SerializeField] public UnityEngine.UI.Image BG;
		[SerializeField] public UnityEngine.UI.Button BtnCoinPercentUpgrade;
		[SerializeField] public UnityEngine.UI.Button BtnExpPercentUpgrade;
		[SerializeField] public UnityEngine.UI.Button BtnClose;
		[SerializeField] public UnityEngine.UI.Text CoinText;
		[SerializeField] public UnityEngine.UI.Button BtnMaxHpUpgrade;
		[SerializeField] public RectTransform CoinUpgradeItemRoot;
		[SerializeField] public UnityEngine.UI.Button CoinUpgradeItemTemplate;

		public void Clear()
		{
			BG = null;
			BtnCoinPercentUpgrade = null;
			BtnExpPercentUpgrade = null;
			BtnClose = null;
			CoinText = null;
			BtnMaxHpUpgrade = null;
			CoinUpgradeItemRoot = null;
			CoinUpgradeItemTemplate = null;
		}

		public override string ComponentName
		{
			get { return "CoinUpgradePanel";}
		}
	}
}
