using System;

namespace QFramework.Example
{
    public class CoinUpgradeItem
    {
        public bool UpgradeFinish {get;private set;} = false;
        public string Key{get;private set;}
        public string Description{get;private set;}
        public int Price {get;private set;}
        public void Upgrade()
        {
            mOnUpgrade?.Invoke(this);
            UpgradeFinish = true;
            CoinUpgradeSystem.OnCoinUpgradeSystemChanged.Trigger();
        }
        public bool ConditionCheck()
        {
            if(mOnCondition!=null)
            {
                return !UpgradeFinish && mOnCondition(this);
            }
            else
            {
                return !UpgradeFinish;
            }
        }
        public Action<CoinUpgradeItem> mOnUpgrade { get; private set; }
        public Func<CoinUpgradeItem,bool> mOnCondition {get;private set;}

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
        public CoinUpgradeItem OnCondition(Func<CoinUpgradeItem,bool> onCondition)
        {
            mOnCondition = onCondition;
            return this;
        }
    }
}