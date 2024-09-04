using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global
{
    /// <summary>
    /// 经验值
    /// </summary>
    public static BindableProperty<int> Exp = new BindableProperty<int>(0);

    /// <summary>
    /// 等级
    /// </summary>
    public static BindableProperty<int> Level = new BindableProperty<int>(1);

    /// <summary>
    /// 基础攻击能力的攻击力数值
    /// </summary>
    public static BindableProperty<float> SimpleAbilityDamage = new BindableProperty<float>(1);

    public static BindableProperty<float> CurrentSeconds = new BindableProperty<float>(0);

    public static void ResetData() {
        Exp.Value = 0;
        Level.Value = 1;
        CurrentSeconds.Value = 0;
        SimpleAbilityDamage.Value = 0;
    }
}
