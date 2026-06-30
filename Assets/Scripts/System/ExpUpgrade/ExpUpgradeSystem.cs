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
            Add(new ExpUpgradeItem(true)
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
                            Global.SimpleSwordUnlocked.Value = true;
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

                Add(new ExpUpgradeItem(true)
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
                            Global.SimpleKnifeUnlocked.Value = true;
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
                Add(new ExpUpgradeItem(true)
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
                                Global.RotateSwordUnlocked.Value = true;
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
                    Add(new ExpUpgradeItem(true)
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
                                    Global.BasketBallUnlocked.Value = true;
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
                    Add(new ExpUpgradeItem(false)
                    .WithKey("simple_bomb")
                    .WithMaxLevel(10)
                    .WithDescription(lv =>
                    {
                        return lv switch
                        {
                        1=>$"Bomb level {lv}:BombDrop By Enemy",
                        2=>$"Bomb level {lv}:drop percentage +5% damage +5",
                        3=>$"Bomb level {lv}:drop percentage +5% damage +5",
                        4=>$"Bomb level {lv}:drop percentage +5% damage +5",
                        5=>$"Bomb level {lv}:drop percentage +5% damage +5",
                        6=>$"Bomb level {lv}:drop percentage +5% damage +5",
                        7=>$"Bomb level {lv}:drop percentage +5% damage +5",
                        8=>$"Bomb level {lv}:drop percentage +5% damage +5",
                        9=>$"Bomb level {lv}:drop percentage +5% damage +5",
                        10=>$"Bomb level {lv}:drop percentage +10% damage +5",
                        _=>null,
                        };
                    }).OnUpgrade((_, level) =>
                    {
                        switch (level)
                        {
                            case 1:
                                Global.BombUnlocked.Value = true;
                                break;
                            case 2:
                                Global.BombDamage.Value += 5;
                                Global.BombPercent.Value += 0.05f;
                            break;
                            case 3:
                                Global.BombDamage.Value += 5;
                                Global.BombPercent.Value += 0.05f;
                                break;
                            case 4:
                                Global.BombDamage.Value += 5;
                                Global.BombPercent.Value += 0.05f;
                                break;
                            case 5:
                                Global.BombDamage.Value += 5;
                                Global.BombPercent.Value += 0.05f;
                                break;
                            case 6:
                                Global.BombDamage.Value += 5;
                                Global.BombPercent.Value += 0.05f;
                                break;
                            case 7:
                                Global.BombDamage.Value += 5;
                                Global.BombPercent.Value += 0.05f;
                                break;
                            case 8:
                                Global.BombDamage.Value += 5;
                                Global.BombPercent.Value += 0.05f;
                                break;
                            case 9:
                                Global.BombDamage.Value += 5;
                                Global.BombPercent.Value += 0.05f;
                                break;
                            case 10:
                                Global.BombDamage.Value += 5;
                                Global.BombPercent.Value += 0.1f;
                                break;
                            default:
                                break;
                        }
                    }));
                    Add(new ExpUpgradeItem(false)
                        .WithKey("simple_critical")
                        .WithMaxLevel(5)
                        .WithDescription(lv =>
                        {
                            return lv switch
                            {
                                1 => $"critical Lv{lv}:\nper time damage 15%",
                                2 => $"critical Lv{lv}:\nper time damage 28%",
                                3 => $"critical Lv{lv}:\nper time damage 43%",
                                4 => $"critical Lv{lv}:\nper time damage 50%",
                                5 => $"critical Lv{lv}:\nper time damage 80%",
                                _ => null
                            };
                        })
                        .OnUpgrade((_, lv) =>
                        {
                            switch (lv)
                            {
                                case 1:
                                    Global.CriticalRate.Value = 0.15f;
                                    break;
                                case 2:
                                    Global.CriticalRate.Value = 0.28f;
                                    break;
                                case 3:
                                    Global.CriticalRate.Value = 0.43f;
                                    break;
                                case 4:
                                    Global.CriticalRate.Value = 0.5f;
                                    break;
                                case 5:
                                    Global.CriticalRate.Value = 0.8f;
                                    break;
                            }
                        })
                    );
                    Add(new ExpUpgradeItem(false)
                        .WithKey("damage_rate")
                        .WithDescription(lv =>
                        {
                            return lv switch
                            {
                                1 => $"Damage Rate Lv{lv}:\n increase external 20% damage",
                                2 => $"Damage Rate Lv{lv}:\n increase external 40% damage",
                                3 => $"Damage Rate Lv{lv}:\n increase external 60% damage",
                                4 => $"Damage Rate Lv{lv}:\n increase external 80% damage",
                                5 => $"Damage Rate Lv{lv}:\n increase external 100% damage",
                                _ => null 
                            };
                        })
                        .OnUpgrade((_, lv) =>
                        {
                            switch (lv)
                            {
                                case 1:
                                    Global.DamageRate.Value = 1.2f;
                                    break;
                                case 2:
                                    Global.DamageRate.Value = 1.4f;
                                    break;
                                case 3:
                                    Global.DamageRate.Value = 1.6f;
                                    break;
                                case 4:
                                    Global.DamageRate.Value = 1.8f;
                                    break;
                                case 5:
                                    Global.DamageRate.Value = 2f;
                                    break;
                            }
                        }));
                    Add(new ExpUpgradeItem(false)
                                .WithKey("simple_fly_count")
                                .WithMaxLevel(3)
                                .WithDescription(lv =>
                                {
                                    return lv switch
                                    {
                                        1 => $"fly item Lv{lv}:\n add one fly item",
                                        2 => $"fly item Lv{lv}:\n add two fly item",
                                        3 => $"fly item Lv{lv}\n add three fly item",
                                        _ => null  
                                    };
                                })
                                .OnUpgrade((_, lv) =>
                                {
                                    switch (lv)
                                    {
                                        case 1:
                                            Global.AdditionalFlyThingCount.Value++;
                                            break;
                                        case 2:
                                            Global.AdditionalFlyThingCount.Value++;
                                            break;
                                        case 3:
                                            Global.AdditionalFlyThingCount.Value++;
                                            break;
                                    }
                                })
                    );
                    Add(new ExpUpgradeItem(false)
                        .WithKey("movement_speed_rate")
                        .WithMaxLevel(5)
                        .WithDescription(lv =>
                        {
                            return lv switch
                            {
                                1 => $"move speed Lv{lv}:\nadd 25% move speed rate",
                                2 => $"move speed Lv{lv}:\nadd 50% move speed rate",
                                3 => $"move speed Lv{lv}:\nadd 75% move speed rate",
                                4 => $"move speed Lv{lv}:\nadd 100% move speed rate",
                                5 => $"move speed Lv{lv}:\nadd 150% move speed rate",
                                _ => null
                            };
                        })
                        .OnUpgrade((_, lv) =>
                        {
                            switch (lv)
                            {
                                case 1:
                                    Global.MovementSpeedRate.Value = 1.25f;
                                    break;
                                case 2:
                                    Global.MovementSpeedRate.Value = 1.5f;
                                    break;
                                case 3:
                                    Global.MovementSpeedRate.Value = 1.75f;
                                    break;
                                case 4:
                                    Global.MovementSpeedRate.Value = 2f;
                                    break;
                                case 5:
                                    Global.MovementSpeedRate.Value = 2.5f;
                                    break;
                            }
                        }));
                        Add(new ExpUpgradeItem(false)
                            .WithKey("simple_collectable_area")
                            .WithMaxLevel(3)
                            .WithDescription(lv =>
                            {
                                return lv switch
                                {
                                  1 => $"pick area Lv{lv}:\nadd 100% range",
                                  2 => $"pick area Lv{lv}:\nadd 200% range",
                                  3 => $"pick area Lv{lv}:\nadd 300* range",
                                  _ => null  
                                };
                            })
                            .OnUpgrade((_, lv) =>
                            {
                                switch (lv)
                                {
                                    case 1:
                                        Global.CollectableArea.Value = 2f;
                                        break;
                                    case 2:
                                        Global.CollectableArea.Value = 3f;
                                        break;
                                    case 3:
                                        Global.CollectableArea.Value = 4f;
                                        break;
                                }
                            })
                        );
                        Add(new ExpUpgradeItem(false)
                            .WithKey("simple_exp")
                            .WithDescription(lv =>
                            {
                                return lv switch
                                {
                                  1 => $"exp Lv{lv}:\nadd 5% drop rate",
                                  2 => $"exp Lv{lv}:\nadd 8% drop rate",
                                  3 => $"exp Lv{lv}:\nadd 12% drop rate",
                                  4 => $"exp Lv{lv}:\nadd 17% drop rate",
                                  5 => $"exp Lv{lv}:\nadd 25% drop rate",
                                  _ => null  
                                };
                            })
                            .OnUpgrade((_, lv) =>
                            {
                                switch (lv)
                                {
                                    case 1:
                                        Global.AdditionalExpPercent.Value = 0.05f;
                                        break;
                                    case 2:
                                        Global.AdditionalExpPercent.Value = 0.08f;
                                        break;
                                    case 3:
                                        Global.AdditionalExpPercent.Value = 0.12f;
                                        break;
                                    case 4:
                                        Global.AdditionalExpPercent.Value = 0.17f;
                                        break;
                                    case 5:
                                        Global.AdditionalExpPercent.Value = 0.25f;
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
            var items = Items.Where(item=>!item.UpgradeFinish).ToList();
            if(items.Count >= 4)
            {
                items.GetAndRemoveRandomItem().Visible.Value = true;
                items.GetAndRemoveRandomItem().Visible.Value = true;
                items.GetAndRemoveRandomItem().Visible.Value = true;
                items.GetAndRemoveRandomItem().Visible.Value = true;
            }
            else
            {
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
}