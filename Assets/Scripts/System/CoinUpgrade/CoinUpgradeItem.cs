using System;

namespace QFramework.Example
{
    public class CoinUpgradeItem
    {
        public string Key{get;private set;}
        public string Description{get;private set;}
        public int Price {get;private set;}
        public void Upgrade()
        {
            mOnUpgrade?.Invoke(this);
        }
        public Action<CoinUpgradeItem> mOnUpgrade { get; private set; }

        public CoinUpgradeItem WithKey(string key)
        {
            Key = key;
            return this;
        }

        public CoinUpgradeItem WithDescription(string description)
        {
            Description = description;
            return this;
        }
        public CoinUpgradeItem OnUpgrade(Action<CoinUpgradeItem> onUpgrade)
        {
            mOnUpgrade = onUpgrade;
            return this;
        }
        public CoinUpgradeItem WithPrice(int price)
        {
            Price = price;
            return this;
        }
    }
}