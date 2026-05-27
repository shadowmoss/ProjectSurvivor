using System.Collections;
using System.Collections.Generic;
using QFramework;
using QFramework.Example;
using UnityEngine;

public class Global : Architecture<Global>
{
    public static BindableProperty<int> Coin = new BindableProperty<int>(0);
    public static BindableProperty<int> Exp = new BindableProperty<int>(0);
    public static BindableProperty<int> Level = new BindableProperty<int>(1);
    public static BindableProperty<float> SimpleAbilityDamage = new BindableProperty<float>(1);
    public static BindableProperty<float> CurrentSeconds = new BindableProperty<float>(0);
    public static BindableProperty<float> SimpleAbilityDuration = new BindableProperty<float>(1.5f);

    public static BindableProperty<float> ExpPercent = new BindableProperty<float>(0.3f);
    public static BindableProperty<float> CoinPercent = new BindableProperty<float>(0.05f);


    [RuntimeInitializeOnLoadMethod]
    public static void AutoInit()
    {
        ResKit.Init();
        UIKit.Root.SetResolution(1920,1080,1);
        Global.Coin.Value = PlayerPrefs.GetInt("coin",0);
        
        Global.ExpPercent.Value = PlayerPrefs.GetFloat(nameof(ExpPercent),0.4f);
        Global.CoinPercent.Value = PlayerPrefs.GetFloat(nameof(CoinPercent),0.1f);
        Global.Coin.Register(coin =>
        {
           PlayerPrefs.SetInt(nameof(coin),coin); 
        });
        Global.ExpPercent.Register(expPercent =>
        {
           PlayerPrefs.SetFloat(nameof(ExpPercent),expPercent); 
        });
        Global.CoinPercent.Register(coinPercent =>
        {
           PlayerPrefs.SetFloat(nameof(CoinPercent),coinPercent); 
        });
    }

    public static void ResetData()
    {
        Coin.Value = 0;
        Exp.Value = 0;
        Level.Value = 1;
        CurrentSeconds.Value = 0;
        SimpleAbilityDamage.Value = 1;
        SimpleAbilityDuration.Value = 1.5f;
        // EnemyGenerator.EnemyCount.Value = 0;
    }
    public static int ExpToNextLevel()
    {
        return Level.Value * 5;
    }
    public static void GeneratePowerUp(GameObject gameObject)
    {
        float expPercent = Random.Range(0, 1f);

        if (expPercent < Global.ExpPercent.Value)
        {
            PowerUpManger.Default.Exp.Instantiate()
                .Position(gameObject.Position())
                .Show();
        }

        float coinPercent = Random.Range(0,1f);

        if(coinPercent < Global.CoinPercent.Value)
        {
            PowerUpManger.Default.Coin.Instantiate()
                .Position(gameObject.Position())
                .Show();
        }
    }

    protected override void Init()
    {
        
    }
}
