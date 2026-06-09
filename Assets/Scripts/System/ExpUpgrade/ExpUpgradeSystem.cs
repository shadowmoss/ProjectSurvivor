using System.Collections.Generic;
using System.Linq;

namespace QFramework.Example
{
    class ExpUpgradeSystem : AbstractSystem
    {
        public List<ExpUpgradeItem> Items = new List<ExpUpgradeItem>();
        public ExpUpgradeItem Add(ExpUpgradeItem item)
        {
            Items.Add(item);
            return item;   
        }

        protected override void OnInit()
        {

            ResetData();
            Global.Level.Register(level =>
            {
                Roll(); 
            });
        }
        public void ResetData()
        {
            Items.Clear();
            ExpUpgradeItem expUpgradeDamage_v1 = Add(
                new ExpUpgradeItem()
                    .WithKey("exp_level_upgrade")
                    .WithDescription((currentLevel) =>
                    {
                        return $"simple_ability_damage_{currentLevel}";
                    })
                    .WithMaxLevel(10)
                    .OnUpgrade((self,currentLevel) =>
                    {
                        if(currentLevel == 1)
                        {
                            
                        }
                        Global.SimpleAbilityDamage.Value *= 1.5f;
                    })
            );
            ExpUpgradeItem expUpgradeFrequence_v1 = Add(
                new ExpUpgradeItem()
                    .WithKey("exp_level_upgrade")
                    .WithDescription((currentLevel) =>
                    {
                        return $"simple_ability_duration_{currentLevel}";
                    })
                    .WithMaxLevel(10)
                    .OnUpgrade((self,currentLevel) =>
                    {
                        if (currentLevel==1)
                        {
                            
                        }
                        Global.SimpleAbilityDuration.Value *= 0.8f;
                    })
            );
        }
        // RandUpgrade
        public void Roll()
        {
            foreach(ExpUpgradeItem expUpgradeItem in Items)
            {
                expUpgradeItem.Visible.Value = false;
            }
            ExpUpgradeItem item = Items.Where(item=>!item.UpgradeFinish).ToList().GetRandomItem();
            if(item == null)
            {

            }
            else
            {
                item.Visible.Value = true;
            }
        }
    }
}