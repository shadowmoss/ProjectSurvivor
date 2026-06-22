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
            Add(new ExpUpgradeItem()
                .WithKey("simple_sword")
                .WithDescription(lv =>
                {
                    return lv switch
                    {
                      1=>$"Sword level {lv}:attack near by enemy",
                      2=>$"Sword level {lv}:damage+3 count+2",
                      3=>$"Sword level {lv}:damage+2 duration-0.25s",
                      4=>$"Sword level {lv}:damage+2 duration-0.25s",
                      5=>$"Sword level {lv}:damage+3 count+2",
                      6=>$"Sword level {lv}:range+1 duration-0.25s",
                      7=>$"Sword level {lv}:damage+3 count+2",
                      8=>$"Sword level {lv}:damage+2 range+1",
                      9=>$"Sword level {lv}:damage+3 duration-0.25s",
                      10=>$"Sword level {lv}:damage+3 count+2",
                      _=>null,
                    };
                })
                .WithMaxLevel(10)
                .OnUpgrade((_, level) =>
                {
                    switch (level)
                    {
                        case 1:
                            break;
                        case 2:
                            Global.SimpleAbilityDamage.Value += 3;
                            Global.SimpleSwordCount.Value +=2;
                        break;
                        case 3:
                            Global.SimpleAbilityDamage.Value += 2;
                            Global.SimpleAbilityDuration.Value -= 0.25f;
                            break;
                        case 4:
                            Global.SimpleAbilityDamage.Value += 2;
                            Global.SimpleAbilityDuration.Value -= 0.25f;
                            break;
                        case 5:
                            Global.SimpleAbilityDamage.Value += 3;
                            Global.SimpleSwordCount.Value += 2;
                            break;
                        case 6:
                            Global.SimpleSwordRange.Value++;
                            Global.SimpleAbilityDuration.Value -= 0.25f;
                            break;
                        case 7:
                            Global.SimpleAbilityDamage.Value += 3;
                            Global.SimpleSwordCount.Value += 2;
                            break;
                        case 8:
                            Global.SimpleAbilityDamage.Value += 2;
                            Global.SimpleSwordRange.Value++;
                            break;
                        case 9:
                            Global.SimpleAbilityDamage.Value += 3;
                            Global.SimpleAbilityDuration.Value -= 0.25f;
                            break;
                        case 10:
                            Global.SimpleAbilityDamage.Value +=3;
                            Global.SimpleSwordCount.Value+=2;
                            break;
                        default:
                            break;
                    }
                })
                );

                Add(new ExpUpgradeItem()
                    .WithKey("simple_knife")
                    .WithMaxLevel(10)
                    .WithDescription(lv =>
                {
                    return lv switch
                    {
                      1=>$"Knife level {lv}:attack near by enemy",
                      2=>$"Knfie level {lv}:damage+3 count+2",
                      3=>$"Knfie level {lv}:duration-0.1s damage+2 count+1",
                      4=>$"Knfie level {lv}:duration-0.1s pierce+1 count+1",
                      5=>$"Knfie level {lv}:damage+3 count+1",
                      6=>$"Knfie level {lv}:duration-0.1s count+1",
                      7=>$"Knfie level {lv}:duration-0.1s pierce+1 count+1",
                      8=>$"Knfie level {lv}:damage+3 count+1",
                      9=>$"Knfie level {lv}:duration-0.1s count+1",
                      10=>$"Knfie level {lv}:damage+3 count+1",
                      _=>null,
                    };
                }).OnUpgrade((_, level) =>
                {
                    switch (level)
                    {
                        case 1:
                            break;
                        case 2:
                            Global.SimpleKnifeDamage.Value += 3;
                            Global.SimpleKnifeCount.Value +=2;
                        break;
                        case 3:
                            Global.SimpleKnifeCount.Value+=1;
                            Global.SimpleKnifeDamage.Value += 2;
                            Global.SimpleKnifeDuration.Value -= 0.1f;
                            break;
                        case 4:
                            Global.SimpleKnifeCount.Value += 1;
                            Global.SimpleKnifeAttackCount.Value++;
                            Global.SimpleAbilityDuration.Value -= 0.1f;
                            break;
                        case 5:
                            Global.SimpleKnifeDamage.Value += 3;
                            Global.SimpleKnifeCount.Value += 1;
                            break;
                        case 6:
                            Global.SimpleKnifeCount.Value++;
                            Global.SimpleAbilityDuration.Value -= 0.1f;
                            break;
                        case 7:
                            Global.SimpleKnifeDuration.Value -= 0.1f;
                            Global.SimpleKnifeCount.Value += 1;
                            Global.SimpleKnifeAttackCount.Value++;
                            break;
                        case 8:
                            Global.SimpleKnifeDamage.Value += 3;
                            Global.SimpleKnifeCount.Value++;
                            break;
                        case 9:
                            Global.SimpleKnifeDuration.Value -= 0.1f;
                            Global.SimpleKnifeCount.Value++;
                            break;
                        case 10:
                            Global.SimpleKnifeDamage.Value +=3;
                            Global.SimpleKnifeCount.Value++;
                            break;
                        default:
                            break;
                    }
                }));
                Add(new ExpUpgradeItem()
                    .WithKey("rotate_sword")
                    .WithMaxLevel(10)
                    .WithDescription(lv =>
                    {
                        return lv switch
                        {
                            1 => $"gurad swordLv{lv}: \naround player gurad sword",
                            2 => $"gurad sowrdLv{lv}: \ncount +1,attack +1",
                            3 => $"gurad sowrdLv{lv}: \nattack +2,speed +25%",
                            4 => $"gurad sowrdLv{lv}: \nspeed +50%",
                            5 => $"gurad sowrdLv{lv}: \ncount +1,damage +1",
                            6 => $"gurad swordLv{lv}: \ndamage +2,speed +25%",
                            7 => $"gurad swordLv{lv}: \ncount +1 damage +1",
                            8 => $"gurad swordLv{lv}: \ndamage +2 speed +25%",
                            9 => $"gurad swordLv{lv}: \ncount +1 damage +1",
                            10 => $"gurad swordLv{lv}: \ndamage +2 speed +25%",
                            _ => null
                        };
                    })
                    .OnUpgrade((_, level) =>
                    {
                        switch (level)
                        {
                            case 1:
                                // Global.RotateSwordUnlocked.Value = true;
                                break;
                            case 2:
                                Global.RotateSwordCount.Value++;
                                Global.RotateSwordDamage.Value++;
                                break;
                            case 3:
                                Global.RotateSwordDamage.Value += 2;
                                Global.RotateSwordSpeed.Value *= 1.25f;
                                break;
                            case 4:
                                Global.RotateSwordSpeed.Value *= 1.5f;
                                break;
                            case 5:
                                Global.RotateSwordCount.Value++;
                                Global.RotateSwordDamage.Value++;
                                break;
                            case 6:
                                Global.RotateSwordDamage.Value+=2;
                                Global.RotateSwordSpeed.Value*=1.25f;
                                break;
                            case 7:
                                Global.RotateSwordCount.Value++;
                                Global.RotateSwordDamage.Value++;
                                break;
                            case 8:
                                Global.RotateSwordDamage.Value += 2;
                                Global.RotateSwordSpeed.Value *= 1.25f;
                                break;
                            case 9:
                                Global.RotateSwordCount.Value++;
                                Global.RotateSwordDamage.Value++;
                                break;
                            case 10:
                                Global.RotateSwordDamage.Value+=2;
                                Global.RotateSwordSpeed.Value *= 1.25f;
                                break;                                
                        }
                    })
                    );
                    Add(new ExpUpgradeItem()
                        .WithKey("basket_ball")
                        .WithMaxLevel(10)
                        .WithDescription(lv =>
                        {
                            return lv switch
                            {
                                1 => $"basketball Lv{lv}: \na basketball bounce in screen",
                                2 => $"basketball Lv{lv}: \ndamage +3",
                                3 => $"basketball Lv{lv}: \ncount +1",
                                4 => $"basketball Lv{lv}: \ndamage +3",
                                5 => $"basketball Lv{lv}: \ncount +1",
                                6 => $"basketball Lv{lv}: \ndamage +3",
                                7 => $"basketball Lv{lv}: \nspeed +20%",
                                8 => $"basketball Lv{lv}: \ndamage +3",
                                9 => $"basketball Lv{lv}: \nspeed +20%",
                                10 => $"basketball Lv{lv: \ncount +1}",
                                _ => null
                            };
                        })
                        .OnUpgrade((_, level) =>
                        {
                            switch (level)
                            {
                                case 1:
                                    break;
                                case 2:
                                    Global.BasketBallDamage.Value += 3;
                                    break;
                                case 3:
                                    Global.BasketBallCount.Value += 3;
                                    break;
                                case 4:
                                    Global.BasketBallDamage.Value += 3;
                                    break;
                                case 5:
                                    Global.BasketBallCount.Value += 1;
                                    break;
                                case 6:
                                    Global.BasketBallDamage.Value += 3;
                                    break;
                                case 7:
                                    Global.BasketBallSpeed.Value *= 1.2f;
                                    break;
                                case 8:
                                    Global.BasketBallDamage.Value +=3;
                                    break;
                                case 9:
                                    Global.BasketBallSpeed.Value *= 1.2f;
                                    break;
                                case 10:
                                    Global.BasketBallCount.Value +=1;
                                    break;
                                default:
                                    break;
                            }
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
            var items = Items.Where(item=>!item.UpgradeFinish).Take(4);
            foreach (var item in items)
            {
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
}