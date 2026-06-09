using System;

namespace QFramework.Example
{
    class ExpUpgradeItem{
    public EasyEvent OnChanged = new EasyEvent();
            public bool UpgradeFinish {get; set;} = false;
            public string Key{get;private set;}
            public string Description{get;private set;}
            public int Price {get;private set;}
            public int MaxLevel {get;private set;}
            public int CurrentLevel{get;private set;}

            public BindableProperty<bool> Visible = new BindableProperty<bool>();
            public void Upgrade()
            {
                CurrentLevel++;
                mOnUpgrade?.Invoke(this,CurrentLevel);
                if (CurrentLevel > MaxLevel)
                {
                    UpgradeFinish = true;
                }
                Description = mDescriptionFactory(CurrentLevel);
                OnChanged.Trigger();
                // CoinUpgradeSystem.OnCoinUpgradeSystemChanged.Trigger();
            }
            public Action<ExpUpgradeItem,int> mOnUpgrade { get; private set; }
            public Func<ExpUpgradeItem,bool> mOnCondition {get;private set;}
            public Func<int,string> mDescriptionFactory {get;private set;}

            public ExpUpgradeItem WithKey(string key)
            {
                Key = key;
                return this;
            }

            public ExpUpgradeItem WithDescription(Func<int,string> descriptionFactory)
            {
                mDescriptionFactory = descriptionFactory;
                Description = mDescriptionFactory(CurrentLevel);
                return this;
            }
            public ExpUpgradeItem OnUpgrade(Action<ExpUpgradeItem,int> onUpgrade)
            {
                mOnUpgrade = onUpgrade;
                return this;
            }
            public ExpUpgradeItem WithPrice(int price)
            {
                Price = price;
                return this;
            }
            public ExpUpgradeItem OnCondition(Func<ExpUpgradeItem,bool> onCondition)
            {
                mOnCondition = onCondition;
                return this;
            }
            public ExpUpgradeItem WithMaxLevel(int maxLevel)
            {
                MaxLevel = maxLevel;
                return this;
            }
        }
}