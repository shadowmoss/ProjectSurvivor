using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global
{
    /// <summary>
    /// ����ֵ
    /// </summary>
    public static BindableProperty<int> Exp = new BindableProperty<int>(0);

    /// <summary>
    /// �ȼ�
    /// </summary>
    public static BindableProperty<int> Level = new BindableProperty<int>(1);

    /// <summary>
    /// �������������Ĺ�������ֵ
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
