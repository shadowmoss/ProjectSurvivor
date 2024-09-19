using ProjectSurvivor;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class Global
{
    /// <summary>
    /// 经验值
    /// </summary>
    public static BindableProperty<int> Exp = new BindableProperty<int>(0);
    /// <summary>
    /// 金币
    /// </summary>
    public static BindableProperty<int> Coin = new BindableProperty<int>(0);

    /// <summary>
    /// 等级
    /// </summary>
    public static BindableProperty<int> Level = new BindableProperty<int>(1);

    /// <summary>
    /// 基础攻击能力的攻击力数值
    /// </summary>
    public static BindableProperty<float> SimpleAbilityDamage = new BindableProperty<float>(1);

    // 当前游戏时间
    public static BindableProperty<float> CurrentSeconds = new BindableProperty<float>(0);

    // 当前SimpleAbility攻击频率
    public static BindableProperty<float> SimpleAbilityDuration = new BindableProperty<float>(1.5f);


    // 经验掉落概率
    public static BindableProperty<float> ExpPercent = new BindableProperty<float>(0.4f);
    // 金币掉落概率
    public static BindableProperty<float> CoinPercent = new BindableProperty<float>(0.1f);


    // 数值初始化
    [RuntimeInitializeOnLoadMethod]
    public static void AutoInit() {
        // 金币
        Global.Coin.Value = PlayerPrefs.GetInt("coin", 0);

        

        // 经验值掉落概率初始化
        Global.ExpPercent.Value = PlayerPrefs.GetFloat("ExpPercent", 0.3f);
        Global.CoinPercent.Value = PlayerPrefs.GetFloat("CoinPercent", 0.1f);

        // 当金币值变化时存储
        Global.Coin.Register(coin =>
        {
            PlayerPrefs.SetInt("coin", coin);
        });
        // 当经验值掉落概率时存储
        Global.ExpPercent.Register(ExpPercent =>
        {
            PlayerPrefs.SetFloat("ExpPercent", ExpPercent);
        });
        // 当金币掉落概率变化时存储
        Global.CoinPercent.Register(CoinPercent =>
        {
            PlayerPrefs.SetFloat("CoinPercent", CoinPercent);
        });
    }
    public static void ResetData() {
        Exp.Value = 0;
        Level.Value = 1;
        CurrentSeconds.Value = 0;
        SimpleAbilityDamage.Value = 0;
        EnemyGenerator.EnemyCount.Value = 0;
        SimpleAbilityDuration.Value = 1.5f;
    }

    public static int ExpToNextLevel() {
        return Global.Level.Value * 5 ;
    }

    public static void GeneratePowerUp(GameObject gameobject) {

        
        float expPercent = Random.Range(0,1f);

        // 经验值生成判断
        if (expPercent < Global.ExpPercent.Value)
        {
            PowerUpManager.Default.Exp.Instantiate()
                .Position(gameobject.Position())
                .Show();
        }
        
        // 金币生成
        float coinPercent = Random.Range(0, 1f);
        if (coinPercent < Global.CoinPercent.Value) {
            PowerUpManager.Default.Coin.Instantiate().Position(gameobject.Position()).Show();
        }
    }
}
