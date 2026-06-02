/****************************************************************************
 * 2026.6 鲁童昕的MacBook Pro
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	public partial class CoinUpgradePanel : UIElement,IController
	{
		private void Awake()
		{
			foreach(var coinUpgradeItem in this.GetSystem<CoinUpgradeSystem>().Items)
			{
				CoinUpgradeItemTemplate.InstantiateWithParent(CoinUpgradeItemRoot)
				.Self(self =>
				{
					var itemCache = coinUpgradeItem;
					self.GetComponentInChildren<Text>().text = itemCache.Description + $" (Price: {itemCache.Price})";
					self.onClick.AddListener(() =>
					{
						itemCache.Upgrade();
						AudioKit.PlaySound("AbilityLevelUp");
					});
				})
				.Show();
			}
			BtnCoinPercentUpgrade.Hide();
			BtnExpPercentUpgrade.Hide();
			BtnMaxHpUpgrade.Hide();
			// Global.Coin.RegisterWithInitValue((coin) =>
			// {
			// 	CoinText.text = $"Coin: {coin}";
			// 	if(coin >= 5)
			// 	{
			// 		BtnCoinPercentUpgrade.Show();
			// 		BtnExpPercentUpgrade.Show();
			// 		BtnMaxHpUpgrade.Show();
			// 	}
			// 	else
			// 	{
			// 		BtnCoinPercentUpgrade.Hide();
			// 		BtnExpPercentUpgrade.Hide();
			// 		BtnMaxHpUpgrade.Hide();
			// 	}
			// }).UnRegisterWhenGameObjectDestroyed(gameObject);
			BtnCoinPercentUpgrade.onClick.AddListener(() =>
			{
				Global.CoinPercent.Value += 0.1f;
				Global.Coin.Value -= 5;
				PlayerPrefs.SetInt(nameof(Coin),Global.Coin.Value);
			
			});
			BtnExpPercentUpgrade.onClick.AddListener(() =>
			{
				Global.ExpPercent.Value += 0.1f;
				Global.Coin.Value -= 5;
				PlayerPrefs.SetInt(nameof(Coin),Global.Coin.Value);
				AudioKit.PlaySound("AbilityLevelUp");
			});
			BtnClose.onClick.AddListener(() =>
			{
				this.Hide();
			});
			BtnMaxHpUpgrade.onClick.AddListener(() =>
			{
				Global.MaxHP.Value += 1;
				Global.HP.Value = Global.MaxHP.Value;
				Global.Coin.Value -= 30;
				PlayerPrefs.SetInt(nameof(Coin),Global.Coin.Value);
				PlayerPrefs.SetInt("MaxHP",Global.MaxHP.Value);
				AudioKit.PlaySound("AbilityLevelUp");
				this.Hide();
			});
		}

		protected override void OnBeforeDestroy()
		{
		}

        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }
    }
}