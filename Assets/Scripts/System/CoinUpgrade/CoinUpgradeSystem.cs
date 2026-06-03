using System.Collections;
using System.Collections.Generic;
using QFramework;
using QFramework.Example;
using UnityEngine;

public class CoinUpgradeSystem : AbstractSystem
{
    public static EasyEvent OnCoinUpgradeSystemChanged = new EasyEvent();
    public List<CoinUpgradeItem> Items {get;} = new List<CoinUpgradeItem>();
    public CoinUpgradeItem Add(CoinUpgradeItem item)
    {
        Items.Add(item);
        return item;
    }
    protected override void OnInit()
    {
        CoinUpgradeItem coinUpgradeLevelv1 = Add(
            new CoinUpgradeItem()
                .WithKey("coin_percent_upgrade")
                .WithDescription("CoinDropPercent")
                .WithPrice(5)
                .OnUpgrade((self) =>
                {
                    Global.CoinPercent.Value += 0.1f;
                    Global.Coin.Value -= self.Price;
                    PlayerPrefs.SetFloat(nameof(Coin),Global.CoinPercent.Value);
                })
        );
        CoinUpgradeItem coinUpgradeLevelv2 = Add(
        new CoinUpgradeItem()
            .WithKey("coin_percent_upgrade")
            .WithDescription("CoinDropPercent")
            .WithPrice(7)
            .OnCondition((self) =>coinUpgradeLevelv1.UpgradeFinish)
            .OnUpgrade((self) =>
            {
                Global.CoinPercent.Value += 0.1f;
                Global.Coin.Value -= self.Price;
                PlayerPrefs.SetFloat(nameof(Coin),Global.CoinPercent.Value);
            })
        );
        CoinUpgradeItem coinUpgradeLevelv3 = Add(
        new CoinUpgradeItem()
            .WithKey("coin_percent_upgrade")
            .WithDescription("CoinDropPercent")
            .WithPrice(10)
            .OnCondition((self)=>coinUpgradeLevelv2.UpgradeFinish)
            .OnUpgrade((self) =>
            {
                Global.CoinPercent.Value += 0.1f;
                Global.Coin.Value -= self.Price;
                PlayerPrefs.SetFloat(nameof(Coin),Global.CoinPercent.Value);
            })
        );
        Items.Add(
        new CoinUpgradeItem()
            .WithKey("exp_percent_upgrade")
            .WithDescription("ExpDropPercent")
            .WithPrice(5)
            .OnUpgrade((self) =>
            {
                Global.ExpPercent.Value += 0.1f;
                Global.Coin.Value -= self.Price;
                PlayerPrefs.SetFloat(nameof(Coin),Global.CoinPercent.Value);
            })
        );

        Items.Add(
        new CoinUpgradeItem()
            .WithKey("coin_max_hp_upgrade")
            .WithDescription("MaxHpUpgrade")
            .WithPrice(30)
            .OnUpgrade((self) =>
            {
                Global.MaxHP.Value += 1;
                Global.Coin.Value -= self.Price;
                PlayerPrefs.SetFloat(nameof(Coin),Global.CoinPercent.Value);
            })
        );
    }
    public void Say()
    {
        Debug.Log("Hello CoinUpgradeSystem");
    }
}
