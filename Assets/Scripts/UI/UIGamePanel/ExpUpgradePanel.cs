/****************************************************************************
 * 2026.6 鲁童昕的MacBook Pro
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Linq;
using UnityEngine.Rendering;

namespace QFramework.Example
{
	public partial class ExpUpgradePanel : UIElement,IController
	{
		private void Awake()
		{
			// UpgrateRoot.Hide();
			ExpUpgradeSystem expUpgradeSystem = this.GetSystem<ExpUpgradeSystem>();
			foreach(ExpUpgradeItem item in expUpgradeSystem.Items)
			{
				Debug.Log(UpgradeItemTemplate);
				Debug.Log(UpgrateRoot);
				UpgradeItemTemplate.InstantiateWithParent(UpgrateRoot)
				.Self(self =>
				{
					Button selfCache = self;
					Text btnDescription = self.GetComponentInChildren<Text>();
					ExpUpgradeItem itemCache = item;
					btnDescription.text = itemCache.Description +$"Exp:{itemCache.Price}" ;
					selfCache.onClick.AddListener(() =>
					{
						Time.timeScale = 1.0f;
						itemCache.Upgrade();
						AudioKit.PlaySound("AbilityLevelUp");
						this.Hide();
					});
					selfCache.Hide();
					itemCache.Visible.RegisterWithInitValue(visible =>
					{
						if (visible)
						{
							selfCache.Show();							
						}
						else
						{
							selfCache.Hide();
						}
					});
					itemCache.OnChanged.Register(() =>
					{
						btnDescription.text = itemCache.Description + $"Exp:{itemCache.Price}";
					});
				// if (itemCache.ConditionCheck())
				// {
				// 	selfCache.Show();		
				// }
				// else
				// {
				// 	selfCache.Hide();
				// }
				// 	itemCache.OnChanged.Register(() =>
				// 	{
				// 		if (itemCache.ConditionCheck())
				// 		{
				// 			selfCache.Show();
				// 		}
				// 		else
				// 		{
				// 			selfCache.Hide();
				// 		}
				// 	});
				});
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