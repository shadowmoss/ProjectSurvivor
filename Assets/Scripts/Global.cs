using System.Collections;
using System.Collections.Generic;
using QFramework;
using QFramework.Example;
using UnityEngine;

public class Global
{
    public static BindableProperty<int> Exp = new BindableProperty<int>(0);
    public static BindableProperty<int> Level = new BindableProperty<int>(1);
    public static BindableProperty<float> SimpleAbilityDamage = new BindableProperty<float>(1);
    public static BindableProperty<float> CurrentSeconds = new BindableProperty<float>(0);
    public static BindableProperty<float> SimpleAbilityDuration = new BindableProperty<float>(1.5f);

    public static void ResetData()
    {
        Exp.Value = 0;
        Level.Value = 1;
        CurrentSeconds.Value = 0;
        SimpleAbilityDamage.Value = 1;
        SimpleAbilityDuration.Value = 1.5f;
        EnemyGenerator.EnemyCount.Value = 0;
    }
    public static int ExpToNextLevel()
    {
        return Level.Value * 5;
    }
}
