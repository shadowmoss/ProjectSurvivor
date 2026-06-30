/****************************************************************************
 * 2026.6 鲁童昕的MacBook Pro
 ****************************************************************************/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Linq;

namespace QFramework.Example
{
	public partial class TreasureChestPanel : UIElement,IController
	{
		private void Awake()
		{
			BtnSure.onClick.AddListener(() =>
			{
				Time.timeScale = 1.0f;
				this.Hide();
			});
		}
        void OnEnable()
        {
            ExpUpgradeSystem expUpgradeSystem = this.GetSystem<ExpUpgradeSystem>();
            List<ExpUpgradeItem> expUpgradeItems = expUpgradeSystem.Items.Where(item =>
			{
				if(item.CurrentLevel.Value >= 0 && !item.UpgradeFinish)
				{
					return true;
				}
				else
				{
					return false;
				}
			}).ToList();
			if (expUpgradeItems.Any())
			{
				var item = expUpgradeItems.GetRandomItem();
				Content.text = "<b>" + item.Key + "</b>\n" + item.Description;
				item.Upgrade();
			}
			else
			{
				if(Global.HP.Value < Global.MaxHP.Value)
				{
					if(Random.Range(0,1.0f) < 0.2f)
					{
						Content.text = "recover 1HP";
						AudioKit.PlaySound("HP");
						Global.HP.Value++;
						return;
					}
				}
				Content.text = "Add 50 Coin";
				Global.Coin.Value += 50;
				
			}
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