using System.Collections;
using System.Collections.Generic;
using QFramework;
using QFramework.Example;
using UnityEngine;

public class CoinUpgradeSystem : AbstractSystem,ICanSave
{
    public static EasyEvent OnCoinUpgradeSystemChanged = new EasyEvent();
    public List<CoinUpgradeItem> Items = new List<CoinUpgradeItem>();
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
            .WithDescription("CoinDropPercent_Lv2")
            .WithPrice(7)
            .OnCondition((self) =>coinUpgradeLevelv1.UpgradeFinish)
            .OnUpgrade((self) =>
            {
                Global.CoinPercent.Value += 0.1f;
                Global.Coin.Value -= self.Price;
                PlayerPrefs.SetFloat(nameof(Coin),Global.CoinPercent.Value);
            })
        );
        coinUpgradeLevelv1.OnChanged.Register(() =>
        {
            coinUpgradeLevelv2.OnChanged.Trigger();
        });
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
        coinUpgradeLevelv2.OnChanged.Register(() =>
        {
            coinUpgradeLevelv3.OnChanged.Trigger();
        });
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
        Load();
        OnCoinUpgradeSystemChanged.Register(() =>
        {
            Save();  
        });
    }
    public void Say()
    {
        Debug.Log("Hello CoinUpgradeSystem");
    }

    public void Save()
    {
        var saveSystem = this.GetSystem<SaveSystem>();
        foreach(CoinUpgradeItem coinUpgradeItem in Items)
        {
            saveSystem.SaveBool(coinUpgradeItem.Key,coinUpgradeItem.UpgradeFinish);
        }
    }

    public void Load()
    {
        var saveSystem = this.GetSystem<SaveSystem>();
        foreach(CoinUpgradeItem coinUpgradeItem in Items)
        {
            coinUpgradeItem.UpgradeFinish = saveSystem.LoadBool(coinUpgradeItem.Key,false);
        }
    }
}
