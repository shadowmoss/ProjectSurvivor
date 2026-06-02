using System.Collections;
using System.Collections.Generic;
using QFramework;
using QFramework.Example;
using UnityEngine;

public class CoinUpgradeSystem : AbstractSystem
{
    public List<CoinUpgradeItem> Items {get;} = new List<CoinUpgradeItem>();
    protected override void OnInit()
    {
        Items.Add(
        new CoinUpgradeItem()
            .WithKey("coin_percent_upgrade")
            .WithDescription("CoinDropPercent")
            .WithPrice(5)
            .OnUpgrade((self) =>
            {
                Global.CoinPercent.Value += 0.1f;
                Global.Coin.Value -= self.Price;
            })
        );
    }
    public void Say()
    {
        Debug.Log("Hello CoinUpgradeSystem");
    }
}
