/****************************************************************************
 * 2026.6 鲁童昕的MacBook Pro
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Linq;

namespace QFramework.Example
{
	public partial class CoinUpgradePanel : UIElement,IController
	{
		public void Refresh()
		{
			CoinUpgradeItemRoot.DestroyChildren();
			foreach(var coinUpgradeItem in this.GetSystem<CoinUpgradeSystem>().Items.Where(item=>item.ConditionCheck()))
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
					Button selfCache = self;
					
					Global.Coin.RegisterWithInitValue(coin =>
					{
						if(Global.Coin.Value <= itemCache.Price)
						{
							self.interactable = false;
						}
						else
						{
							self.interactable = true;
						}
					}).UnRegisterWhenGameObjectDestroyed(selfCache);
				}).Show();
			}
		}
		private void Awake()
		{
			CoinUpgradeItemTemplate.Hide();
			CoinUpgradeSystem.OnCoinUpgradeSystemChanged.Register(()=>{
				Refresh();
			}).UnRegisterWhenGameObjectDestroyed(this);
			Global.Coin.Value = PlayerPrefs.GetInt(nameof(Coin),0);
			Global.Coin.RegisterWithInitValue(coin =>
			{
				CoinText.text = $"Coin:{coin}";
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			BtnClose.onClick.AddListener(() =>
			{
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